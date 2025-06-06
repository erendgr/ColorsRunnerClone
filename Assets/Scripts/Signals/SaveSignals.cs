﻿using System;
using Extentions;
using Keys;
using UnityEngine.Events;

namespace Signals
{
    public class SaveSignals : MonoSingleton<SaveSignals>
    {
        //Runner
        public UnityAction onRunnerSaveData = delegate { };
        public Func<int> onGetRunnerLevelID = delegate { return 0; };
        //Idle
        public UnityAction onIdleSaveData = delegate {  };
        public Func<SaveIdleDataParams> onSaveIdleParams= delegate { return default;};
        public UnityAction<SaveIdleDataParams> onLoadIdleGame = delegate { };
        public UnityAction onLoadIdle = delegate {  };
        public Func<int> onIdleLevel = delegate { return 0;};
        //public Func<SaveIdleDataParams> onIdleLoad = delegate { return default;};

    }
}