using Data.UnityObject;
using Datas.ValueObjects;
using UnityEngine;

namespace Controllers.Player
{
    public class PlayerMeshController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private SkinnedMeshRenderer skinnedMeshRenderer;

        #endregion
        
        #region Private Variables
        private ColorData _colorData;

        #endregion
        
        #endregion
        private void Awake()
        {
            _colorData = GetColorData();
            ColorSet();
        }

        private ColorData GetColorData() => Resources.Load<CD_Color>("Data/CD_Color").colorData;
        public void ShowSkinnedMesh()
        {
            skinnedMeshRenderer.enabled = true;
        }

        private void ColorSet()
        {
            
            skinnedMeshRenderer.material = _colorData.RainbowMaterial;
        
        }
    }
}