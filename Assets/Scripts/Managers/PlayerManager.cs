using Commands.Player;
using Controllers.Player;
using Data.ValueObject;
using Datas.UnityObjects;
using Enums;
using UnityEngine;
using Keys;
using Signals;

namespace Managers
{
    public class PlayerManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        public PlayerData Data;

        #endregion

        #region Serialized Variables

        [SerializeField] private PlayerMovementController movementController;
        [SerializeField] private PlayerAnimationController animationController;
        [SerializeField] private PlayerMeshController meshController;

        #endregion

        #region Private Variables

        private JumpCommand _jumpCommand;
        private SetPlayerPositionAfterDronePool _setPlayerPositionAfterDronePool;
        private Rigidbody _rb;
        private PlayerParticleController _particleController;

        #endregion

        #endregion

        private void Awake()
        {
            GetReferences();
            Init();
            SetStackPosition();
            SendPlayerDataToControllers();
            animationController.SetAnimState(CollectableAnimStates.Idle);
        }

        private PlayerData GetPlayerData() => Resources.Load<CD_Player>("Data/CD_Player").Data;

        private void Init()
        {
            var transform1 = transform;
            _jumpCommand = new JumpCommand(ref Data, transform1);
            _setPlayerPositionAfterDronePool = new SetPlayerPositionAfterDronePool(transform1);
            _rb = GetComponent<Rigidbody>();
            _particleController = GetComponent<PlayerParticleController>();
        }

        private void GetReferences()
        {
            Data = GetPlayerData();
        }

        private void SendPlayerDataToControllers()
        {
            movementController.SetMovementData(Data.MovementData);
        }

        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            InputSignals.Instance.onInputTaken += OnActivateMovement;
            InputSignals.Instance.onInputReleased += OnDeactiveMovement;
            InputSignals.Instance.onRunnerInputDragged += OnSetRunnerInputValues;
            InputSignals.Instance.onJoystickDragged += OnSetIdleInputValues;
            CoreGameSignals.Instance.onPlay += OnPlay;
            CoreGameSignals.Instance.onReset += OnReset;
            CoreGameSignals.Instance.onChangeGameState += OnChangeMovementState;
            LevelSignals.Instance.onLevelSuccessful += OnLevelSuccessful;
            LevelSignals.Instance.onLevelFailed += OnLevelFailed;
            StackSignals.Instance.onBoostArea += _jumpCommand.Execute;
            DronePoolSignals.Instance.onPlayerCollideWithDronePool += movementController.DeactiveForwardMovement;
            DronePoolSignals.Instance.onDroneGone += movementController.UnDeactiveForwardMovement;
            DronePoolSignals.Instance.onPlayerGotoTruePool += _setPlayerPositionAfterDronePool.Execute;
            StackSignals.Instance.onSetPlayerScale += OnSetPlayerScale;
            IdleSignals.Instance.onInteractionBuild += OnInteractionBuyPoint;
        }

        private void UnsubscribeEvents()
        {
            InputSignals.Instance.onInputTaken -= OnActivateMovement;
            InputSignals.Instance.onInputReleased -= OnDeactiveMovement;
            InputSignals.Instance.onRunnerInputDragged -= OnSetRunnerInputValues;
            InputSignals.Instance.onJoystickDragged -= OnSetIdleInputValues;
            CoreGameSignals.Instance.onPlay -= OnPlay;
            CoreGameSignals.Instance.onReset -= OnReset;
            CoreGameSignals.Instance.onChangeGameState -= OnChangeMovementState;
            LevelSignals.Instance.onLevelSuccessful -= OnLevelSuccessful;
            LevelSignals.Instance.onLevelFailed -= OnLevelFailed;
            StackSignals.Instance.onBoostArea -= _jumpCommand.Execute;
            DronePoolSignals.Instance.onPlayerCollideWithDronePool -= movementController.DeactiveForwardMovement;
            DronePoolSignals.Instance.onDroneGone -= movementController.UnDeactiveForwardMovement;
            DronePoolSignals.Instance.onPlayerGotoTruePool -= _setPlayerPositionAfterDronePool.Execute;
            StackSignals.Instance.onSetPlayerScale -= OnSetPlayerScale;
            IdleSignals.Instance.onInteractionBuild -= OnInteractionBuyPoint;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        #region Event Methods

        #region Movement Controller

        private void OnActivateMovement()
        {
            movementController.EnableMovement();
        }

        private void OnDeactiveMovement()
        {
            movementController.DeactiveMovement();
        }

        private void OnSetRunnerInputValues(RunnerInputParams inputParams)
        {
            movementController.UpdateRunnerInputValue(inputParams);
        }

        private void OnSetIdleInputValues(IdleInputParams inputParams)
        {
            movementController.UpdateIdleInputValue(inputParams);
            animationController.SetSpeedVariable(inputParams);
        }

        private void OnChangeMovementState()
        {
            movementController.IsReadyToPlay(true);
            movementController.ChangeMovementState();
            movementController.EnableMovement();
            _rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationX |
                              RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
        }

        #endregion

        #region Others

        private void OnPlay()
        {
            SetStackPosition();
            movementController.IsReadyToPlay(true);
            animationController.SetAnimState(CollectableAnimStates.Run);
        }

        private void OnLevelSuccessful()
        {
            movementController.IsReadyToPlay(false);
            animationController.SetAnimState(CollectableAnimStates.Idle);
            meshController.ShowSkinnedMesh();
        }

        private void OnLevelFailed()
        {
            movementController.IsReadyToPlay(false);
        }

        private void OnSetPlayerScale(float value)
        {
            animationController.SetPlayerScale(value);
        }

        private void OnReset()
        {
            gameObject.SetActive(true);
            movementController.OnReset();
            SetStackPosition();
        }

        private void OnInteractionBuyPoint(bool isInteractionBuyPoint, Transform targetTransform)
        {
            if (isInteractionBuyPoint)
            {
                SetAnim(CollectableAnimStates.Buy);
                ParticuleState(true, targetTransform);
            }
            else
            {
                SetAnim(CollectableAnimStates.Run);
                ParticuleState(false);
            }
        }

        #endregion

        #endregion

        #region Methods

        private void SetStackPosition()
        {
            StackSignals.Instance.onPlayerGameObject?.Invoke(gameObject);
        }

        private void SetAnim(CollectableAnimStates animState)
        {
            animationController.SetAnimState(animState);
        }

        private void ParticuleState(bool active, Transform instantiateTransform = null)
        {
            if (active)
            {
                _particleController.StartParticle(instantiateTransform);
            }
            else
            {
                _particleController.StopParticle();
            }
        }

        #endregion
    }
}