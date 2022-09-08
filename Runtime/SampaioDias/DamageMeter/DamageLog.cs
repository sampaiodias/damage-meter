using System;

namespace SampaioDias.DamageMeter
{
    public class DamageLog
    {
        public string ID { get; }
        public double DamageAmount { get; }
        public DateTime Moment { get; }
        public string SubCategory { get; }

        public DamageLog(string id, double damageAmount, string subCategory)
        {
            ID = id;
            DamageAmount = damageAmount;
            Moment = DateTime.Now;
            SubCategory = subCategory;
        }
    }
}