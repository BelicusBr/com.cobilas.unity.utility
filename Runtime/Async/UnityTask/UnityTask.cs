using System;
using System.Threading;
using System.Threading.Tasks;

namespace Cobilas.Unity.Utility {
    public class UnityTask : IAsyncResult, IDisposable {
        private Task myTask;
        private bool disposed;
        private Delegate myAction;
        private UnityTaskResult myResult;
        private CancellationTokenSource source;

        public int Id => myTask.Id;
        public Task MyTask => myTask;
        public bool IsDisposed => disposed;
        public TaskStatus Status => myTask.Status;
        public bool IsFaulted => myTask.IsFaulted;
        public UnityTaskResult Result => myResult;
        public bool IsCanceled => myTask.IsCanceled;
        public object AsyncState => myTask.AsyncState;
        public bool IsCompleted => myTask.IsCompleted;
        public AggregateException Exception => myTask.Exception;
        public bool IsRunning => myTask.Status == TaskStatus.Running;
        public bool IsCancellationRequested => source.IsCancellationRequested;

        WaitHandle IAsyncResult.AsyncWaitHandle => (myTask as IAsyncResult).AsyncWaitHandle;
        bool IAsyncResult.CompletedSynchronously => (myTask as IAsyncResult).CompletedSynchronously;

        private UnityTask() {
#if UNITY_EDITOR
            EditorUnityTaskPool.AddTask(this);
#endif
        }

        public UnityTask(Action<UnityTask> action, CancellationTokenSource source, UnityTaskResult result) : this() {
            this.myAction = action;
            this.myResult = result;
            this.myTask = new Task(TaskAction, (this.source = source).Token);
        }

        public UnityTask(Action<UnityTask> action, CancellationTokenSource source) :
            this(action, source, new UnityTaskResult()) { }

        public UnityTask(Action<UnityTask> action) :
            this(action, new CancellationTokenSource()) { }

        public void Continue(CancellationTokenSource newsource) {
            myTask = myTask.ContinueWith((a) => {
                a.Dispose();
                source.Dispose();
                source = newsource;
            });
        }

        public void Wait()
            => myTask.Wait();

        public void Wait(int millisecondsTimeout)
            => myTask.Wait(millisecondsTimeout);

        public void Wait(TimeSpan timeout)
            => myTask.Wait(timeout);

        public void Continue()
            => Continue(new CancellationTokenSource());

        public void Start()
            => myTask.Start();

        public void Cancel()
            => source.Cancel();

        public void Cancel(bool throwOnFirstException)
            => source.Cancel(throwOnFirstException);

        public void CancelAfter(TimeSpan timeSpan)
            => source.CancelAfter(timeSpan);

        public void CancelAfter(int millisecondsDelay)
            => source.CancelAfter(millisecondsDelay);

        public void Dispose() {
            if (disposed) 
                throw new ObjectDisposedException($"The object {nameof(UnityTask)} has already been discarded");
            disposed = true;
            myTask?.Dispose();
            source?.Dispose();
        }

        private void TaskAction() {
            _ = myAction.DynamicInvoke(this);
            if (source.IsCancellationRequested)
                source.Token.ThrowIfCancellationRequested();
        }

        public static UnityTask Delay(TimeSpan delay, CancellationTokenSource source) {
            Task task = Task.Delay(delay, source.Token);
            UnityTask unityTask = new UnityTask();
            unityTask.myTask = task;
            unityTask.myResult = new UnityTaskResult();
            unityTask.myAction = (Action<UnityTask>)null;
            unityTask.source = source;
            return unityTask;
        }

        public static UnityTask Delay(TimeSpan delay)
            => Delay(delay, new CancellationTokenSource());

        public static UnityTask Delay(int millisecondsDelay, CancellationTokenSource source)
            => Delay(TimeSpan.FromMilliseconds(millisecondsDelay), source);

        public static UnityTask Delay(int millisecondsDelay)
            => Delay(millisecondsDelay, new CancellationTokenSource());

        public static UnityTask RunAndDelay(TimeSpan delay, CancellationTokenSource source) {
            UnityTask unityTask = Delay(delay, source);
            unityTask.Wait();
            return unityTask;
        }

        public static UnityTask RunAndDelay(TimeSpan delay)
            => RunAndDelay(delay, new CancellationTokenSource());

        public static UnityTask Run(Action<UnityTask> action, CancellationTokenSource source, UnityTaskResult result) {
            UnityTask temp = new UnityTask(action, source, result);
            temp.Start();
            return temp;
        }

        public static UnityTask Run(Action<UnityTask> action, CancellationTokenSource source)
            => Run(action, source, new UnityTaskResult());

        public static UnityTask Run(Action<UnityTask> action)
            => Run(action, new CancellationTokenSource());
    }
}
