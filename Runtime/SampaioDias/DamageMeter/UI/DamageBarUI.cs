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

        private SkillData _event;
        private DamageMeterManager _manager;

        public void Initialize(SkillData skillData, DamageMeterManager manager)
        {
            _event = skillData;
            _manager = manager;
            skillName.text = _event.Name;
            icon.sprite = _event.Icon;
        }

        public void UpdateBar(DamageLogWrapper wrapper, int index, float fillPercentage)
        {
            var eventValues = wrapper.Values;
            var options = _manager.formatOptions;
            skillDamage.text = options.textLayout
                    .Replace("DPS", DamageStringFormat.Format(eventValues.DamagePerSecond, options))
                    .Replace("TOTAL", DamageStringFormat.Format(eventValues.TotalDamage, options))
                    .Replace("PERCENT", $"{(fillPercentage * 100).ToString(options.toStringFormatter)}%");
            transform.SetSiblingIndex(index);
            bar.fillAmount = fillPercentage;
            bar.color = wrapper.SkillData.Color;
        }
    }
}