using System.IO;
using UnityEngine;
using UnityEditor;
using System.Text;
using Cobilas.Unity.Utility;

namespace Cobilas.Unity.Editor.Utility {
    public static class UnityFile {

        [MenuItem("Assets/Create/Unity file/Text file")]
        public static void InitTextFile()
            => CreateFile("TEXTFile.txt");

        [MenuItem("Assets/Create/Unity file/Global custom defines/Target .NET 3.5 ")]
        public static void InitGlobalCustomDefinesNET3_5()
            => CreateFile("mcs.rsp");

        [MenuItem("Assets/Create/Unity file/Global custom defines/Target .NET 4.x ")]
        public static void InitGlobalCustomDefinesNET4_X()
            => CreateFile("csc.rsp");

        [MenuItem("Assets/Create/Unity file/XML file")]
        public static void InitXMLFile()
            => CreateFile("XNLFile.xml",
                "<?xml version=\"1.0\" encoding=\"utf - 8\"?>\n"
                );

        [MenuItem("Assets/Create/Unity file/EmpytCS file")]
        public static void InitEmpCsharpFile()
            => CreateFile("EmpytCS.cs",
                ""
                );

        [MenuItem("Assets/Create/Unity file/CSharp Editor file")]
        public static void InitEditorCSharpFile() {
            StringBuilder build = new StringBuilder();
            build.AppendLine("using UnityEngine;");
            build.AppendLine("using UnityEditor;\n");
            build.AppendLine("public class CSharpEditor : Editor {\n\n}");
            CreateFile("CSharpEditor.cs", build.ToString());
        }

        [MenuItem("Assets/Create/Unity file/CSharp EditorWindow file")]
        public static void InitEditorWinCSharpFile() {
            StringBuilder build = new StringBuilder();
            build.AppendLine("using UnityEngine;");
            build.AppendLine("using UnityEditor;\n");
            build.AppendLine("public class CSharpEditorWindow : EditorWindow {\n");
            build.AppendLine("\t[MenuItem(\"Window/CSharpEditorWindow\")]");
            build.AppendLine("\tprivate static void Init() {");
            build.AppendLine("\t\tCSharpEditorWindow temp = GetWindow<CSharpEditorWindow>();");
            build.AppendLine("\t\ttemp.titleContent = new GUIContent(\"CSharp Editor Window\");");
            build.AppendLine("\t\ttemp.Show();");
            build.AppendLine("\t}\n}");
            CreateFile("CSharpEditorWindow.cs", build.ToString());
        }

        private static void CreateFile(string newFile)
            => CreateFile(newFile, "");

        private static void CreateFile(string newFile, string content) {
            string path = UnityPath.GetDirectoryName(Application.dataPath);
            string assetfolderpath = AssetDatabase.GetAssetPath(Selection.activeObject);
            string newPath;

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
