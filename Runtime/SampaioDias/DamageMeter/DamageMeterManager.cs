using System;
using System.Collections.Generic;
using SampaioDias.DamageMeter.Utility;
using UnityEngine;

namespace SampaioDias.DamageMeter
{
    /// <summary>
    /// The manager that ties the entire system together.
    /// </summary>
    public class DamageMeterManager : MonoBehaviour
    {
        /// <summary>
        /// The data of every "skill" currently being logged (total damage, DPS, name, icon, etc.)
        /// </summary>
        public List<DamageLogWrapper> CurrentValues { get; private set; }
        
        [Tooltip("The frequency which the manager will calculate the DPS values. Higher values give better performance.")]
        public float updateFrequencyInSeconds = 0.2f;

        /// <summary>
        /// Number options used for the UI
        /// </summary>
        public DamageStringFormat.DamageNumberOptions formatOptions = 
            new DamageStringFormat.DamageNumberOptions(10000, "K", "M", "n1", "DPS | TOTAL (PERCENT)");

        public bool IsPaused { get; private set; }
        
        /// <summary>
        /// On every X seconds, this is invoked with the current DPS values. X is updateFrequencyInSeconds.
        /// </summary>
        public Action<List<DamageLogWrapper>> OnUpdateValues;
        
        /// <summary>
        /// This is invoked when a certain DamageEvent ID is registered for the first time.
        /// </summary>
        public Action<SkillData> OnNewSkillRegistered;

        private DamageLogContainer _history;
        private float _currentTick;

        private void Awake()
        {
            _history = new DamageLogContainer();
            _history.Reset();
            _currentTick = 0;
        }

        private void Update()
        {
            if (IsPaused) return;
            
            _currentTick += Time.deltaTime;
            if (_currentTick < updateFrequencyInSeconds) return;
            
            _history.Compute(_currentTick);
            CurrentValues = _history.GetValues();
            OnUpdateValues?.Invoke(CurrentValues);
            _currentTick = 0;
        }

        /// <summary>
        /// Registers a damage amount associated to a specific skill. Does nothing if logging is paused.
        /// </summary>
        /// <param name="skillData">The skill information, which should be immutable</param>
        /// <param name="damageAmount">How much damage this skill is dealing right now</param>
        /// <param name="subCategory">If needed, provide a subCategory ("Critical Strike", "Damage over Time", etc.). If not, just pass null or an empty string.</param>
        public void Register(SkillData skillData, double damageAmount, string subCategory)
        {
            if (IsPaused) return;
            
            var firstTimeThisSkillWasRegistered = _history.Register(skillData, damageAmount, subCategory);
            if (firstTimeThisSkillWasRegistered)
            {
                OnNewSkillRegistered?.Invoke(skillData);
            }
        }

        /// <summary>
        /// Pauses the current logging mechanism/timer. Useful for Pause Screens or when the player leaves combat.
        /// </summary>
        public void PauseLogging()
        {
            IsPaused = true;
        }

        /// <summary>
        /// Resumes the current logging mechanism/timer.
        /// </summary>
        public void ResumeLogging()
        {
            IsPaused = false;
        }
        
        /// <summary>
        /// Pauses or resumes the current logging mechanism/timer (depending if it is currently running or not)
        /// </summary>
        public void ToggleLogging()
        {
            IsPaused = !IsPaused;
        }

        /// <summary>
        /// Resets the current logs, clearing all data. You may call ResumeLogging() to start it again.
        /// </summary>
        public void ResetLogging()
        {
            PauseLogging();
            _currentTick = 0;
            _history.Reset();
        }
    }
}