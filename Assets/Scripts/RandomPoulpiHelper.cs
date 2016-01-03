using UnityEngine;
using System.Collections;

/// <summary>
/// Creating instance of particles from code with no effort
/// </summary>
public class RandomPoulpiHelper : MonoBehaviour
{
	public GameObject enemyPrefab;

	public float numEnemies = 15;
	public float xMin = 5F;
	public float xMax = 85F;
	public float yMin = 6.5F;
	public float yMax = -6.5F;

	public float delay = 36F;

	IEnumerator Spawn (float duration) {

		GameObject newParent = GameObject.Find ("2 - Background Enemy");

		var dist = (transform.position - Camera.main.transform.position).z;
		float rightBorder = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, dist)).x;

		for (int i=0; i < numEnemies; i++) 
		{
			Vector3 newPos = new Vector3 (Random.Range((rightBorder + xMin), (rightBorder + xMax)), Random.Range(yMin, yMax), 1);
			GameObject octo = Instantiate (enemyPrefab, newPos, Quaternion.identity) as GameObject;
			
			octo.transform.parent = newParent.transform;
		}

		yield return new WaitForSeconds(duration);

		StartCoroutine (Spawn(delay));

	}


	// Use this for initialization
	void Start () {
		
		StartCoroutine (Spawn(delay));
		
	}
}