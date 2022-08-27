using System;
using UnityEngine;
using Cobilas.Collections;

namespace Cobilas.Unity.Utility {
    [Serializable]
    public class RepaintTexture2D {
        [SerializeField] private Texture2D mainTexture;
        [SerializeField, HideInInspector] private Texture2D modifiedTexture;
        [SerializeField] private Color tint;
        private Color oldTint;

        public Texture2D MainTexture => mainTexture;
        public Texture2D ModifiedTexture => modifiedTexture;
        public Color Tint { get => tint; set => SetTint(value); }

        public RepaintTexture2D(Texture2D mainTexture, Color tint, bool linear) {
            this.mainTexture = mainTexture;
            this.tint = tint;
            Copy(linear);
            Paint();
        }

        public RepaintTexture2D(Texture2D mainTexture, Color tint) :
            this(mainTexture, tint, false) { }

        public RepaintTexture2D(Texture2D mainTexture, bool linear) :
            this(mainTexture, Color.white, linear) { }

        public RepaintTexture2D(Texture2D mainTexture) :
            this(mainTexture, Color.white, false) { }

        private void Copy(bool linear) {
            modifiedTexture = new Texture2D(mainTexture.width, mainTexture.height, mainTexture.format, mainTexture.mipmapCount, linear);
            Graphics.CopyTexture(mainTexture, modifiedTexture);
            modifiedTexture.Apply();
        }

        private void Paint() {
            Color[] pixes = mainTexture.GetPixels();
            for (int I = 0; I < ArrayManipulation.ArrayLength(pixes); I++)
                pixes[I] = pixes[I] * tint;
            modifiedTexture.SetPixels(pixes);
            modifiedTexture.Apply();
        }

        private void SetTint(Color color) {
            if (oldTint != (oldTint = tint = color))
                Paint();
        }
    }
}
