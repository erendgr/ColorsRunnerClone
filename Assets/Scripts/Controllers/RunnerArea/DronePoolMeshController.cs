using System.Collections.Generic;
using System.Linq;
using Data.UnityObject;
using Datas.ValueObjects;
using DG.Tweening;
using Enums;
using UnityEngine;

namespace Controllers.RunnerArea
{
    public class DronePoolMeshController : MonoBehaviour
    {
        #region Self Variables

        #region Serializefield Variables

        [SerializeField] private List<MeshRenderer> colorBlocks;

        #endregion

        #region Private Variables

        private ColorData _colorData;
        private List<ColorEnum> _areaColorEnum;
        private ColorEnum _trueColorState;

        #endregion

        #endregion

        private void Awake()
        {
            _colorData = GetColorData();
        }

        private ColorData GetColorData() => Resources.Load<CD_Color>("Data/CD_Color").colorData;

        public void SetColors(List<ColorEnum> areaColorEnum)
        {
            _areaColorEnum = areaColorEnum;
            for (int i = 0; i < areaColorEnum.Count; i++)
            {
                colorBlocks[i].material.color = _colorData.color[(int)areaColorEnum[i]];
            }
        }

        public ColorEnum GetColor(Transform poolTransform)
        {
            return _areaColorEnum.Where((t, i) => colorBlocks[i].transform.Equals(poolTransform)).FirstOrDefault();
        }

        public void SetTrueColor(ColorEnum trueColor)
        {
            _trueColorState = trueColor;
        }

        public void PoolScaleReduction()
        {
            for (int i = 0; i < colorBlocks.Count; i++)
            {
                if (_areaColorEnum[i] != _trueColorState)
                {
                    colorBlocks[i].transform.DOScaleZ(0, 1f);
                }
            }
        }

        public Transform GetTruePool()
        {
            for (int i = 0; i < colorBlocks.Count; i++)
            {
                if (_areaColorEnum[i] == _trueColorState)
                {
                    return colorBlocks[i].transform;
                }
            }

            return colorBlocks[0].transform;
        }
    }
}