using System;

namespace Cobilas.Unity.Utility.GameData {
    [Serializable]
    public sealed class GameDataItem : GameDataBase {
        private string name;
        private object value;

        public object Value => value;
        public override string Name => name;

        public GameDataItem(string name, object value) {
            this.name = name;
            this.value = value;
        }

        public override void Dispose() {
            name = (string)null;
            value = (object)null;
        }
    }
}
