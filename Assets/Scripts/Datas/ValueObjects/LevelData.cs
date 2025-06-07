using System;
using System.Collections.Generic;
using Enums;
using UnityEngine;

namespace Datas.ValueObjects
{
    [Serializable]
    public class LevelData
    {
        public List<LevelBuilding> LevelBuildings;
    }

    [Serializable]
    public class LevelBuilding
    {
        public int LevelNumber;
        public GameObject LevelPlane;
        public List<LevelBuildingData> LevelBuildingDatas;
    }

    [Serializable]
    public class LevelBuildingData
    {
        public MainBuildingData mainBuildingData;
        public List<SideBuildindData> sideBuildindData;
    }

    [Serializable]
    public class MainBuildingData
    {
        public BuildingState BuildingState = BuildingState.Uncompleted;
        public GameObject Building;
        public int MainBuildingScore = 100;
        public Vector3 InstantitatePos;
    }

    [Serializable]
    public class SideBuildindData
    {
        public BuildingState BuildingState = BuildingState.Uncompleted;
        public GameObject Building;
        public int SideBuildingScore = 100;
        public Vector3 InstantitatePos;
    }
}