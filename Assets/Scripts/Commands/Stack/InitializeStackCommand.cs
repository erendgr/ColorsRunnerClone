using Managers;
using Signals;
using UnityEngine;

namespace Commands.Stack
{
    public class InitializeStackCommand
    {
        #region Self Variables

        #region Private Variables

        private StackManager _manager;
        private GameObject _collectable;

        #endregion

        #endregion

        public InitializeStackCommand(GameObject collectable, StackManager manager)
        {
            _collectable = collectable;
            _manager = manager;
        }

        public void Execute(int count)
        {
            for (int i = 0; i < count /*_manager.StackData.InitialStackItem*/; i++)
            {
                GameObject obj = Object.Instantiate(_collectable);
                _manager.ItemAddOnStack.Execute(obj);
                if (i == 0)
                {
                    ScoreSignals.Instance.onSetLeadPosition?.Invoke(obj);
                }
            }

            ScoreSignals.Instance.onSetScore?.Invoke(count);
            // _manager.StackValueUpdateCommand.Execute();
        }
    }
}