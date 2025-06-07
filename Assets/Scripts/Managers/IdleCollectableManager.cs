using Controllers.IdleArea;
using Datas.ValueObjects;
using Enums;
using UnityEngine;

namespace Managers
{
    public class IdleCollectableManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        public ColorEnum ColorState
        {
            get => colorState;
            private set
            {
                colorState = value;
                collectableMeshController.ColorSet();
            }
        }

        #endregion

        #region SerializeField Variables

        [SerializeField] private IdleCollectableMeshController collectableMeshController;
        [SerializeField] private CollectableAnimationController animationController;
        [SerializeField] private ColorEnum colorState;
        [SerializeField] private CollectableAnimStates initialAnimState = CollectableAnimStates.Clap;

        #endregion

        #region Private Variables

        private ColorData _colorData;
        private ColorEnum _poolColorEnum;

        #endregion

        #endregion

        // private void OnEnable()
        // {
        //    // initialAnimState = CollectableAnimStates.Clap;
        //     SetReferences();
        // }

        private void OnEnable()
        {
            SetCollectableAnimation(initialAnimState);
        }

        private void Start()
        {
            SetReferences();
        }

        private void SetReferences()
        {
            ColorState = colorState;
        }

        private void SetCollectableAnimation(CollectableAnimStates newAnimState)
        {
            animationController.SetAnimState(newAnimState);
        }
    }
}