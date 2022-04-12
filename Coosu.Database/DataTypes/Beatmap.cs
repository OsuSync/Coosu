﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coosu.Database.DataTypes;

public record Beatmap
{
    public string Artist { get; set; } = null!;
    public string ArtistUnicode { get; set; } = null!;
    public string Title { get; set; } = null!;
    public string TitleUnicode { get; set; } = null!;
    public string Creator { get; set; } = null!;
    public string Difficulty { get; set; } = null!;
    public string AudioFileName { get; set; } = null!;
    public string MD5Hash { get; set; } = null!;
    public string FileName { get; set; } = null!;
    public RankedStatus RankedStatus { get; set; }
    public short CirclesCount { get; set; }
    public short SlidersCount { get; set; }
    public short SpinnersCount { get; set; }
    public DateTime LastModified { get; set; }
    public float ApproachRate { get; set; }
    public float CircleSize { get; set; }
    public float HpDrain { get; set; }
    public float OverallDifficulty { get; set; }
    public double SliderVelocity { get; set; }

    public Dictionary<Mods, double>? StarRatingStd { get; set; }
    public Dictionary<Mods, double>? StarRatingTaiko { get; set; }
    public Dictionary<Mods, double>? StarRatingCtb { get; set; }
    public Dictionary<Mods, double>? StarRatingMania { get; set; }

    public int DrainTime { get; set; }
    public int TotalTime { get; set; }
    public int AudioPreviewTime { get; set; }
    public List<TimingPoint>? TimingPoints { get; set; }
    public int BeatmapId { get; set; }
    public int BeatmapSetId { get; set; }
    public int ThreadId { get; set; }
    public Grade GradeStandard { get; set; }
    public Grade GradeTaiko { get; set; }
    public Grade GradeCtb { get; set; }
    public Grade GradeMania { get; set; }
    public short LocalOffset { get; set; }
    public float StackLeniency { get; set; }
    public DbGameMode GameMode { get; set; }
    public string Source { get; set; } = null!;
    public string Tags { get; set; } = null!;
    public short OnlineOffset { get; set; }
    public string TitleFont { get; set; } = null!;
    public bool IsUnplayed { get; set; }
    public DateTime LastPlayed { get; set; }
    public bool IsOsz2 { get; set; }
    public string FolderName { get; set; } = null!;
    public DateTime LastTimeChecked { get; set; }
    public bool IsSoundIgnored { get; set; }
    public bool IsSkinIgnored { get; set; }
    public bool IsStoryboardDisabled { get; set; }
    public bool IsVideoDisabled { get; set; }
    public bool IsVisualOverride { get; set; }
    public byte ManiaScrollSpeed { get; set; }
}