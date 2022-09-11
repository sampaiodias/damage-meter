using System;
using System.Collections.Generic;
using SampaioDias.DamageMeter.Utility;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SampaioDias.DamageMeter.UI
{
    public class DamageBarUI : MonoBehaviour
    {
        public TMP_Text skillName;
        public TMP_Text skillDamage;
        public Image icon;
        public Image bar;
        public DamageBarDetailsUI details;

        private SkillData _event;
        private DamageMeterManager _manager;
        private DamageMeterUI _damageMeterUI;

        public void Initialize(SkillData skillData, DamageMeterManager manager, DamageMeterUI damageMeterUI)
        {
            _event = skillData;
            _manager = manager;
            skillName.text = _event.Name;
            icon.sprite = _event.Icon;
            _damageMeterUI = damageMeterUI;
            details.Initialize(skillData, manager, damageMeterUI);
        }

        public void UpdateBar(DamageLogWrapper wrapper, int index, float fillPercentage)
        {
            var eventValues = wrapper.Values;
            var options = _manager.formatOptions;
            skillDamage.text = DamageStringFormat.SkillDamageText(eventValues, fillPercentage, options);
            transform.SetSiblingIndex(index);
            bar.fillAmount = fillPercentage;
            bar.color = wrapper.SkillData.Color;
        }
    }
}