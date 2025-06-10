using System;
using System.Collections.Generic;
using UnityEngine;

namespace Datas.ValueObjects
{
    [Serializable]
    public class LevelBuilding
    {
        public int LevelNumber;
        public GameObject LevelPlane;
        public List<LevelBuildingData> LevelBuildingDatas;
    }
}