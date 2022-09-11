using System;
using System.Collections.Generic;
using SampaioDias.DamageMeter.Utility;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SampaioDias.DamageMeter.UI
{
    public class DamageBarUI : MonoBehaviour
    {
        public TMP_Text skillName;
        public TMP_Text skillDamage;
        public Image icon;
        public Image bar;
        public EventTrigger trigger;
        public DamageBarDetailsUI details;

        private SkillData _event;
        private DamageMeterManager _manager;
        private DamageMeterUI _damageMeterUI;

        public void Initialize(SkillData skillData, DamageMeterManager manager, DamageMeterUI damageMeterUI, ScrollRect scrollView)
        {
            _event = skillData;
            _manager = manager;
            skillName.text = _event.Name;
            icon.sprite = _event.Icon;
            _damageMeterUI = damageMeterUI;
            details.Initialize(skillData, manager, damageMeterUI);
            FixEventTrigger(scrollView);
        }

        private void FixEventTrigger(ScrollRect scrollView)
        {
            EventTrigger.Entry entryBegin = new EventTrigger.Entry(),
                entryDrag = new EventTrigger.Entry(),
                entryEnd = new EventTrigger.Entry(),
                entryPotential = new EventTrigger.Entry(),
                entryScroll = new EventTrigger.Entry();
 
            entryBegin.eventID = EventTriggerType.BeginDrag;
            entryBegin.callback.AddListener((data) => { scrollView.OnBeginDrag((PointerEventData)data); });
            trigger.triggers.Add(entryBegin);
 
            entryDrag.eventID = EventTriggerType.Drag;
            entryDrag.callback.AddListener((data) => { scrollView.OnDrag((PointerEventData)data); });
            trigger.triggers.Add(entryDrag);
 
            entryEnd.eventID = EventTriggerType.EndDrag;
            entryEnd.callback.AddListener((data) => { scrollView.OnEndDrag((PointerEventData)data); });
            trigger.triggers.Add(entryEnd);
 
            entryPotential.eventID = EventTriggerType.InitializePotentialDrag;
            entryPotential.callback.AddListener((data) => { scrollView.OnInitializePotentialDrag((PointerEventData)data); });
            trigger.triggers.Add(entryPotential);
 
            entryScroll.eventID = EventTriggerType.Scroll;
            entryScroll.callback.AddListener((data) => { scrollView.OnScroll((PointerEventData)data); });
            trigger.triggers.Add(entryScroll);
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