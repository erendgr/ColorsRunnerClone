using Datas.ValueObjects;
using UnityEngine;

namespace Data.UnityObject
{
    [CreateAssetMenu(fileName = "CD_Color", menuName = "ColorsRunners/CD_Color", order = 0)]
    public class CD_Color : ScriptableObject
    {
        public ColorData colorData;
    }
}