namespace Cobilas.Unity.Utility {
    public enum ComTaskStatus : byte {
        Running = 0,
        Completed = 1,
        Canceled = 2,
        Faulted = 3,
        WaitingToRun = 4
    }
}
