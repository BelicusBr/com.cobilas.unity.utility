using System;

namespace Cobilas.Unity.Utility.GameData {
    public interface IGameDataList {
        int Count { get; }
        GameDataBase this[int index] { get; }
        GameDataBase this[string name] { get; }

        bool Contains(string name);
        int IndexOff(string name);
        void Add(GameDataBase item);
        void Add(GameDataBase[] itens);
        bool Remove(string name);
        bool Remove(GameDataBase item);
        void AddAndRemove(GameDataBase item);
        void AddAndRemove(GameDataBase[] itens);
        void Clear();
        GameDataItem GetItem(string name);
        GameDataList GetList(string name);
        GameDataList GetListInList(string name);
        void Foreach(Action<GameDataItem> action);
        void Foreach(Action<GameDataList> action);
        void Foreach(Action<GameDataBase> action);
    }
}
