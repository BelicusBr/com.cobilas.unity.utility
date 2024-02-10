using System;
using UnityEngine;
using System.Collections;

namespace Cobilas.Unity.Utility {
    /// <summary>Commonplace Task</summary>
    public sealed class ComTask {
        private readonly int id;
        public Coroutine coroutine;
        private Exception exception;
        private ComTaskStatus status;
        private IEnumerator enumerator;

        public int Id => id;
        public ComTaskStatus Status => status;
        public Exception Exception => exception;

        private ComTask() {
            status = ComTaskStatus.WaitingToRun;
            this.id = new System.Random().Next(360, 4500);
        }

        public void Reset() {
            exception = (Exception)null;
            coroutine = (Coroutine)null;
            status = ComTaskStatus.WaitingToRun;
        }

        private IEnumerator MyEnumerator() {
            status = ComTaskStatus.Running;
            while (true && status != ComTaskStatus.Canceled) {
                try {
                    if (!enumerator.MoveNext()) break;
                } catch (Exception e) {
                    exception = e;
                    status = ComTaskStatus.Faulted;
                }
                yield return enumerator.Current;
            }
            if (status == ComTaskStatus.Running)
                status = ComTaskStatus.Completed;
        }

        public static ComTask Create(IEnumerator enumerator) {
            ComTask task = new ComTask();
            task.enumerator = enumerator;
            return task;
        }

        public static ComTask StartDelay(ComTask task) {
            if (task.status != ComTaskStatus.WaitingToRun)
                throw ComTaskException.StartException();
            RequestedCoroutines.StartDelay(task);
            return task;
        }

        public static ComTask StartDelay(IEnumerator enumerator)
            => StartDelay(Create(enumerator));

        public static void StopDelay(ComTask task) {
            if (task.status != ComTaskStatus.Running)
                throw ComTaskException.StopException();
            RequestedCoroutines.StopDelay(task); 
        }

        public static ComTask Start(MonoBehaviour mono, ComTask task) {
            if (task.status != ComTaskStatus.WaitingToRun)
                throw ComTaskException.StartException();
            task.coroutine = mono.StartCoroutine(task.MyEnumerator());
            return task;
        }

        public static ComTask Start(MonoBehaviour mono, IEnumerator enumerator)
            => Start(mono, Create(enumerator));

        public static ComTask Start(ComTask task) {
            if (task.status != ComTaskStatus.WaitingToRun)
                throw ComTaskException.StartException();
            RequestedCoroutines.Start(task);
            return task;
        }

        public static ComTask Start(IEnumerator enumerator)
            => Start(Create(enumerator));

        public static void Stop(MonoBehaviour mono, ComTask task) {
            if (task.status != ComTaskStatus.Running)
                throw ComTaskException.StopException();
            task.status = ComTaskStatus.Canceled;
            mono.StopCoroutine(task.coroutine);
        }

        public static void Stop(ComTask task) {
            if (task.status != ComTaskStatus.Running)
                throw ComTaskException.StopException();
            task.status = ComTaskStatus.Canceled;
            RequestedCoroutines.Stop(task);
        }
    }
}
