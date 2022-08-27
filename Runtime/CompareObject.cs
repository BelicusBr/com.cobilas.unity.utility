using System;
using System.Collections.Generic;

namespace Cobilas.Unity.Utility {
    public sealed class CompareObject {
        private object[] compareList;
        private CompareResult result;
        private event Action<CompareResult> compareAction;

        public CompareObject(int capacity) {
            this.compareList = new object[capacity];
            result = new CompareResult(this.compareList);
        }

        public void AddCompareAcition(Action<CompareResult> action)
            => compareAction += action;

        public void ClearCompareAcitionEvent() => compareAction = (Action<CompareResult>)null;

        public bool Compare() {
            result.Reset();
            compareAction?.Invoke(result);
            this.compareList = result.GetList();
            return result.Result;
        }

        public T GetValue<T>(int index) => (T)compareList[index];

        public sealed class CompareResult {
            private bool result;
            private object[] compareList;

            public bool Result => result;

            internal CompareResult(object[] compareList)
                => this.compareList = compareList;

            public void Compare<T>(int index, T item) {
                if (compareList[index] == (object)null) {
                    compareList[index] = item;
                    result = true;
                } else if (!EqualityComparer<T>.Default.Equals((T)compareList[index], item)) {
                    compareList[index] = item;
                    result = true;
                }
            }

            internal void Reset() => result = false;
            internal object[] GetList() => compareList;
        }
    }
}
