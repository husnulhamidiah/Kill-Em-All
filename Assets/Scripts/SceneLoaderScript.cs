using UnityEngine;
using System.Collections;

public class SceneLoaderScript : MonoBehaviour {

	private bool loadScene = false;

	// Update is called once per frame
	void Update () {
	
		if (loadScene == false)
		{
			loadScene = true;

			StartCoroutine(LoadNewScene());
		}

		if (loadScene == true) 
		{

		}
	}

	IEnumerator LoadNewScene() 
	{
		yield return new WaitForSeconds(3);

		AsyncOperation async = Application.LoadLevelAsync("Menu");

		while (!async.isDone)
		{
			yield return null;
		}
	}
}
