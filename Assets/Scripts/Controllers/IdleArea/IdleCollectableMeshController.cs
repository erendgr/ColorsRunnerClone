using Data.UnityObject;
using Datas.ValueObjects;
using Enums;
using Managers;
using UnityEngine;

namespace Controllers.IdleArea
{
    public class IdleCollectableMeshController : MonoBehaviour
    {
        #region Self Variables

        #region Serializefield Variables

        [SerializeField] private IdleCollectableManager manager;
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
    }
}