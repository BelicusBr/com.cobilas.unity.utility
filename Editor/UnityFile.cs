﻿using System.IO;
using UnityEngine;
using UnityEditor;
using System.Text;
using Cobilas.Unity.Utility;

namespace Cobilas.Unity.Editor.Utility {
    public static class UnityFile {

        [MenuItem("Assets/Create/Cobilas file/Text file")]
        public static void InitTextFile()
            => CreateFile("TEXTFile.txt");

        [MenuItem("Assets/Create/Cobilas file/Global custom defines/Target .NET 3.5 ")]
        public static void InitGlobalCustomDefinesNET3_5()
            => CreateFile("mcs.rsp");

        [MenuItem("Assets/Create/Cobilas file/Global custom defines/Target .NET 4.x ")]
        public static void InitGlobalCustomDefinesNET4_X()
            => CreateFile("csc.rsp");

        [MenuItem("Assets/Create/Cobilas file/XML file")]
        public static void InitXMLFile()
            => CreateFile("XNLFile.xml",
                "<?xml version=\"1.0\" encoding=\"utf - 8\"?>\n"
                );

        [MenuItem("Assets/Create/Cobilas file/EmpytCS file")]
        public static void InitEmpCsharpFile()
            => CreateFile("EmpytCS.cs",
                ""
                );

        private static void CreateFile(string newFile)
            => CreateFile(newFile, "");

        private static void CreateFile(string newFile, string content) {
            string path = UnityPath.GetDirectoryName(Application.dataPath);
            string assetfolderpath = AssetDatabase.GetAssetPath(Selection.activeObject);
            string newPath = null;

            if (AssetDatabase.IsValidFolder(assetfolderpath))
                newPath = UnityPath.Combine(path, assetfolderpath, Path.GetFileNameWithoutExtension(newFile));
            else newPath = UnityPath.Combine(path, "Assets", Path.GetFileNameWithoutExtension(newFile));

            string newPathCont = newPath;
            ulong index = 0;
            while (File.Exists(string.Format("{0}{1}", newPathCont, Path.GetExtension(newFile)))) {
                index += 1;
                newPathCont = string.Format("{0}({1})", newPath, index);
            }

            newPathCont = string.Format("{0}{1}", newPathCont, Path.GetExtension(newFile));

            using (FileStream fileStream = new FileStream(newPathCont, FileMode.CreateNew, FileAccess.Write, FileShare.Write))
                fileStream.Write(content, Encoding.UTF8);

            AssetDatabase.Refresh();
        }
    }
}