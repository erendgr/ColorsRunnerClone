using System.Collections.Generic;
using Keys;
using Signals;
using Unity.Mathematics;
using UnityEngine;

namespace Managers
{
    public class IdleCollectableAreaManager : MonoBehaviour
    {
        #region Self Variables

        #region SerializeField Variables

        [SerializeField] private List<GameObject> collectableSpawnPoint = new List<GameObject>();
        [SerializeField] private GameObject collectable;

        #endregion

        #region Private Variables

        private int _collectableScore;
        private List<GameObject> _collectablePool = new();

        #endregion

        #endregion

        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            SaveSignals.Instance.onLoadIdleGame += LoadIdleDatas;
            IdleSignals.Instance.onColectableScore += OnGetIdleSaveDatas;
            //  SaveSignals.Instance.onSaveIdleParams += OnGetIdleSaveDatas;
            IdleSignals.Instance.onIdleCollectableValue += OnCollectableValue;
            IdleSignals.Instance.onCollectableAreaNextLevel += OnNextLevel;
        }

        private void UnsubscribeEvents()
        {
            SaveSignals.Instance.onLoadIdleGame -= LoadIdleDatas;
            IdleSignals.Instance.onColectableScore -= OnGetIdleSaveDatas;
            IdleSignals.Instance.onIdleCollectableValue -= OnCollectableValue;
            //SaveSignals.Instance.onSaveIdleParams -= OnGetIdleSaveDatas;
            IdleSignals.Instance.onCollectableAreaNextLevel -= OnNextLevel;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        private void Awake()
        {
            _collectableScore = 0;
            for (int i = 0; i < collectableSpawnPoint.Count; i++)
            {
                GameObject collectableCache = Instantiate(collectable, collectableSpawnPoint[i].transform.position,
                    quaternion.identity, transform);
                collectableCache.SetActive(false);
                _collectablePool.Add(collectableCache);
            }
        }

        private void Start()
        {
            SaveSignals.Instance.onLoadIdle?.Invoke();
            CollectableStartOpen(_collectableScore);
        }

        private void LoadIdleDatas(SaveIdleDataParams saveIdleDataParams)
        {
            _collectableScore = saveIdleDataParams.CollectablesCount;
        }

        private void OnCollectableValue(int collectableCount)
        {
            if (_collectableScore + collectableCount <= _collectablePool.Count &&
                _collectableScore + collectableCount >= 0)
            {
                _collectableScore += collectableCount;
            }
            else if (_collectableScore + collectableCount > _collectablePool.Count)
            {
                _collectableScore = _collectablePool.Count;
            }
            else if (_collectableScore + collectableCount < 0)
            {
                _collectableScore = 0;
            }
        }

        private void CollectableStartOpen(int collectableCount)
        {
            foreach (var t in _collectablePool)
            {
                t.SetActive(false);
            }

            for (int i = 0; i < collectableCount; i++)
            {
                _collectablePool[i].SetActive(true);
            }
        }

        private int OnGetIdleSaveDatas()
        {
            return _collectableScore;
        }

        private void OnNextLevel()
        {
            CollectableStartOpen(_collectableScore);
        }
    }
}