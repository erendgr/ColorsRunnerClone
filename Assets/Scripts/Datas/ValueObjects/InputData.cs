using System;
using UnityEngine;

namespace Datas.ValueObjects
{
    [Serializable]
    public class InputData
    {
        public float HorizontalInputSpeed = 2f;
        public Vector2 ClampSides = new Vector2(-3f, 3f);
        public float ClampSpeed = 0.007f;
    }
}