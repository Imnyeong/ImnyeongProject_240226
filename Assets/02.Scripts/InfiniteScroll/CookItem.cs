using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Imnyeong
{
	public class CookItem : MonoBehaviour
	{
		[SerializeField]
		private Image thumbnail;

		[SerializeField]
		private Text nameText;
		[SerializeField]
		private Text cookText;

		[SerializeField]
		private Button recipeButton;
		[SerializeField]
		private Button cookButton;

		private Image foodThmbnail;
		private string foodName;

		public void UpdateItem(FoodData _data)
		{
			foodThmbnail = _data.thumbnail;
			foodName = _data.foodName;
			CheckIngredient(_data);

			SetItem(foodThmbnail, foodName, delegate { OnClickRecipeButton(_data); }, delegate { OnClickCookButton(_data); });
		}

		public void SetItem(Image _image, string _name, UnityAction _recipeAction, UnityAction _cookAction)
		{
			recipeButton.onClick.RemoveAllListeners();
			cookButton.onClick.RemoveAllListeners();

			thumbnail = _image;
			nameText.text = _name;

			recipeButton.onClick.AddListener(_recipeAction);
			if(cookButton.interactable)
            {
				cookButton.onClick.AddListener(_cookAction);
			}
		}

		public void OnClickRecipeButton(FoodData _data)
        {
			Debug.Log($"_data.foodName = {_data.foodName}");
			for(int i = 0; i < _data.requiredIngredients.Count; i++)
            {
				Debug.Log($"_data.requiredIngredients{i}.name = {_data.requiredIngredients[i].name} _data.requiredCounts{i} = {_data.requiredCounts[i]}");
			}
        }
		public void OnClickCookButton(FoodData _data)
		{
			GameManager.instance.GetFood(_data);
			CheckIngredient(_data);
		}

		public void CheckIngredient(FoodData _data)
        {
			cookButton.interactable = GameManager.instance.CheckIngredients(_data);
			cookText.text = cookButton.interactable ? "요리 가능" : "재료 부족";
		}
	}
}