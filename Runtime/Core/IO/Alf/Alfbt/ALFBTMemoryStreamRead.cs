﻿using System;
using Cobilas.Collections;
using Cobilas.IO.Alf.Components;
using Cobilas.IO.Alf.Alfbt.Flags;
using System.Collections.Generic;
using Cobilas.IO.Alf.Alfbt.Components;
using Cobilas.IO.Alf.Components.Collections;
using Cobilas.IO.Alf.Alfbt.Components.Collections;

namespace Cobilas.IO.Alf.Alfbt {
    internal class ALFBTMemoryStreamRead : ALFBTRead {

        private bool disposedValue;
        private readonly ALFItem root;
        private readonly ALFReadSettings memory;

        public override int Count => root.Count;
        public override ALFReadSettings Settings => memory;
        public override IItemReadOnly ReadOnly => new ALFBTFlagReadOnly(root);
        public override IItemReadOnly this[int index] => new ALFBTFlagReadOnly(root[index]);

        public ALFBTMemoryStreamRead(ALFReadSettings memory) {
            this.memory = memory;
            root = ALFItem.DefaultRoot;
            Read();
        }

        public override void Dispose() {
            if (disposedValue)
                throw ALFException.GetALFException(1000, GetType());
            disposedValue = true;
            root.Dispose();
            memory.Dispose();
        }

        public override void Close() => memory.Close();

        public override void Flush() => memory.Flush();

        public override bool FlagExists(string name, AlfbtFlags flags) {
            foreach (IItemReadOnly item in GetFlags(name))
                switch (flags) {
                    case AlfbtFlags.MarkingFlag:
                        return GetFlagName(item.Name) == name && GetFlagPrefix(item.Name) == "tg";
                    case AlfbtFlags.TextFlag:
                        return GetFlagName(item.Name) == name && GetFlagPrefix(item.Name) == "txt";
                    case AlfbtFlags.HeaderFlag:
                        return GetFlagName(item.Name) == name && GetFlagPrefix(item.Name) == "hd";
                    default: return false;
                }
            return false;
        }

        public override bool FlagExists(string name) {
            foreach (ALFItem item in root)
                if (GetFlagName(item.name) == name)
                    return true;
            return false;
        }

        public override CommentFlag GetCommentFlag() {
            CommentFlag comment = new CommentFlag();
            foreach (var item in ReadOnly)
                if (item.Name == ALFBTUtility.n_Comment)
                    comment.Add(item as ALFBTFlagReadOnly);
            return comment;
        }

        public override IEnumerator<IItemReadOnly> GetEnumerator()
            => ((IEnumerable<IItemReadOnly>)ReadOnly).GetEnumerator();

        public override IItemReadOnly GetFlag(string name) {
            foreach (var item in ReadOnly)
                if (GetFlagName(item.Name) == name)
                    return item;
            return (ALFBTFlagReadOnly)null;
        }

        public IItemReadOnly GetFlag(string name, AlfbtFlags flags) {
            foreach (IItemReadOnly item in GetFlags(name)) {
                string prefix = GetFlagPrefix(item.Name);
                string flagName = GetFlagName(item.Name);
                if (flags == AlfbtFlags.MarkingFlag && prefix == "tg" && flagName == name)
                    return item;
                else if (flags == AlfbtFlags.HeaderFlag && prefix == "hd" && flagName == name)
                    return item;
                else if (flags == AlfbtFlags.TextFlag && prefix == "txt" && flagName == name)
                    return item;
            }
            return (ALFBTFlagReadOnly)null;
        }

        protected override void Read() {
            using (CharacterCursor cursor = new CharacterCursor(memory.Read()))
                GetALFBTFlags(root, cursor);
            if (memory.RemoveEscapeOnSpecialCharacters)
                RemoveEscapeOnSpecialCharactersInALFItem(root);
        }

        private IItemReadOnly[] GetFlags(string name) {
            IItemReadOnly[] res = null;
            foreach (IItemReadOnly item in ReadOnly)
                if (GetFlagName(item.Name) == name)
                    ArrayManipulation.Add(item, ref res);
            return res;
        }

        private string GetFlagName(string name) {
            string prefix = GetFlagPrefix(name);
            if (name.Contains('/') && (prefix == "hd" || prefix == "txt"))
                return name.Remove(0, name.IndexOf('/') + 1);
            return name;
        }

        private string GetFlagPrefix(string name) {
            if (name.Contains('/'))
                return name.Remove(name.IndexOf('/'));
            return "tg";
        }

        protected override void RemoveEscapeOnSpecialCharactersInALFItem(ALFItem root) {
            foreach (ALFItem item in root) {
                if (item.name == ALFBTUtility.n_Comment) continue;
                _ = item.text.Replace("\\\\", "\\").Replace("\\/", "/").Replace("\\*", "*");
            }
        }
    }
}
