using Managers;
using UnityEngine;

namespace Controllers.IdleArea
{
    public class IdlePhysicsController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private IdleAreaManager manager;

        #endregion

        #endregion

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                manager.ScoreAdd(true);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                manager.ScoreAdd(false);
            }
        }
    }
}