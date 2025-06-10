using System.Collections.Generic;
using Signals;
using UnityEngine;

namespace Commands.Stack
{
    public class ItemRemoveOnStackCommand
    {
        #region Self Variables

        #region Private Variables

        private List<GameObject> _collectableStack;
        private GameObject _levelHolder;

        #endregion

        #endregion

        public ItemRemoveOnStackCommand(ref List<GameObject> collectableStack, ref GameObject levelHolder)
        {
            _collectableStack = collectableStack;
            _levelHolder = levelHolder;
        }

        public void Execute(GameObject collectableGameObject)
        {
            if (!_collectableStack.Contains(collectableGameObject)) return;

            int index = _collectableStack.IndexOf(collectableGameObject);
            collectableGameObject.transform.SetParent(_levelHolder.transform.GetChild(0));
            collectableGameObject.SetActive(false);
            
            _collectableStack.RemoveAt(index);
            _collectableStack.TrimExcess();
            
            if (DronePoolSignals.Instance.onGetStackCount() <= 0)
            {
                LevelSignals.Instance.onLevelFailed?.Invoke();
                return;
            }

            if (index == 0)
            {
                ScoreSignals.Instance.onSetLeadPosition?.Invoke(_collectableStack[0]);
            }

            ScoreSignals.Instance.onSetScore?.Invoke(_collectableStack.Count);
        }
    }
}