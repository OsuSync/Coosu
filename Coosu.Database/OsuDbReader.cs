﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using Microsoft.VisualBasic;

namespace Coosu.Database;

/// <summary>
/// Only support version after 20191106
/// <seealso href="https://github.com/ppy/osu/wiki/Legacy-database-file-structure"/>
/// </summary>
public class OsuDbReader : IDisposable
{
    private readonly Stream _stream;
    private readonly BinaryReader _binaryReader;

    private bool _documentStart;
    private bool _objectStart;

    private int _innerIndex = -1;
    private int _innerCount;

    private int _beatmapIndex = 0;
    private int _beatmapCount;

    private int _generalItemIndex = -1;
    private int _beatmapItemIndex = -1;
    private readonly Stack<Action> _arrayHandlers = new();
    private readonly Stack<string> _arrays = new();

    public static readonly string[] GeneralSequence =
    {
        "OsuVersion","FolderCount","AccountUnlocked","AccountUnlockDate","PlayerName","BeatmapCount",
        "Beatmaps[]", "UserPermission"
    };

    private static readonly DataType[] GeneralSequenceType =
    {
        DataType.Int32, DataType.Int32, DataType.Boolean, DataType.DateTime, DataType.String, DataType.Int32,
        DataType.Array, DataType.Int32
    };

    public static readonly string[] BeatmapSequence =
    {
        "Artist", "ArtistUnicode", "Title", "TitleUnicode", "Creator", "Difficulty",
        "AudioFileName", "MD5Hash", "FileName", "RankedStatus", "CirclesCount", "SlidersCount", "SpinnersCount",
        "LastModifiedTime", "ApproachRate", "CircleSize", "HPDrain", "OverallDifficulty", "SliderVelocity",

        "StarRatingStandardCount", "StarRatingStandard[]", "StarRatingTaikoCount", "StarRatingTaiko[]",
        "StarRatingCtbCount", "StarRatingCtb[]", "StarRatingManiaCount", "StarRatingMania[]",

        "DrainTime", "TotalTime", "AudioPreviewTime",
        "TimingPointCount", "TimingPoints[]",

        "BeatmapId", "BeatmapSetId", "ThreadId",
        "GradeStandard", "GradeTaiko", "GradeCtb", "GradeMania",
        "LocalOffset", "StackLeniency", "Ruleset", "SongSource", "SongTags", "OnlineOffset",
        "TitleFont", "IsUnplayed", "LastTimePlayed", "IsOsz2", "FolderName", "LastTimeChecked",
        "IsSoundIgnored", "IsSkinIgnored", "IsStoryboardDisabled", "IsVideoDisabled", "IsVisualOverrideEnabled",
        "LastModification?", "ManiaScrollSpeed",
    };

    public static readonly DataType[] BeatmapSequenceType =
    {
        DataType.String, DataType.String, DataType.String, DataType.String, DataType.String, DataType.String,
        DataType.String, DataType.String, DataType.String, DataType.Byte, DataType.Int16, DataType.Int16,
        DataType.Int16,
        DataType.DateTime, DataType.Single, DataType.Single, DataType.Single, DataType.Single, DataType.Double,

        DataType.Int32, DataType.Array, DataType.Int32, DataType.Array,
        DataType.Int32, DataType.Array, DataType.Int32, DataType.Array,

        DataType.Int32, DataType.Int32, DataType.Int32,
        DataType.Int32, DataType.Array,

        DataType.Int32, DataType.Int32, DataType.Int32,
        DataType.Byte, DataType.Byte, DataType.Byte, DataType.Byte,
        DataType.Int16, DataType.Single, DataType.Byte, DataType.String, DataType.String, DataType.Int16,
        DataType.String, DataType.Boolean, DataType.DateTime, DataType.Boolean, DataType.String, DataType.DateTime,
        DataType.Boolean, DataType.Boolean, DataType.Boolean, DataType.Boolean, DataType.Boolean,
        DataType.Int32, DataType.Byte
    };


    public OsuDbReader(string path) : this(File.OpenRead(path))
    {
    }

    public OsuDbReader(Stream stream)
    {
        _stream = stream;
        _binaryReader = new BinaryReader(stream);
    }

    public string? Name { get; private set; }
    public object? Value { get; private set; }
    public NodeType NodeType { get; private set; }
    public DataType DataType { get; private set; }

