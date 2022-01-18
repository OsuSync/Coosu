﻿using System.Diagnostics;

namespace Coosu.Beatmap.MetaData
{
    [DebuggerDisplay("{DebuggerDisplay()}")]
    public struct MapIdentity : IMapIdentifiable
    {
        public MapIdentity(string folderName, string version, bool inOwnDb) : this()
        {
            FolderName = folderName;
            Version = version;
            InOwnDb = inOwnDb;
        }

        public string FolderName { get; }
        public string Version { get; }
        public bool InOwnDb { get; }
        public MapIdentity GetIdentity() => this;

        public static MapIdentity Default { get; } = new MapIdentity();

        public override bool Equals(object obj)
        {
            if (obj is not MapIdentity mi)
            {
                return false;
            }

            return mi.FolderName == FolderName && mi.Version == Version;
        }

        public override int GetHashCode() => base.GetHashCode();

        private string DebuggerDisplay()
        {
            if (this.IsMapTemporary())
                return $"temp: \"{FolderName}\"";

            if (InOwnDb)
                return $"own: [\"{FolderName}\",\"{Version}\"]";

            return $"osu: [\"{FolderName}\",\"{Version}\"]";
        }
    }
}
