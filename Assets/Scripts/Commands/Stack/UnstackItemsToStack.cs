using System.Collections.Generic;
using Commands.Stack;
using UnityEngine;
using Signals;

namespace Commands
{
    public class UnstackItemsToStack
    {
        #region Self Variables

        #region Private Variables

        private List<GameObject> _collectableStack;
        private List<GameObject> _unStackList;
        private DuplicateStateItemsCommand _duplicateStateItemsCommand;
        private GameObject _stackManager;

        #endregion

        #endregion

        public UnstackItemsToStack(ref List<GameObject> collectableStack, ref List<GameObject> unStackList,
            ref DuplicateStateItemsCommand duplicateStateItemsCommand, GameObject stackManager)
        {
            _collectableStack = collectableStack;
            _unStackList = unStackList;
            _duplicateStateItemsCommand = duplicateStateItemsCommand;
            _stackManager = stackManager;
        }

        public void Execute()
        {
            foreach (var i in _unStackList)
            {
                i.transform.SetParent(_stackManager.transform);
                _collectableStack.Add(i);
            }

            if (DronePoolSignals.Instance.onGetStackCount() <= 0)
            {
                LevelSignals.Instance.onLevelFailed?.Invoke();
                return;
            }

            _unStackList.Clear();
            _duplicateStateItemsCommand.Execute();
            ScoreSignals.Instance.onSetScore?.Invoke(_collectableStack.Count);
            ScoreSignals.Instance.onSetLeadPosition?.Invoke(_collectableStack[0]);
        }
    }
}