    public bool Read()
    {
        if (_generalItemIndex >= GeneralSequence.Length - 1)
        {
            return false;
        }

        if (!_documentStart && _generalItemIndex == -1)
        {
            NodeType = NodeType.FileStart;
            _documentStart = true;
            return true;
        }

        if (_arrayHandlers.Count == 0)
        {
            _generalItemIndex++;
            Name = GeneralSequence[_generalItemIndex];
            DataType = GeneralSequenceType[_generalItemIndex];
            if (DataType != DataType.Array)
            {
                NodeType = NodeType.Attribute;
                Value = ReadValueByType(DataType);
                if (Name == "BeatmapCount")
                {
                    _beatmapCount = (int)Value;
                }
            }
            else
            {
                NodeType = NodeType.ArrayStart;
                Value = null;
                _arrayHandlers.Push(ReadBeatmap);
                _arrays.Push(Name);
            }
        }
        else
        {
            _arrayHandlers.Peek().Invoke();
        }

        return true;
    }

    private void ReadBeatmap()
    {
        if (_beatmapItemIndex >= BeatmapSequence.Length - 1)
        {
            if (ValidateEndArray(_beatmapCount, ref _beatmapIndex))
                return;

            _beatmapIndex++;
            _beatmapItemIndex = -1;
            _objectStart = false;
            Name = null;
            Value = null;
            DataType = DataType.Object;
            NodeType = NodeType.ObjectEnd;
            return;
        }

        if (!_objectStart && _beatmapItemIndex == -1)
        {
            NodeType = NodeType.ObjectStart;
            Name = null;
            Value = null;
            DataType = DataType.Object;
            _objectStart = true;
            return;
        }

        _beatmapItemIndex++;

        Name = BeatmapSequence[_beatmapItemIndex];
        DataType = BeatmapSequenceType[_beatmapItemIndex];
        if (DataType != DataType.Array)
        {
            NodeType = NodeType.Attribute;
            Value = ReadValueByType(DataType);
        }
        else
        {
            NodeType = NodeType.ArrayStart;
            if (_beatmapItemIndex is 20 or 22 or 24 or 26)
            {
                _innerCount = (int)Value!;

                _arrayHandlers.Push(ReadStarRatings);
                _arrays.Push(Name);
            }
            else if (_beatmapItemIndex is 31)
            {
                _innerCount = (int)Value!;

                _arrayHandlers.Push(ReadTimingPoints);
                _arrays.Push(Name);
            }

            Value = null;
        }
    }

    private void ReadTimingPoints()
    {
        if (ValidateEndArray(_innerCount, ref _innerIndex)) return;
        _innerIndex++;

        Name = null;
        NodeType = NodeType.Attribute;
        DataType = DataType.TimingPoint;
        Value = ReadValueByType(DataType);
    }

    private void ReadStarRatings()
    {
        if (ValidateEndArray(_innerCount, ref _innerIndex)) return;
        _innerIndex++;

        Name = null;
        NodeType = NodeType.Attribute;
        DataType = DataType.IntDoublePair;
        Value = ReadValueByType(DataType);
    }

    private bool ValidateEndArray(int count, ref int index)
    {
        if (index < count - 1) return false;
        index = -1;
        _arrayHandlers.Pop();
        Name = _arrays.Pop();
        Value = null;
        DataType = DataType.Array;
        NodeType = NodeType.ArrayEnd;
        return true;
    }

    private object ReadValueByType(DataType dataType)
    {
        return dataType switch
        {
            DataType.Byte => _binaryReader.ReadByte(),
            DataType.Int16 => _binaryReader.ReadInt16(),
            DataType.Int32 => _binaryReader.ReadInt32(),
            DataType.Int64 => _binaryReader.ReadInt64(),
            DataType.ULEB128 => _stream.ReadLEB128Unsigned(),
            DataType.Single => _binaryReader.ReadSingle(),
            DataType.Double => _binaryReader.ReadDouble(),
            DataType.Boolean => _binaryReader.ReadBoolean(),
            DataType.String => _binaryReader.ReadStringA(),
            DataType.IntDoublePair => _binaryReader.ReadIntDoublePairA(),
            DataType.TimingPoint => _binaryReader.ReadTimingPointA(),
            DataType.DateTime => _binaryReader.ReadDateTimeA(),
            _ => throw new ArgumentOutOfRangeException(nameof(dataType), dataType, null)
        };
    }

    public void Dispose()
    {
        _stream.Dispose();
        _binaryReader.Dispose();
    }
}