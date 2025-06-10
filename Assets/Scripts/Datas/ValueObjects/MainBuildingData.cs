using System;
using Enums;
using UnityEngine;

namespace Datas.ValueObjects
{
    [Serializable]
    public class MainBuildingData
    {
        public BuildingState BuildingState = BuildingState.Uncompleted;
        public GameObject Building;
        public int MainBuildingScore = 100;
        public Vector3 InstantiatePos;
    }
}