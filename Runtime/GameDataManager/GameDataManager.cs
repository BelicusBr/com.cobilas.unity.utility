using System.IO;
using Cobilas.Unity.Utility.Cryptography;
using System.Runtime.Serialization.Formatters.Binary;

namespace Cobilas.Unity.Utility.GameData {
    public static class GameDataManager {
        public static string GameDataFolder => UnityPath.Combine(UnityPath.StreamingAssetsPath, "GameData");

        public static void UnloadAndEncryptGameData(string filePath, params GameDataBase[] data)
            => Internal_UnloadGameData(filePath, true, StringCipher.password, data);

        public static void UnloadAndEncryptGameData(string filePath, string passPhrase, params GameDataBase[] data)
            => Internal_UnloadGameData(filePath, true, passPhrase, data);

        public static void UnloadGameData(string filePath, params GameDataBase[] data)
            => Internal_UnloadGameData(filePath, false, string.Empty, data);

        public static GameDataList LoadEncryptedGameData(string filePath)
            => Internal_LoadGameData(filePath, true, StringCipher.password);

        public static GameDataList LoadEncryptedGameData(string filePath, string passPhrase)
            => Internal_LoadGameData(filePath, true, passPhrase);

        public static GameDataList LoadGameData(string filePath)
            => Internal_LoadGameData(filePath, false, string.Empty);

        private static GameDataList Internal_LoadGameData(string filePath, bool Decrypt, string passPhrase) {
            byte[] txt = File.ReadAllBytes(filePath);
            txt = Decrypt ? StringCipher.DecryptBytes(txt, passPhrase) : txt;
            using (MemoryStream memory = new MemoryStream(txt))
                return (GameDataList)new BinaryFormatter().Deserialize(memory);
        }

        private static void Internal_UnloadGameData(string filePath, bool Encrypt, string passPhrase, params GameDataBase[] data) {
            using (MemoryStream memory = new MemoryStream()) {
                using (GameDataList dataList = new GameDataList("Root", data))
                    new BinaryFormatter().Serialize(memory, dataList);

                using (FileStream writer = File.Create(filePath)) {
                    if (Encrypt) writer.Write(StringCipher.EncryptBytes(memory.ToArray(), passPhrase));
                    else writer.Write(memory.ToArray());
                }
            }
        }
    }
}
