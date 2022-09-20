using System;
using System.Text;
using System.Collections.Generic;

namespace Cobilas.Unity.Editor.Utility.ChangeVersion {
    public sealed class VersionInfo : IDisposable {
        public List<VersionValue> versions;

        public VersionValue this[int index] {
            get => versions[index];
            set => versions[index] = value;
        }

        public VersionValue this[string name] {
            get => versions[IndexOff(name)];
            set => versions[IndexOff(name)] = value;
        }

        public VersionInfo(params VersionValue[] versions)
            => this.versions = new List<VersionValue>(versions);

        public VersionInfo() : this(new VersionValue[0]) { }

        public void Add(string name) {
            VersionValue item = new VersionValue();
            item.name = name;
            versions.Add(item);
        }

        public void Dispose() {
            for (int I = 0; I < versions.Count; I++)
                versions[I].Dispose();
            versions.Clear();
            versions.Capacity = 0;
        }

        public int IndexOff(string name) {
            for (int I = 0; I < versions.Count; I++)
                if (versions[I].name == name)
                    return I;
            return -1;
        }

        public override string ToString() {
            StringBuilder builder = new StringBuilder();
            for (int I = 0; I < versions.Count; I++) {
                builder.Append(versions[I].level);
                if (I < versions.Count - 1)
                    builder.Append('.');
            }
            return builder.ToString();
        }
    }
}
