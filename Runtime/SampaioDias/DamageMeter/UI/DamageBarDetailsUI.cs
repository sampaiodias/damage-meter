using System;
using System.Collections.Generic;
using SampaioDias.DamageMeter.Utility;
using TMPro;
using UnityEngine;

namespace SampaioDias.DamageMeter.UI
{
    public class DamageBarDetailsUI : MonoBehaviour
    {
        public TMP_Text textTitles;
        public TMP_Text textNumbers;
        public CanvasGroup canvasGroup;
        
        private SkillData _event;
        private DamageMeterManager _manager;

        public void Initialize(SkillData skillData, DamageMeterManager manager)
        {
            _event = skillData;
            _manager = manager;
        }

        public void Show()
        {
            canvasGroup.alpha = 1;
        }

        public void Hide()
        {
            canvasGroup.alpha = 0;
        }

        private void Update()
        {
            if (!(canvasGroup.alpha > 0)) return;
            
            var myWrapper = _manager.CurrentValues.Find(wrapper => wrapper.SkillData.ID == _event.ID);
            var titles = $"{myWrapper.SkillData.Name}";
            var damage = $"{DamageStringFormat.SkillDamageText(myWrapper.Values, 1, _manager.formatOptions)}";
            
            foreach (var subCategoryKeyPair in myWrapper.SubCategoryValues)
            {
                titles += $"\n{myWrapper.SkillData.Name} ({subCategoryKeyPair.Key})";
                damage += $"\n{DamageStringFormat.SkillDamageText(subCategoryKeyPair.Value, (float)(subCategoryKeyPair.Value.TotalDamage / myWrapper.Values.TotalDamage), _manager.formatOptions)}";
            }
            
            textTitles.SetText(titles);
            textNumbers.SetText(damage);
        }
    }
}