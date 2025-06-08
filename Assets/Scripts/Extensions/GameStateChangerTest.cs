using Signals;
using UnityEngine;

namespace Extensions
{
    public class GameStateChangerTest : MonoBehaviour
    {
        #region Event Subscriptions

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onPlay += OnChangeGameState;
        }
        
        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onPlay -= OnChangeGameState;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        private void OnChangeGameState()
        {
            CoreGameSignals.Instance.onChangeGameState?.Invoke();
        }
    }
}