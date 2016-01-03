using UnityEngine;
using System.Collections;

public class MenuScript : MonoBehaviour
{
	public Sprite splashSprite;
	public Sprite loadingSprite;
	// public Sprite controlSprite;

	void Start()
	{
		// Mulai tampilno splash screen pas start. Nek method iki gak iso gantien method OnPreRender/OnWillRenderObject
		StartCoroutine(ShowSplashScreen());
	}

	void Update()
	{
		bool start = Input.GetButtonDown ("Fire1");
		bool quit = Input.GetButtonDown ("Fire2");

		if (start) {
			StartCoroutine(LoadNewScene());
		} else if (quit) {
			Application.Quit();
		}
	}

	IEnumerator ShowSplashScreen()
	{
		// Splash screen iku asline ngilangi logo + tombol" + ngganti background karo gambar splash screen.
		// Nek gambar backgroune gak sip, scale nganggo vector. 

		// TODO: Gantien gambar background
		// TODO: Ilangono object logo karo tombol
		// GameObject.Find("Loadingbutton").GetComponent<SpriteRenderer>().sprite = splashSprite;
		// GameObject.Find("Loadingbutton").transform.localScale = new Vector3(2,2,1);

		yield return new WaitForSeconds(3);
		// GameObject.Find("Control").GetComponent<SpriteRenderer>().sprite = controlSprite;
	}

	IEnumerator LoadNewScene() 
	{
		AsyncOperation async = Application.LoadLevelAsync("Stage0");
		
		while (!async.isDone)
		{
			GameObject.Find("Control").GetComponent<SpriteRenderer>().sprite = loadingSprite;
			yield return null;
		}
	}
}