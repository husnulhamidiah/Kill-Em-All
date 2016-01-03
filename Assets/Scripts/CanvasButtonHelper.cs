using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CanvasButtonHelper : MonoBehaviour {
	
	private Button[] buttons;
	
	void Awake()
	{		
		// Get the buttons
		buttons = GetComponentsInChildren<Button>();
		EventSystem.current.SetSelectedGameObject(null);
	}

	void Start()
	{
		foreach (var b in buttons)
		{
			b.gameObject.SetActive(true);
			b.gameObject.AddComponent<ButtonEventScript>();
		}
		EventSystem.current.SetSelectedGameObject(buttons[0].gameObject);
	}

	void OnEnable()
	{
		EventSystem.current.SetSelectedGameObject(buttons[0].gameObject);
	}
}
