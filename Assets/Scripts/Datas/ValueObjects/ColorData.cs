using System;
using System.Collections.Generic;
using UnityEngine;

namespace Datas.ValueObjects
{
    [Serializable]
    public class ColorData
    {
        public Material RainbowMaterial;
        public Material RainbowGageMaterial;
        public Material ColorMaterial;
        public List<Color> color = new List<Color>();
    }
}