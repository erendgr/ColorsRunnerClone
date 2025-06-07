using System;
using System.Collections.Generic;
using Enums;

namespace Datas.ValueObjects
{
    [Serializable]
    public class IdleTargetData
    {
        public List<IdleNavigationEnum> axises = new List<IdleNavigationEnum>();
    }
}