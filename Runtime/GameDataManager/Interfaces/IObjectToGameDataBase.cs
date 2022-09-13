namespace Cobilas.Unity.Utility.GameData {
    public interface IObjectToGameDataBase {
        GameDataBase ObjectToGameDataBase();
        GameDataBase ObjectToGameDataBase(string name);
    }
}
