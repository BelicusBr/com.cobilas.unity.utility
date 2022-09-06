using System.IO;
using UnityEngine;
using UnityEditor;
using System.Text;
using Cobilas.Unity.Utility;

namespace Cobilas.Unity.Editor.Utility {
    [System.Obsolete("Use Cobilas.Unity.Editor.Utility.UnityFile class")]
    public static class CobilasFile {

        //[MenuItem("Assets/Create/Cobilas file/Text file")]
        public static void InitTextFile()
            => UnityFile.InitTextFile();

        //[MenuItem("Assets/Create/Cobilas file/Global custom defines/Target .NET 3.5 ")]
        public static void InitGlobalCustomDefinesNET3_5()
            => UnityFile.InitGlobalCustomDefinesNET3_5();

        //[MenuItem("Assets/Create/Cobilas file/Global custom defines/Target .NET 4.x ")]
        public static void InitGlobalCustomDefinesNET4_X()
            => UnityFile.InitGlobalCustomDefinesNET4_X();

        //[MenuItem("Assets/Create/Cobilas file/XML file")]
        public static void InitXMLFile()
            => UnityFile.InitXMLFile();

        //[MenuItem("Assets/Create/Cobilas file/EmpytCS file")]
        public static void InitEmpCsharpFile()
            => UnityFile.InitEmpCsharpFile();
    }
}
