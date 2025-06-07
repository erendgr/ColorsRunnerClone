using Datas.ValueObjects;
using UnityEngine;

namespace Datas.UnityObjects
{
    [CreateAssetMenu(fileName = "CD_Drone", menuName = "ColorsRunners/CD_Drone", order = 0)]
    public class CD_Drone : ScriptableObject
    {
        public DronePoolData Data;
    }
}