using System.Collections.Generic;
using UnityEngine;

namespace Datas.UnityObjects
{
    [CreateAssetMenu(fileName = "CD_IdleLevel", menuName = "ColorsRunners/CD_IdleLevel", order = 0)]
    public class CD_IdleLevel : ScriptableObject
    {
        public List<CD_LevelBuildingData> Levels = new List<CD_LevelBuildingData>();
    }
}