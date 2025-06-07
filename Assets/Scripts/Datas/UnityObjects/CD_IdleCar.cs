using Datas.ValueObjects;
using UnityEngine;

namespace Datas.UnityObjects
{
    [CreateAssetMenu(fileName = "CD_IdleCar", menuName = "ColorsRunners/CD_IdleCar", order = 0)]
    public class CD_IdleCar : ScriptableObject
    {
        public IdleCarData Data;
    }
}