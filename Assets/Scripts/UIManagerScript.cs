using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class UIManagerScript : MonoBehaviour {

	public GameObject mainCanvas, settingCanvas, resultCanvas;
	private GameObject main, setting, result;
	private int scoreNumber = 0;

	void Awake()
	{
		main = mainCanvas;
		setting = settingCanvas;
		result = resultCanvas;
	}

	void Start()
	{
		ResumeGame();
	}

	void Update()
	{
		bool esc = Input.GetButtonDown ("Cancel");
		if (esc) {
			ShowPauseMenu();
		}
	}

	public void ExitToMenu()
	{
		Application.LoadLevel("Menu");
	}
	
	public void RestartGame()
	{
		string stage = Application.loadedLevelName;
		Application.LoadLevel(stage);

	}

	public void ResumeGame()
	{
		EventSystem.current.SetSelectedGameObject(null);
		setting.SetActive(false);
		main.SetActive(false);
		Time.timeScale = 1;
	}

	public void ShowPauseMenu()
	{
		main.SetActive(true);
		Time.timeScale = 0;
	}

	public void ShowResultMenu(bool win)
	{
		result.SetActive(true);
		if (win) {
			var resultButton = GameObject.FindGameObjectWithTag("Result");
			resultButton.GetComponentInChildren<Text>().text = "You Win!";
		}

		Time.timeScale = 0;
	}

	public void ShowSettingsMenu()
	{
		main.SetActive(false);
		setting.SetActive(true);
	}

	public void UpdateLives(int num)
	{
		var lives = GameObject.FindGameObjectWithTag("Lives");
		lives.GetComponentInChildren<Text>().text = "Lives :: " + num;
	}

	public void UpdateScore()
	{
		scoreNumber += 100;
		var score = GameObject.FindGameObjectWithTag("Score");
		score.GetComponentInChildren<Text>().text = "SCORE : " + scoreNumber;
	}

	public void MuteMusic()
	{
		var music = GameObject.FindGameObjectWithTag("Music");
		var stat = music.GetComponent<AudioSource>().mute;

		if (stat) {
			music.GetComponent<AudioSource>().mute = false;
			GameObject b = GameObject.Find("UIManager/SettingCanvas/MainPanel/MusicButton");
			b.GetComponentInChildren<Text>().text = "Music      ON";
		} else {
			music.GetComponent<AudioSource>().mute = true;
			GameObject b = GameObject.Find("UIManager/SettingCanvas/MainPanel/MusicButton");
			b.GetComponentInChildren<Text>().text = "Music      OFF";
		}
	}

	public void MuteSFx()
	{
		var script = GameObject.Find("Scripts");
		if (!script.GetComponent<SoundEffectsHelper>().mute) {
			script.GetComponent<SoundEffectsHelper>().mute = true;
			GameObject b = GameObject.Find("UIManager/SettingCanvas/MainPanel/SfxButton");
			b.GetComponentInChildren<Text>().text = "SFx        OFF";
		} else {
			script.GetComponent<SoundEffectsHelper>().mute = false;
			GameObject b = GameObject.Find("UIManager/SettingCanvas/MainPanel/SfxButton");
			b.GetComponentInChildren<Text>().text = "SFx        ON";
		}
	}

	public void NextScene(string lvl)
	{
		StartCoroutine(LoadNewScene(lvl));
	}

	IEnumerator LoadNewScene(string lvl) 
	{
		AsyncOperation async = Application.LoadLevelAsync(lvl);
		
		while (!async.isDone)
		{
			var resultButton = GameObject.FindGameObjectWithTag("Result");
			resultButton.GetComponentInChildren<Text>().text = "Loading...";
			yield return null;
		}
	}
}
