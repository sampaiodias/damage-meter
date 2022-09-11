using System;
using System.Collections.Generic;
using System.Linq;
using SampaioDias.DamageMeter.Utility;
using TMPro;
using UnityEngine;

namespace SampaioDias.DamageMeter.UI
{
    public class DamageBarDetailsUI : MonoBehaviour
    {
        
        public CanvasGroup canvasGroup;
        public Transform textParent;
        public GameObject wrapperPrefab;
        
        private SkillData _event;
        private DamageMeterManager _manager;
        private List<DamageBarDetailsWrapperUI> _texts;

        public void Initialize(SkillData skillData, DamageMeterManager manager)
        {
            _event = skillData;
            _manager = manager;
            _texts ??= new List<DamageBarDetailsWrapperUI>();
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
            
            InstantiateMissingTextWrappers(myWrapper);

            var index = UpdateTexts(myWrapper);

            for (var i = index; i < _texts.Count; i++)
            {
                _texts[i].gameObject.SetActive(false);
            }

            OrderTexts();
        }

        private int UpdateTexts(DamageLogWrapper myWrapper)
        {
            var firstText = _texts[0];
            firstText.UpdateText(
                $"{myWrapper.SkillData.Name} ({myWrapper.Values.Count})", 
                $"{DamageStringFormat.SkillDamageText(myWrapper.Values, 1, _manager.formatOptions)}",
                myWrapper.Values);

            var index = 1;
            foreach (var subCategoryKeyPair in myWrapper.SubCategoryValues)
            {
                var text = _texts[index];
                text.UpdateText(
                    $"{subCategoryKeyPair.Key} ({subCategoryKeyPair.Value.Count})",
                    $"{DamageStringFormat.SkillDamageText(subCategoryKeyPair.Value, (float)(subCategoryKeyPair.Value.TotalDamage / myWrapper.Values.TotalDamage), _manager.formatOptions)}",
                    subCategoryKeyPair.Value);
                index++;
            }

            return index;
        }

        private void InstantiateMissingTextWrappers(DamageLogWrapper myWrapper)
        {
            var missingTexts = (1 + myWrapper.SubCategoryValues.Count) - _texts.Count;
            for (var i = 0; i < missingTexts; i++)
            {
                _texts.Add(Instantiate(wrapperPrefab, textParent).GetComponent<DamageBarDetailsWrapperUI>());
            }
        }
        
        private void OrderTexts()
        {
            var orderedTexts = _texts.OrderBy(t => t.currentValue.TotalDamage);
            foreach (var t in orderedTexts)
            {
                t.transform.SetSiblingIndex(0);
            }
        }
    }
}