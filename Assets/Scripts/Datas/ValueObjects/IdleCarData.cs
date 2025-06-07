using System;
using DG.Tweening;

namespace Datas.ValueObjects
{
    [Serializable]
    public class IdleCarData
    {
        public float ReachingTime = 2f;
        public float RotationTime = 1f;
        public Ease RotationEase = Ease.Unset;
        public Ease ReachingEase = Ease.Linear;
    }
}