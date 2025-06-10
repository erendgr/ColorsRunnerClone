using System;
using Enums;
using UnityEngine;

namespace Datas.ValueObjects
{
    [Serializable]
    public class SideBuildindData
    {
        public BuildingState BuildingState = BuildingState.Uncompleted;
        public GameObject Building;
        public int SideBuildingScore = 100;
        public Vector3 InstantitatePos;
    }
}