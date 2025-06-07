using System.Collections.Generic;
using System.Linq;
using Datas.UnityObjects;
using Datas.ValueObjects;
using Enums;
using Keys;
using Signals;
using UnityEngine;

namespace Managers
{
    public class IdleManager : MonoBehaviour
    {
        #region Self Variables

        #region Private Variables

        private int _idleLevel;
        private LevelData _levelsData;
        private List<GameObject> _mainAreas = new List<GameObject>();
        private List<List<GameObject>> _sideAreas = new List<List<GameObject>>();
        private List<int> _mainCurrentScore = new List<int>();
        private List<int> _sideCurrentScore = new List<int>();
        private List<BuildingState> _mainBuildingState = new List<BuildingState>();
        private List<BuildingState> _sideBuildingState = new List<BuildingState>();
        private SaveIdleDataParams _saveIdleDataParams;
        private int _sideCache;
        private int _completeSides;
        private bool _isMainSide;

        #endregion

        #endregion

        private void Awake()
        {
            _sideCache = 0;
            _levelsData = GetIdleLevelBuildingData();
        }

        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            SaveSignals.Instance.onSaveIdleParams += OnGetIdleSaveDatas;
            SaveSignals.Instance.onLoadIdleGame += LoadIdleDatas;
            LevelSignals.Instance.onNextLevel += OnNextLevel;
            IdleSignals.Instance.onMainSideComplete += OnMainSideComplete;
            IdleSignals.Instance.onMainSideObjects += OnMainSideSetReferences;
            IdleSignals.Instance.onSideObjects += OnSideSetReferences;
        }

        private void UnsubscribeEvents()
        {
            SaveSignals.Instance.onSaveIdleParams -= OnGetIdleSaveDatas;
            SaveSignals.Instance.onLoadIdleGame -= LoadIdleDatas;
            LevelSignals.Instance.onNextLevel -= OnNextLevel;
            IdleSignals.Instance.onMainSideComplete -= OnMainSideComplete;
            IdleSignals.Instance.onMainSideObjects -= OnMainSideSetReferences;
            IdleSignals.Instance.onSideObjects -= OnSideSetReferences;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        private void Start()
        {
            Init();
        }

        private void Init()
        {
            SaveSignals.Instance.onLoadIdle?.Invoke();
            EmptyListCheck();
        }
        
        private void LoadIdleDatas(SaveIdleDataParams saveIdleDataParams)
        {
            _saveIdleDataParams = saveIdleDataParams;
            _mainCurrentScore = _saveIdleDataParams.MainCurrentScore;
            _sideCurrentScore = _saveIdleDataParams.SideCurrentScore;
            _mainBuildingState = _saveIdleDataParams.MainBuildingState;
            _sideBuildingState = _saveIdleDataParams.SideBuildingState;
        }

        private LevelData GetIdleLevelBuildingData() => Resources.Load<CD_LevelBuildingData>("Data/CD_IdleLevelBuild").Levels;

        private void EmptyListCheck()
        {
            if (!_mainCurrentScore?.Any() ?? true)
            {
                _idleLevel = SaveSignals.Instance.onIdleLevel();
                foreach (var levelBuildingDatas in _levelsData.LevelBuildings[_idleLevel].LevelBuildingDatas)
                {
                    if (_mainCurrentScore != null) _mainCurrentScore.Add(0);
                    _mainBuildingState.Add(BuildingState.Uncompleted);
                    foreach (var t in levelBuildingDatas.sideBuildindData)
                    {
                        _sideCurrentScore.Add(0);
                        _sideBuildingState.Add(BuildingState.Uncompleted);
                    }
                }
            }
        }

        private void OnMainSideSetReferences(GameObject mainObGameObject, int buildingPrice)
        {
            _mainAreas.Add(mainObGameObject);
            mainObGameObject.GetComponent<IdleAreaManager>().SetBuildRef(_mainAreas.Count - 1, true, buildingPrice,
                _mainCurrentScore[_mainAreas.Count - 1], _mainBuildingState[_mainAreas.Count - 1], this);
        }

        private void OnSideSetReferences(List<GameObject> sideGameObjects, List<int> buildingPrices)
        {
            _sideAreas.Add(sideGameObjects);
            for (int i = 0; i < sideGameObjects.Count; i++)
            {
                sideGameObjects[i].GetComponent<IdleAreaManager>().SetBuildRef(_sideCache, false,
                    buildingPrices[i], _sideCurrentScore[_sideCache], _sideBuildingState[_sideCache], this);
                _sideCache++;
            }
        }

        private void OnMainSideComplete(int mainID)
        {
            foreach (var t in _sideAreas[mainID])
            {
                t.GetComponent<IdleAreaManager>().MainSideComplete();
            }
        }

        private void SaveParameters()
        {
            _isMainSide = true;
            foreach (var t in _mainAreas)
            {
                t.GetComponent<IdleAreaManager>().SendRefsToIdleManager();
            }

            _isMainSide = false;
            foreach (var t in _sideAreas.SelectMany(side => side))
            {
                t.GetComponent<IdleAreaManager>().SendRefsToIdleManager();
            }
        }

        public void SetSaveDatas(int areaID, int currentScore, BuildingState buildingState)
        {
            if (_isMainSide)
            {
                _mainCurrentScore[areaID] = currentScore;
                _mainBuildingState[areaID] = buildingState;
            }
            else
            {
                _sideCurrentScore[areaID] = currentScore;
                _sideBuildingState[areaID] = buildingState;
            }
        }

        private SaveIdleDataParams OnGetIdleSaveDatas()
        {
            return new SaveIdleDataParams()
            {
                IdleLevel = SaveSignals.Instance.onIdleLevel(),
                CollectablesCount = IdleSignals.Instance.onColectableScore(),
                MainCurrentScore = _mainCurrentScore,
                SideCurrentScore = _sideCurrentScore,
                MainBuildingState = _mainBuildingState,
                SideBuildingState = _sideBuildingState
            };
        }

        private void CompleteSidesControl()
        {
            MainCompleteSides();
            SideCompleteSides();
        }

        private void MainCompleteSides()
        {
            foreach (var t in _mainBuildingState)
            {
                if (t == BuildingState.Completed)
                {
                    _completeSides++;
                }
            }
        }

        private void SideCompleteSides()
        {
            foreach (var t in _sideBuildingState)
            {
                if (t == BuildingState.Completed)
                {
                    _completeSides++;
                }
            }
        }

        private void NextIdleLevelCheck()
        {
            if ((_mainAreas.Count - 1 + _sideCache) - _completeSides <= 0)
            {
                _mainAreas.Clear();
                _sideAreas.Clear();
                _mainCurrentScore.Clear();
                _sideCurrentScore.Clear();
                _mainBuildingState.Clear();
                _sideBuildingState.Clear();
                _sideCache = 0;
                IdleSignals.Instance.onNextIdleLevel?.Invoke();
                SaveSignals.Instance.onIdleSaveData?.Invoke();
                Init();
                IdleSignals.Instance.onClearIdle?.Invoke();
            }
            else SaveSignals.Instance.onIdleSaveData?.Invoke();
        }

        private void OnNextLevel()
        {
            SaveParameters();
            CompleteSidesControl();
            IdleSignals.Instance.onIdleCollectableValue?.Invoke(_completeSides);
            NextIdleLevelCheck();
            IdleSignals.Instance.onCollectableAreaNextLevel?.Invoke();
            _completeSides = 0;
        }
    }
}