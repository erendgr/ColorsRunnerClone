﻿using Data.UnityObject;
using Datas.ValueObjects;
using DG.Tweening;
using Enums;
using Managers;
using Signals;
using UnityEngine;

namespace Controllers.Collectables
{
    public class CollectableMeshController : MonoBehaviour
    {
        #region Self Variables

        #region Serializefield Variables

        [SerializeField] private CollectableManager manager;
        [SerializeField] private SkinnedMeshRenderer mesh;

        #endregion

        #region Private Variables

        private ColorEnum _otherColorState;
        private ColorData _colorData;

        #endregion

        #endregion

        private void Awake()
        {
            _colorData = GetColorData();
        }

        private ColorData GetColorData() => Resources.Load<CD_Color>("Data/CD_Color").colorData;

        public void ColorControl(GameObject otherGameObject)
        {
            CollectableManager cm = otherGameObject.transform.parent.gameObject.GetComponent<CollectableManager>();
            _otherColorState = cm.ColorState;

            if (manager.ColorState == _otherColorState)
            {
                otherGameObject.tag = "Collected";
                cm.SetCollectableAnimation(CollectableAnimStates.Run);
                StackSignals.Instance.onInteractionCollectable?.Invoke(otherGameObject.transform.parent.gameObject);
            }
            else
            {
                otherGameObject.transform.parent.gameObject.SetActive(false);
                StackSignals.Instance.onInteractionObstacle?.Invoke(manager.gameObject);
            }
        }

        public void ColorSet()
        {
            if (manager.ColorState == ColorEnum.Rainbow)
            {
                mesh.material = _colorData.RainbowMaterial;
            }
            else
            {
                mesh.material = _colorData.ColorMaterial;
                mesh.material.color = _colorData.color[(int)manager.ColorState];
            }
        }

        public void SetOutlineBorder(bool isOutlineOn)
        {
            mesh.material.DOFloat(isOutlineOn ? 0f : 100f, "_OutlineSize", 1f);
        }
    }
}