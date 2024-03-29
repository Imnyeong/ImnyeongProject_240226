using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Imnyeong
{
    public class Cat : MonoBehaviour
    {
        [SerializeField] public Image thumbnail;

        [Header("Ability")]
        [SerializeField] public AbilityType abilityType;
        [SerializeField] public int abilityValue;

        [Header("Work")]
        [SerializeField] public int maxWorkPoint;
        [SerializeField] public int currentWorkPoint;
        [SerializeField] private Text textWorkPoint;
        [SerializeField] private Slider workSlider;
        [SerializeField] private Button infoButton;

        public float workDelay = 1.0f;
        private Coroutine coroutine = null;

        private void Start()
        {
            coroutine = StartCoroutine(WorkCoroutine());
            infoButton.onClick.AddListener(OnClickInfo);
        }
        public void SetInfo(Cat _cat)
        {

        }
        public void OnClickCharacter()
        {
            IncreaseWorkPoint();
        }
        public void OnClickInfo()
        {
            UIManager.instance.popupManagerL.ShowCatInfoPopup(this);
        }
        private IEnumerator WorkCoroutine()
        {
            yield return new WaitForSecondsRealtime(workDelay);
            IncreaseWorkPoint();
            coroutine = StartCoroutine(WorkCoroutine());
        }
        private void IncreaseWorkPoint()
        {
            currentWorkPoint++;

            if (currentWorkPoint >= maxWorkPoint)
            {
                GameManager.instance.GetIngredient(abilityType, abilityValue);
                currentWorkPoint = 0;
            }
            workSlider.value = ((float)currentWorkPoint / (float)maxWorkPoint);
            //textWorkPoint.text = $"{currentWorkPoint} / {maxWorkPoint}";
        }
        public void CalculateWorkPoint(int _sec)
        {
            int count = _sec / maxWorkPoint;
            int remain = _sec % maxWorkPoint;

            GameManager.instance.GetIngredient(abilityType, abilityValue, count);

            currentWorkPoint = remain;
            workSlider.value = ((float)currentWorkPoint / (float)maxWorkPoint);
        }
    }
}
