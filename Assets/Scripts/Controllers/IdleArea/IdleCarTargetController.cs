using Datas.ValueObjects;
using UnityEngine;

namespace Controllers.IdleArea
{
    public class IdleCarTargetController : MonoBehaviour
    {
        [SerializeField] private IdleTargetData data;

        public IdleTargetData GetData()
        {
            return data;
        }
    }
}