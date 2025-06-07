using System.Collections.Generic;
using Datas.ValueObjects;
using Signals;
using UnityEngine;

namespace Commands.Stack
{
    public class ItemAddOnStackCommand
    {
        #region Self Variables

        #region Private Variables

        private List<GameObject> _collectableStack;
        private Transform _transform;
        private StackData _stackData;

        #endregion

        #endregion

        public ItemAddOnStackCommand(ref List<GameObject> collectableStack, Transform transform, StackData stackData)
        {
            _collectableStack = collectableStack;
            _transform = transform;
            _stackData = stackData;
        }

        public void Execute(GameObject collectableGameObject)
        {
            if (_collectableStack.Count == 0)
            {
                _collectableStack.Add(collectableGameObject);
                collectableGameObject.transform.SetParent(_transform);
                collectableGameObject.transform.localPosition = Vector3.zero;
            }
            else
            {
                collectableGameObject.transform.SetParent(_transform);
                Vector3 newPos = _collectableStack[_collectableStack.Count - 1].transform.localPosition;
                newPos.z -= _stackData.CollectableOffsetInStack;
                collectableGameObject.transform.localPosition = newPos;
                _collectableStack.Add(collectableGameObject);
            }

            ScoreSignals.Instance.onSetScore?.Invoke(_collectableStack.Count);
        }
    }
}