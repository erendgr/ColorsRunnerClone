using Managers;
using UnityEngine;

namespace Controllers.RunnerArea
{
    public class GunPoolPhysicsController : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        public bool IsTruePool;

        #endregion

        #region Serialized Variables

        [SerializeField] private GunPoolManager manager;

        #endregion

        #region Private Variables

        private readonly string _player = "Player";
        //private bool _isTriggered = false;

        #endregion

        #endregion

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag(_player)) return;

            if (IsTruePool.Equals(true))
            {
                manager.StopAsyncManager();
            }
            else
            {
                manager.StopAllCoroutineTrigger();
                manager.StartAsyncManager();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.CompareTag(_player)) return;
            if (IsTruePool.Equals(false))
            {
                if (IsTruePool.Equals(false))
                {
                    //manager.StopAllCoroutineTrigger();
                }
            }
        }
    }
}