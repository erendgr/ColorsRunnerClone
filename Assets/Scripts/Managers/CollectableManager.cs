﻿using System.Collections;
using Controllers.Collectables;
using Controllers.IdleArea;
using Datas.UnityObjects;
using Datas.ValueObjects;
using Enums;
using Signals;
using UnityEngine;

namespace Managers
{
    public class CollectableManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        public ColorEnum ColorState
        {
            get => colorState;
            set
            {
                colorState = value;
                collectableMeshController.ColorSet();
            }
        }

        #endregion

        #region SerializeField Variables

        [SerializeField] private CollectablePhysicController physicController;
        [SerializeField] private CollectableMeshController collectableMeshController;
        [SerializeField] private CollectableAnimationController animationController;
        [SerializeField] private ColorEnum colorState;
        [SerializeField] private CollectableAnimStates initialAnimState;

        #endregion

        #region Private Variables

        private CollectableData _data;
        private ColorData _colorData;
        private ColorEnum _poolColorEnum;

        #endregion

        #endregion

        private void Awake()
        {
            _data = GetCollectableData();
        }

        private CollectableData GetCollectableData() => Resources.Load<CD_Collectable>("Data/CD_Collectable").Data;

        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            DronePoolSignals.Instance.onDroneArrives += OnDroneArrives;
            DronePoolSignals.Instance.onDronePoolExit += physicController.CanEnterDronePool;
            CoreGameSignals.Instance.onPlay += OnPlay;
            DronePoolSignals.Instance.onDroneGone += OnDroneGone;
        }

        private void UnsubscribeEvents()
        {
            DronePoolSignals.Instance.onDroneArrives -= OnDroneArrives;
            DronePoolSignals.Instance.onDronePoolExit -= physicController.CanEnterDronePool;
            CoreGameSignals.Instance.onPlay -= OnPlay;
            DronePoolSignals.Instance.onDroneGone -= OnDroneGone;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        private void Start()
        {
            SetReferences();
        }

        private void SetReferences()
        {
            ColorState = colorState;
            SetCollectableAnimation(initialAnimState);
        }

        public void InteractionWithCollectable(GameObject collectableGameObject)
        {
            collectableMeshController.ColorControl(collectableGameObject);
        }

        public void InteractionWithObstacle(GameObject collectableGameObject)
        {
            StackSignals.Instance.onInteractionObstacle?.Invoke(collectableGameObject);
        }

        private void OnPlay()
        {
            initialAnimState = CollectableAnimStates.Run;

            SetCollectableAnimation(CompareTag("Collectable")
                ? CollectableAnimStates.Idle
                : CollectableAnimStates.Run);
        }

        public IEnumerator CrouchAnim()
        {
            yield return new WaitForSeconds(1f);
            SetCollectableAnimation(CollectableAnimStates.Crouch);
        }

        private void OnDroneArrives(Transform poolTransform)
        {
            if (!CompareTag("Collected")) return;
            if (_poolColorEnum.Equals(ColorState)) return;
            DronePoolSignals.Instance.onWrongDronePool?.Invoke(gameObject);
            SetCollectableAnimation(CollectableAnimStates.Die);
            StartCoroutine(SetActiveFalse());
        }

        public void CollectableOnGunPool()
        {
            initialAnimState = CollectableAnimStates.CrouchedWalk;
            SetCollectableAnimation(initialAnimState);
        }

        public void CollectableOnExitGunPool()
        {
            if (!CompareTag("Collected")) return;
            initialAnimState = CollectableAnimStates.Run;
            SetCollectableAnimation(initialAnimState);
        }

        public void SetPoolColor(ColorEnum poolColorEnum)
        {
            _poolColorEnum = poolColorEnum;
        }

        public void OutLineBorder(bool isOutlineOn)
        {
            collectableMeshController.SetOutlineBorder(isOutlineOn);
        }

        private void OnDroneGone()
        {
            if (!CompareTag("Collected")) return;
            initialAnimState = CollectableAnimStates.Run;
            SetCollectableAnimation(initialAnimState);
        }

        private IEnumerator SetActiveFalse()
        {
            yield return new WaitForSeconds(3f);
            gameObject.SetActive(false);
        }

        public void SetCollectableAnimation(CollectableAnimStates newAnimState)
        {
            animationController.SetAnimState(newAnimState);
        }
    }
}