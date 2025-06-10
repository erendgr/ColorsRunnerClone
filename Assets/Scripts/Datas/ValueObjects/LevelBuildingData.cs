using System;
using System.Collections.Generic;
using UnityEngine;

namespace Datas.ValueObjects
{
    [Serializable]
    public class LevelBuildingData
    {
        public MainBuildingData mainBuildingData;
        public List<SideBuildindData> sideBuildindData;
    }
}