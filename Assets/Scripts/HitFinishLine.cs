using UnityEngine;
using System.Collections;

public class HitFinishLine : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D collider)
	{
		GameObject hitObjt = collider.gameObject;

		if (hitObjt.tag == "Player")
		{
			var UIManager = FindObjectOfType<UIManagerScript>();
			UIManager.ShowResultMenu(true);
		}
	}
}
