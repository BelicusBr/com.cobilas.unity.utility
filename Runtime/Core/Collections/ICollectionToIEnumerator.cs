﻿using System.Collections.Generic;

namespace Cobilas.Collections {
    public class ICollectionToIEnumerator<T> : ArrayToIEnumerator<T> {
        public override T Current => base.Current;

        public ICollectionToIEnumerator(ICollection<T> collection) : base()
            => collection.CopyTo(list, 0);

        public override bool MoveNext() => base.MoveNext();

        public override void Reset() => base.Reset();

        protected override void Dispose(bool disposing) => base.Dispose(disposing);
    }
}
