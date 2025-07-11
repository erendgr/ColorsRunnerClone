﻿using Commands;
using Enums;
using Signals;
using TMPro;
using UnityEngine;

namespace Managers
{
    public class ScoreManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private GameObject stackGO;
        [SerializeField] private TextMeshPro /*spriteTMP,*/ scoreTMP;
        [SerializeField] private GameObject textPlane;

        #endregion

        #region Private Variables

        private int _score, _idleScore,_idleOldScore;
        private GameObject _playerGO;
        private GameStates _currentState = GameStates.Runner;
        private SetScoreCommand _setScoreCommand;
        private SetVisibilityOfScore _setVisibilityOfScore;
        private GameObject _parentGO;
        private bool _isActive;
        private int _savedScore;

        #endregion

        #endregion

        private void Awake()
        {
            _savedScore = GetActiveLevel();
            Init();
        }
        
        private void Init()
        {
            _setScoreCommand = new SetScoreCommand(ref _score);
            _setVisibilityOfScore = new SetVisibilityOfScore(ref scoreTMP, /*ref spriteTMP,*/ ref textPlane);
        }
        
        #region Event Subscriptions

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onChangeGameState += OnChangeGameState;
            ScoreSignals.Instance.onSetScore += OnUpdateScore;
            ScoreSignals.Instance.onVisibleScore += _setVisibilityOfScore.Execute;
            CoreGameSignals.Instance.onPlay += OnPlay;
            ScoreSignals.Instance.onSetLeadPosition += OnSetLead;
            LevelSignals.Instance.onRestartLevel += OnReset;
            LevelSignals.Instance.onLevelSuccessful += OnLevelSuccessful;
            ScoreSignals.Instance.onGetIdleScore += OnGetCurrentScore;
            LevelSignals.Instance.onNextLevel += OnNextLevel;
        }

        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onChangeGameState -= OnChangeGameState;
            ScoreSignals.Instance.onSetScore -= OnUpdateScore;
            ScoreSignals.Instance.onVisibleScore -= _setVisibilityOfScore.Execute;
            CoreGameSignals.Instance.onPlay -= OnPlay;
            ScoreSignals.Instance.onSetLeadPosition -= OnSetLead;
            LevelSignals.Instance.onRestartLevel -= OnReset;
            LevelSignals.Instance.onLevelSuccessful -= OnLevelSuccessful;
            ScoreSignals.Instance.onGetIdleScore -= OnGetCurrentScore;
            LevelSignals.Instance.onNextLevel -= OnNextLevel;

        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion
        
        private void Update()
        {
            SetScoreManagerRotation();
            if (_currentState == GameStates.Runner && _isActive)
            {
                SetScoreManagerPosition();
            }
        }
        
        #region Event Methods

        private void OnPlay()
        {
            FindPlayerGameObject();
        }

        private void OnChangeGameState()
        {
            _currentState = GameStates.Idle;
            var transform1 = transform;
            transform1.parent = _playerGO.transform;
            transform1.localPosition = new Vector3(0, 2f, 0);
            _setVisibilityOfScore.Execute(true);
            ScoreSignals.Instance.onSetScore?.Invoke(_savedScore);
        }
        
        private void OnSetLead(GameObject gO)
        {
            Debug.Log(gO.name);
            _parentGO = gO;
        }
        
        private void OnReset()
        {
            _isActive = false;
        }

        private void OnLevelSuccessful()
        {
            _savedScore = GetActiveLevel();
            ScoreSignals.Instance.onGetScore?.Invoke(_currentState == GameStates.Runner ? _idleOldScore : _idleScore);
            _setVisibilityOfScore.Execute(false);
        }

        private void OnUpdateScore(int score)
        {
            if (_currentState == GameStates.Runner)
            {
                _setScoreCommand.Execute(score);
                _idleOldScore = score;
            }
            else
            {
                if (_idleOldScore < 0) return;
                _idleScore = _idleOldScore + score;
                _setScoreCommand.Execute(_idleScore);
                _idleOldScore = _idleScore;
                //StackSignals.Instance.onSetPlayerScale?.Invoke(-.1f);
            }
        }
        
        private int GetActiveLevel()
        {
            if (!ES3.FileExists()) return 0;
            return ES3.KeyExists("Collectable") ? ES3.Load<int>("Collectable") : 0;
        }
        
        private int OnGetCurrentScore()
        {
            return _idleScore;
        }

        private void OnNextLevel()
        {
            Transform transform1;
            (transform1 = transform).SetParent(null);
            transform1.localScale = Vector3.one;
            _currentState = GameStates.Runner;
        }

        #endregion
        
        #region Methods

        private void SetScoreManagerPosition()
        {
            transform.position = _parentGO.transform.position + new Vector3(0, 2f, 0);
        }

        private void SetScoreManagerRotation()
        {
            transform.rotation = Quaternion.Euler(0, 0, transform.rotation.z * -1f);
        }

        private void FindPlayerGameObject()
        {
            _playerGO = GameObject.FindGameObjectWithTag("Player");
            _isActive = true;
        }
        
        #endregion
    }
}