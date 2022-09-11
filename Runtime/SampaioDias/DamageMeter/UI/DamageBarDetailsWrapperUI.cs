using TMPro;
using UnityEngine;

namespace SampaioDias.DamageMeter.UI
{
    public class DamageBarDetailsWrapperUI : MonoBehaviour
    {
        public TMP_Text textTitle;
        public TMP_Text textNumbers;
        public DamageLogComputedValues currentValue;

        public void UpdateText(string title, string numbers, DamageLogComputedValues newValue)
        {
            textTitle.text = title;
            textNumbers.text = numbers;
            currentValue = newValue;
            gameObject.SetActive(true);
        }
    }
}