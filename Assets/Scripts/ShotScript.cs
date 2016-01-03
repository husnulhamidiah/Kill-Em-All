using UnityEngine;
using System.Collections;

public class ShotScript : MonoBehaviour {

	public int damage = 1;
	public bool isEnemyShot = false;

	// Use this for initialization
	void Start () {

		// Destroy (gameObject, 2);

	}

	void Update () {
		// Out of camera?
		var dist = (transform.position - Camera.main.transform.position).z;
		float leftBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).x;
		var rightBorder = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, dist)).x + 1.0;

		if (gameObject.transform.position.x < leftBorder || gameObject.transform.position.x > rightBorder)
		{
			Destroy(gameObject);
		}
	}
}
