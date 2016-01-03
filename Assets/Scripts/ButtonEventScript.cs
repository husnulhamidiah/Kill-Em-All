using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using UnityEngine.UI;

public class ButtonEventScript : MonoBehaviour, ISelectHandler, IDeselectHandler, IPointerEnterHandler, IPointerExitHandler {

	public void OnSelect(BaseEventData eventData)
	{
		transform.gameObject.GetComponent<Image>().color = Color.grey;
	}

	public void OnDeselect(BaseEventData eventData)
	{
		transform.gameObject.GetComponent<Image>().color = Color.white;
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		EventSystem.current.SetSelectedGameObject(transform.gameObject);
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		EventSystem.current.SetSelectedGameObject(null);
	}
}
