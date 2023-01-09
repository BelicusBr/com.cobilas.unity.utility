using UnityEngine;
using System.Collections.Generic;

namespace Cobilas.Unity.Utility {
    public class RequestedCoroutines : MonoBehaviour {
        private static List<ComTask> requestedInitialization;
        private static List<ComTask> finalizationRequested;

        static RequestedCoroutines()
        {
            requestedInitialization = new List<ComTask>();
            finalizationRequested = new List<ComTask>();
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Init() {
            if (FindObjectOfType<RequestedCoroutines>() != null) return;
            GameObject gob = new GameObject(nameof(RequestedCoroutines), typeof(RequestedCoroutines));
            gob.transform.position = Vector3.zero;
            gob.isStatic = true;
            DontDestroyOnLoad(gob);
        }

        private void LateUpdate() {
            if (requestedInitialization.Count != 0) {
                foreach (var item in requestedInitialization)
                    ComTask.Start(this, item);
                requestedInitialization.Clear();
            }
            if (finalizationRequested.Count != 0) {
                foreach (var item in finalizationRequested)
                    ComTask.Stop(this, item);
                finalizationRequested.Clear();
            }
        }

        internal static void StartDelay(ComTask coroutine)
            => requestedInitialization.Add(coroutine);

        internal static void StopDelay(ComTask coroutine)
            => finalizationRequested.Add(coroutine);

        internal static void Start(ComTask coroutine) {
            RequestedCoroutines temp = FindObjectOfType<RequestedCoroutines>();
            if (temp == (RequestedCoroutines)null) {
                ComTask.Start(temp, coroutine);
                return;
            }
            StartDelay(coroutine);
        }

        internal static void Stop(ComTask coroutine) {
            RequestedCoroutines temp = FindObjectOfType<RequestedCoroutines>();
            if (temp == (RequestedCoroutines)null) {
                ComTask.Stop(temp, coroutine);
                return;
            }
            StopDelay(coroutine);
        }
    }
}
