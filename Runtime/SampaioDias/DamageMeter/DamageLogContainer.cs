using System;
using System.Collections.Generic;
using System.Linq;

namespace SampaioDias.DamageMeter
{
    public class DamageLogContainer
    {
        private Dictionary<string, DamageLogWrapper> _dataById;
        private List<DamageLog> _uncomputedLogs;
        public float TotalSeconds { get; private set; }

        public void Reset()
        {
            TotalSeconds = 0;
            _dataById = new Dictionary<string, DamageLogWrapper>();
            _uncomputedLogs = new List<DamageLog>();
        }

        /// <summary>
        /// Registers a damage amount associated to a specific "skill".
        /// </summary>
        /// <param name="skillData"></param>
        /// <param name="damageAmount"></param>
        /// <param name="subCategory"></param>
        /// <returns>True if this DamageEvent was registered for the first time.</returns>
        public bool Register(SkillData skillData, double damageAmount, string subCategory)
        {
            _uncomputedLogs.Add(new DamageLog(skillData.ID, damageAmount, subCategory));
            
            if (!_dataById.ContainsKey(skillData.ID))
            {
                var wrapper = new DamageLogWrapper(skillData);
                _dataById.Add(skillData.ID, wrapper);
                return true;
            }

            return false;
        }

        public void Compute(float elapsedTime)
        {
            TotalSeconds += elapsedTime;
            foreach (var log in _uncomputedLogs)
            {
                var data = _dataById[log.ID];
                data.Logs.Add(log);
                data.Values.TotalDamage += log.DamageAmount;
                data.Values.DamagePerSecond = data.Values.TotalDamage / TotalSeconds;
            }
            _uncomputedLogs.Clear();
        }

        public List<DamageLogWrapper> GetValues()
        {
            return _dataById.Values.OrderByDescending(wrapper => wrapper.Values.TotalDamage).ToList();
        }
    }
}