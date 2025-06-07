﻿using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace Commands.Stack
{
    public class StackItemBorder
    {
        #region Self Variables

        #region Private Variables

        [CanBeNull] private List<GameObject> _unstack;

        #endregion

        #endregion

        public StackItemBorder(ref List<GameObject> unStack)
        {
            _unstack = unStack;
        }

        public void Execute(bool isOutlineOpen)
        {
            if (_unstack != null)
                foreach (var t in _unstack)
                {
                    t.GetComponent<CollectableManager>().OutLineBorder(isOutlineOpen);
                }
        }
    }
}