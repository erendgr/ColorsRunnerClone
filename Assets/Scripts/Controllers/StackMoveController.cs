using System.Collections.Generic;
using Data.ValueObject;
using Datas.ValueObjects;
using UnityEngine;

namespace Controllers
{
    public class StackMoveController
    {
        #region Self Variables

        #region Private Veriables

        private StackData _stackData;
        #endregion
        #endregion

        public void InitializedController(StackData stackData)
        {
            _stackData = stackData;
        }

        public void StackItemsMoveOrigin(Vector3 direction,List<GameObject> collectableStack, bool _isOnDronePool = false)
        {
            if (collectableStack.Count <= 0)
            {
                return;
            }
            
            float directX = Mathf.Lerp(collectableStack[0].transform.localPosition.x, direction.x,_stackData.LerpSpeed_x);
            float directY = Mathf.Lerp(collectableStack[0].transform.localPosition.y, direction.y,_stackData.LerpSpeed_y);
            float directZ = Mathf.Lerp(collectableStack[0].transform.localPosition.z, direction.z + _stackData.DistanceFormPlayer ,_stackData.LerpSpeed_z);
            
            if (_isOnDronePool == true)
            {
                collectableStack[0].transform.localPosition = new Vector3(directX, 
                    collectableStack[0].transform.position.y, collectableStack[0].transform.position.z);
                StackItemsLerpMoveOnDronePool(collectableStack);
            }
            else
            {
                collectableStack[0].transform.localPosition = new Vector3(directX, directY, directZ);
                collectableStack[0].transform.LookAt(direction);
                StackItemsLerpMove(collectableStack);
            }
        }

        private void StackItemsLerpMove(List<GameObject> collectableStack)
        {
            for (int i = 1; i < collectableStack.Count; i++)
            {
                Vector3 pos = collectableStack[i - 1].transform.localPosition;
                pos.z = collectableStack[i - 1].transform.localPosition.z - _stackData.CollectableOffsetInStack;
                collectableStack[i].transform.localPosition = new Vector3(
                    Mathf.Lerp(collectableStack[i].transform.localPosition.x, pos.x, _stackData.LerpSpeed_x),
                    Mathf.Lerp(collectableStack[i].transform.localPosition.y, pos.y, _stackData.LerpSpeed_y),
                    Mathf.Lerp(collectableStack[i].transform.localPosition.z, pos.z, _stackData.LerpSpeed_z));
                collectableStack[i].transform.LookAt(collectableStack[i-1].transform);
            }
        }

        private void StackItemsLerpMoveOnDronePool(List<GameObject> collectableStack)
        {
            for (int i = 1; i < collectableStack.Count; i++)
            {
                Vector3 pos = collectableStack[i - 1].transform.localPosition;
                pos.z = collectableStack[i - 1].transform.localPosition.z - _stackData.CollectableOffsetInStack;
                collectableStack[i].transform.localPosition = new Vector3(
                    Mathf.Lerp(collectableStack[i].transform.localPosition.x, pos.x, _stackData.LerpSpeed_x),
                    Mathf.Lerp(collectableStack[i].transform.localPosition.y, pos.y, _stackData.LerpSpeed_y),
                    Mathf.Lerp(collectableStack[i].transform.localPosition.z, pos.z, _stackData.LerpSpeed_z));
            }
        }
    }
}