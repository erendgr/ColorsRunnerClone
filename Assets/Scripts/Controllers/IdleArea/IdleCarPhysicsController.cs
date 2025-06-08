using Managers;
using UnityEngine;

namespace Controllers.IdleArea
{
    public class IdleCarPhysicsController : MonoBehaviour
    {
        #region Self Variables

        #region Private Variables

        private IdleCarManager _manager;
        private bool _isOnTargetTrigger;

        #endregion

        #endregion
        
        private void Awake()
        {
            _manager = transform.parent.GetComponent<IdleCarManager>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Target"))
            {
                _isOnTargetTrigger = true;
                _manager.SelectRandomDirection(other.GetComponent<IdleCarTargetController>().GetData(),
                    other.transform);
                return;
            }

            if (other.CompareTag("Player"))
            {
                _manager.PlayerOnRoad();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Target"))
            {
                _isOnTargetTrigger = false;
                return;
            }

            if (other.CompareTag("Player"))
            {
                //_manager.Move();
                _manager.MoveAfterPlayer(_isOnTargetTrigger);
            }
        }
    }
}