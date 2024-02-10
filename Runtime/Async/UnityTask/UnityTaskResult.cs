using System;

namespace Cobilas.Unity.Utility {
    public class UnityTaskResult : IDisposable {
        private object value;

        public UnityTaskResult()
        {
            this.value = (object)null;
        }

        public virtual void SetValue(object value) => this.value = value;
        public virtual object GetValue() => this.value;
        public virtual T GetValue<T>() => (T)this.value;

        public void Dispose() {
            if (value is IDisposable idp)
                idp.Dispose();
        }
    }
}
