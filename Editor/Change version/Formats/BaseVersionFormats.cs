using System.Collections.Generic;

namespace Cobilas.Unity.Editor.Utility.ChangeVersion {
    public abstract class BaseVersionFormats {
        public abstract KeyValuePair<string, VersionInfo>[] GetFormats();
    }
}
