using System.Collections.Generic;
using UnityEngine;

namespace Datas.UnityObjects
{
    [CreateAssetMenu(fileName = "CD_Level", menuName = "ColorsRunners/CD_Level", order = 0)]
    public class CD_Level : ScriptableObject
    {
        public List<int> Levels = new List<int>();
    }
}