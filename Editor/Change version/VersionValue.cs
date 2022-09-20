using System;

namespace Cobilas.Unity.Editor.Utility.ChangeVersion {
    public struct VersionValue : IDisposable {
        public long level;
        public string name;
        public bool isBuild;
        public bool isMRACOC;
        public string format;
        public bool updateWhenClose;

        public bool Rename;
        public bool ChangeLevel;
        public bool ChangeFormat;
        public bool AlreadyUpdatedWhenClose;

        public VersionValue(string name, long level, bool isBuild, bool isMRACOC, bool updateWhenClose, string format) {
            this.name = name;
            this.level = level;
            this.isBuild = isBuild;
            this.isMRACOC = isMRACOC;
            this.format = format;
            this.updateWhenClose = updateWhenClose;
            this.Rename =
                this.ChangeFormat =
                this.ChangeLevel = 
                this.AlreadyUpdatedWhenClose = false;
        }

        public void Dispose() {
            level = default;
            name = format = (string)null;
        }
    }
}
