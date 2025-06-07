using Datas.ValueObjects;
using UnityEngine;

namespace Datas.UnityObjects
{
    [CreateAssetMenu(fileName = "CD_Collectable", menuName = "Game/CD_Collectable", order = 0)]
    public class CD_Collectable : ScriptableObject
    {
        public CollectableData Data;
    }
}