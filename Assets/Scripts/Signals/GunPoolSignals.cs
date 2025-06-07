using Extentions;
using UnityEngine.Events;

namespace Signals
{
    public class GunPoolSignals : MonoSingleton<GunPoolSignals>
    {
        public UnityAction onWrongGunPool = delegate { };
        public UnityAction onWrongGunPoolExit = delegate { };
        public UnityAction onGunPoolExit = delegate { };
    }
}