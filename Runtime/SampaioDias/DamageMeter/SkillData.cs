using System;
using UnityEngine;

namespace SampaioDias.DamageMeter
{
    [Serializable]
    public class SkillData
    {
        [field: SerializeField]
        public Sprite Icon { get; private set; }
        [field: SerializeField]
        public string ID { get; private set; }
        [field: SerializeField]
        public string Name { get; private set; }
        [field: SerializeField]
        public Color Color { get; private set; }

        public SkillData(Sprite icon, string id, string name, Color color)
        {
            this.Icon = icon;
            this.ID = id;
            this.Name = name;
            this.Color = color;
        }
    }
}