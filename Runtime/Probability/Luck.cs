using System;
using System.Collections.Generic;

namespace Cobilas.Unity.Utility.Probability {
    public static class Luck {

        public static Func<List<IProbability>, IProbability> WhatProbabilityFunc;

        public static T WhatProbability<T>(T[] list) where T : IProbability
            => WhatProbability<T>(new List<T>(list));

        public static T WhatProbability<T>(List<T> list) where T : IProbability
            => (T)WhatProbability(list.ConvertAll<IProbability>((p) => p));

        public static IProbability WhatProbability(List<IProbability> list)
            => WhatProbabilityFunc == null ? IWhatProbability(list) : WhatProbabilityFunc(list);

        private static IProbability IWhatProbability(List<IProbability> list) {
            if (list is null || list.Count == 0)
                return (IProbability)null;
            double random = AsyncRandomico.value;
            double totalProb = 0d;
            double probTemp = 0d;
            foreach (IProbability item in list)
                totalProb += item.Probability;
            for (int I = 0; I < list.Count - 1; I++) {
                probTemp += list[I].Probability;
                if (random < probTemp / totalProb)
                    return list[I];
            }
            return list[list.Count - 1];
        }
    }
}
