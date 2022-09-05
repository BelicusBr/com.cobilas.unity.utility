using System;
using System.Collections;
using Cobilas.Collections;
using System.Collections.Generic;

namespace Cobilas.Unity.Utility.GameData {
    [Serializable]
    public sealed class GameDataList : GameDataBase, IGameDataList, IEnumerable, IEnumerable<GameDataBase> {
        private string name;
        private GameDataBase[] data;

        public override string Name => name;
        public int Count => ArrayManipulation.ArrayLength(data);

        public GameDataBase this[int index] { get => data[index]; }
        public GameDataBase this[string name] { get => data[IndexOff(name)]; }

        public GameDataList(string name, params GameDataBase[] data) {
            this.name = name;
            this.data = data;
        }

        public GameDataList(string name) : this(name, new GameDataBase[0]) { }

        public override void Dispose() {
            name = (string)null;
            for (int I = 0; I < Count; I++)
                data[I].Dispose();
            Clear();
        }

        public IEnumerator<GameDataBase> GetEnumerator()
            => new ArrayToIEnumerator<GameDataBase>(data);

        public GameDataItem GetItem(string name) {
            for (int I = 0; I < Count; I++)
                if (data[I].Name == name)
                    if (data[I] is GameDataItem gdi)
                        return gdi;
            return (GameDataItem)null;
        }

        public GameDataList GetList(string name) {
            for (int I = 0; I < Count; I++)
                if (data[I].Name == name)
                    if (data[I] is GameDataList gdl)
                        return gdl;
            return (GameDataList)null;
        }

        public GameDataList GetListInList(string name) {
            for (int I = 0; I < Count; I++) {
                if (data[I].Name == name) {
                    if (data[I] is GameDataList gdl)
                        return gdl;
                } else if (data[I] is GameDataList gdl)
                    return gdl.GetListInList(name);
            }
            return (GameDataList)null;
        }

        public void Add(GameDataBase item)
            => ArrayManipulation.Add(item, ref data);

        public void Add(GameDataBase[] itens)
            => ArrayManipulation.Add(itens, ref data);

        public bool Remove(GameDataBase item)
            => Remove(item.Name);

        public bool Remove(string name) {
            int index;
            if ((index = IndexOff(name)) < 0) return false;
            data[index].Dispose();
            ArrayManipulation.Remove(index, ref data);
            return true;
        }

        public void AddAndRemove(GameDataBase item) {
            _ = Remove(item.Name);
            Add(item);
        }

        public void AddAndRemove(GameDataBase[] itens) {
            for (int I = 0; I < ArrayManipulation.ArrayLength(itens); I++)
                AddAndRemove(itens[I]);
        }

        public void Clear()
            => ArrayManipulation.ClearArraySafe(ref data);

        public bool Contains(string name)
            => IndexOff(name) >= 0;

        public int IndexOff(string name) {
            for (int I = 0; I < Count; I++)
                if (data[I].Name == name)
                    return I;
            return -1;
        }

        public void Foreach(Action<GameDataItem> action) {
            for (int I = 0; I < Count; I++)
                if (this[I] is GameDataItem gdi)
                    action?.Invoke(gdi);
        }

        public void Foreach(Action<GameDataList> action) {
            for (int I = 0; I < Count; I++)
                if (this[I] is GameDataList gdl)
                    action?.Invoke(gdl);
        }

        public void Foreach(Action<GameDataBase> action) {
            for (int I = 0; I < Count; I++)
                action?.Invoke(this[I]);
        }

        IEnumerator IEnumerable.GetEnumerator()
            => new ArrayToIEnumerator<GameDataBase>(data);
    }
}
