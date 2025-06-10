using Managers;
using Signals;
using UnityEngine;

namespace Controllers.Player
{
    public class PlayerPhysicsController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private PlayerManager manager;
        [SerializeField] private ParticleSystem particle;
        [SerializeField] private ParticleSystem currentParticle;

        #endregion

        #region Private Variables

        private readonly string _dronePool = "DronePool";
        private readonly string _dronePoolReset = "DronePoolReset";
        private readonly string _dronePoolExit = "DronePoolExit";
        private readonly string _finish = "Finish";
        private readonly string _gunPoolExit = "GunPoolExit";
        private readonly string _gunPool = "GunPool";
        private readonly string _citizen = "Citizen";
        private readonly string _collectableIdle = "CollectableIdle";

        #endregion

        #endregion

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(_dronePool))
            {
                DronePoolSignals.Instance.onPlayerCollideWithDronePool?.Invoke(other.transform);
                ScoreSignals.Instance.onVisibleScore?.Invoke(false);
                return;
            }

            if (other.CompareTag(_finish))
            {
                LevelSignals.Instance.onLevelSuccessful?.Invoke();
                other.gameObject.SetActive(false);
                return;
            }

            if (other.CompareTag(_dronePoolReset))
            {
                DronePoolSignals.Instance.onDronePoolExit?.Invoke();
                return;
            }

            if (other.CompareTag(_gunPoolExit))
            {
                manager.Data.MovementData.ForwardSpeed = manager.Data.MovementData.RunSpeed;
                GunPoolSignals.Instance.onGunPoolExit?.Invoke();
                return;
            }

            if (other.CompareTag(_dronePoolExit))
            {
                ScoreSignals.Instance.onVisibleScore?.Invoke(true);
                return;
            }

            if (other.CompareTag(_gunPool))
            {
                manager.Data.MovementData.ForwardSpeed = manager.Data.MovementData.CrouchSpeed;
                return;
            }

            if (other.CompareTag(_citizen))
            {
                //ScoreSignals.Instance.onUpdateScore?.Invoke(1);
                //int currentScore = ScoreSignals.Instance.onGetIdleScore();
                ScoreSignals.Instance.onSetScore?.Invoke(1);
                return;
            }

            if (other.CompareTag(_collectableIdle))
            {
                ScoreSignals.Instance.onSetScore?.Invoke(1);
                other.transform.parent.gameObject.SetActive(false);
                IdleSignals.Instance.onIdleCollectableValue(-1);
            }
        }
    }
}