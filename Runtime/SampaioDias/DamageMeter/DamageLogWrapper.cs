using System.Collections.Generic;

namespace SampaioDias.DamageMeter
{
    public class DamageLogWrapper
    {
        public SkillData SkillData;
        public List<DamageLog> Logs;
        public DamageLogComputedValues Values;
        public Dictionary<string, DamageLogComputedValues> SubCategoryValues;

        public DamageLogWrapper(SkillData skillData)
        {
            SkillData = skillData;
            Logs = new List<DamageLog>();
            Values = new DamageLogComputedValues();
            SubCategoryValues = new Dictionary<string, DamageLogComputedValues>();
        }
    }
}