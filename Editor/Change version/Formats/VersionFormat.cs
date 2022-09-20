using System.Collections.Generic;

namespace Cobilas.Unity.Editor.Utility.ChangeVersion {
    public sealed class VersionFormat : BaseVersionFormats {
        public override KeyValuePair<string, VersionInfo>[] GetFormats()
            => new KeyValuePair<string, VersionInfo>[] {
                new KeyValuePair<string, VersionInfo>("Default-mmbr", new VersionInfo(
                    new VersionValue("Major", 0, false, false, false, (string)null),
                    new VersionValue("Minor", 0, false, false, false, (string)null),
                    new VersionValue("Build", 0, true, false, false, (string)null),
                    new VersionValue("Revision", 0, false, false, true, (string)null)
                    )),
                new KeyValuePair<string, VersionInfo>("Default-mbr", new VersionInfo(
                    new VersionValue("Major", 0, false, false, false, (string)null),
                    new VersionValue("Build", 0, true, false, false, (string)null),
                    new VersionValue("Revision", 0, false, false, true, (string)null)
                    )),
                new KeyValuePair<string, VersionInfo>("Default-mr", new VersionInfo(
                    new VersionValue("Major", 0, false, false, false, (string)null),
                    new VersionValue("Revision", 0, false, false, true, (string)null)
                    ))
            };
    }
}
