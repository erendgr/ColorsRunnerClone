using Data.UnityObject;
using Datas.ValueObjects;
using Enums;
using Signals;
using UnityEngine;

namespace Controllers.RunnerArea
{
    public class ColorGateController : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        private ColorEnum ColorEnum
        {
            get => colorEnum;
            set
            {
                this.colorEnum = value;
                SetColor();
            }
        }

        #endregion

        #region SerializeField Variables

        [SerializeField] private ColorEnum colorEnum;

        #endregion

        #region Private Variables

        private ColorData _colorData;

        #endregion

        #endregion

        private void Awake()
        {
            _colorData = GetColorData();
        }

        private void Start()
        {
            ColorEnum = colorEnum;
        }

        private ColorData GetColorData() => Resources.Load<CD_Color>("Data/CD_Color").colorData;

        private void SetColor()
        {
            if (ColorEnum == ColorEnum.Rainbow)
            {
                gameObject.GetComponent<Renderer>().material = _colorData.RainbowGageMaterial;
            }
            else gameObject.GetComponent<Renderer>().material.color = _colorData.color[(int)ColorEnum];
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                StackSignals.Instance.ColorType?.Invoke(ColorEnum);
            }
        }
    }
}