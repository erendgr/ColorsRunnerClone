using Datas.ValueObjects;
using UnityEngine;

namespace Datas.UnityObjects
{
    [CreateAssetMenu(fileName = "CD_IdleCitizen", menuName = "ColorsRunners/CD_IdleCitizen", order = 0)]
    public class CD_IdleCitizen : ScriptableObject
    {
        public IdleCitizenData Data;
    }
}