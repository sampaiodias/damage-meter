using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

// ReSharper disable once CheckNamespace
namespace SampaioDias.DamageMeter.Demo
{
    public class DemoManager : MonoBehaviour
    {
        public List<SkillData> skills;
        public DamageMeterManager manager;
        private List<bool> _enabledSkills;

        private void Awake()
        {
            _enabledSkills = new List<bool>();
            skills.ForEach(x => _enabledSkills.Add(false));
        }

        private void FixedUpdate()
        {
            for (var index = 0; index < skills.Count; index++)
            {
                if (_enabledSkills[index])
                {
                    var skill = skills[index];
                    
                    var isCriticalStrike = Random.Range(1, 100) >= 80;
                    var subCategory = isCriticalStrike ? "Critical Strike" : "";
                    var randomAmountOfDamage = Random.Range(1, 20 - index) * (isCriticalStrike ? 2 : 1);
                    
                    manager.Register(skill, randomAmountOfDamage, subCategory); // This is the important part
                }
            }
        }

        public void Toggle(int index)
        {
            _enabledSkills[index] = !_enabledSkills[index];
        }
    }
}
