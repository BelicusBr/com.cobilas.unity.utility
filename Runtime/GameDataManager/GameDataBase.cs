using System;

namespace Cobilas.Unity.Utility.GameData {
    [Serializable]
    public abstract class GameDataBase : IDisposable {
        public abstract string Name { get; }
        public abstract void Dispose();
    }
}
