using UnityEngine;
using Datas.ValueObjects;

namespace Data.UnityObject
{
    [CreateAssetMenu(fileName = "CD_StackData", menuName = "ColorsRunners/CD_Stack", order = 0)]
    public class CD_Stack : ScriptableObject
    {
        public StackData StackData;
    }
}