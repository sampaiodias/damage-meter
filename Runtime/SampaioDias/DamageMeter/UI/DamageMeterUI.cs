using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SampaioDias.DamageMeter.UI
{
    public class DamageMeterUI : MonoBehaviour
    {
        [Tooltip("When enabled, mousing over the bars will display additional information.")]
        public bool enableMouseOver = true;
        [Tooltip("When enabled, the name of the subcategory will be followed by its count (how many instances were logged for that subcategory).")]
        public bool mouseOverSkillCount = true;
        [Tooltip("List of keys that, when pressed, will show/hide this panel.")]
        public List<KeyCode> toggleVisibilityHotkeys = new List<KeyCode>() { KeyCode.F12 };
        
        [Space]
        public DamageMeterManager manager;
        public GameObject barPrefab;
        public GameObject content;
        public CanvasGroup canvasGroup;

        private Dictionary<string, DamageBarUI> _barDictionary;

        private void Awake()
        {
            if (manager == null)
            {
                manager = FindObjectOfType<DamageMeterManager>();
            }

            _barDictionary = new Dictionary<string, DamageBarUI>();
        }

        private void Update()
        {
            foreach (var keyCode in toggleVisibilityHotkeys)
            {
                if (Input.GetKeyDown(keyCode))
                {
                    ToggleVisibility();
                }
            }
        }

        private void OnEnable()
        {
            manager.OnNewSkillRegistered += NewSkill;
            manager.OnUpdateValues += ValuesUpdated;
        }

        private void OnDisable()
        {
            manager.OnNewSkillRegistered -= NewSkill;
            manager.OnUpdateValues -= ValuesUpdated;
        }

        private void NewSkill(SkillData skillData)
        {
            var newBarGameObject = Instantiate(barPrefab, content.transform);
            var newBar = newBarGameObject.GetComponent<DamageBarUI>();
            newBarGameObject.name = $"DamageBar - {skillData.ID}";
            _barDictionary.Add(skillData.ID, newBar);
            newBar.Initialize(skillData, manager, this);
        }
        
        private void ValuesUpdated(List<DamageLogWrapper> newValues)
        {
            var accumulatedDamage = newValues.Sum(wrapper => wrapper.Values.TotalDamage);
            
            for (var index = 0; index < newValues.Count; index++)
            {
                var wrapper = newValues[index];
                var damageBarUI = _barDictionary[wrapper.SkillData.ID];
                damageBarUI.UpdateBar(wrapper, index, (float)(wrapper.Values.TotalDamage / accumulatedDamage));
            }
        }
        
        /// <summary>
        /// Pauses or resumes the current logging mechanism/timer (depending if it is currently running or not)
        /// </summary>
        public void ToggleLogging()
        {
            manager.ToggleLogging();
        }
        
        /// <summary>
        /// Resets the current logs, clearing all data.
        /// </summary>
        public void ResetLogging()
        {
            manager.ResetLogging();
            foreach (var bar in _barDictionary.Values)
            {
                Destroy(bar.gameObject);
            }
            _barDictionary.Clear();
        }
        
        public void ToggleVisibility()
        {
            canvasGroup.alpha = canvasGroup.alpha == 0 ? 1 : 0;
            canvasGroup.blocksRaycasts = !canvasGroup.blocksRaycasts;
        }
    }
}