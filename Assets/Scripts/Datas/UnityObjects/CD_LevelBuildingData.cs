using Datas.ValueObjects;
using UnityEngine;

namespace Datas.UnityObjects
{
    [CreateAssetMenu(fileName = "CD_IdleLevel", menuName = "ColorsRunners/CD_IdleLevelBuilding", order = 0)]
    public class CD_LevelBuildingData : ScriptableObject
    {
        public LevelData Levels;
    }
}