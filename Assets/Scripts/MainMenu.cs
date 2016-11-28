using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	public string playGameLevel;
	public string level2;
	public GameObject instructionsMenu;
	public GameObject playMenu;
	public GameObject settingMenu;
	public GameObject exitInstructionsControl;

	public Slider volumeCotrol;

	// Use this for initialization
	void Start () {
		float defaultVolume = PlayerPrefs.HasKey ("volume") ? PlayerPrefs.GetFloat ("volume") : 0.5f;
		AudioListener.volume = defaultVolume;
		volumeCotrol.value = defaultVolume;
        PlayerPrefs.SetInt("Hearts", 3);
    }

	public void PlayGame() {
		SceneManager.LoadScene (playGameLevel);
		// Application.LoadLevel (playGameLevel);
	}

	public void PlayGame2() {
		SceneManager.LoadScene (level2);
		// Application.LoadLevel (level2);
	}

	public void ExitGame() {
		Application.Quit ();
	}

	public void Instructions() {
		playMenu.SetActive (false);
		instructionsMenu.SetActive (true);
		exitInstructionsControl.SetActive (true);
		settingMenu.SetActive (false);
	}

	public void ExitInstructions() {
		playMenu.SetActive (true);
		instructionsMenu.SetActive (false);
		exitInstructionsControl.SetActive (false);
	}

	public void SettingMenu() {
		settingMenu.SetActive (true);
		instructionsMenu.SetActive (false);
		exitInstructionsControl.SetActive (false);
	}

	public void ExitSettingMenu() {
		settingMenu.SetActive (false);
	}

	public void VolumeControl() {
		PlayerPrefs.SetFloat ("volume", volumeCotrol.value);
		AudioListener.volume = volumeCotrol.value;
	}

	public void onMouseEnter() {
		Cursor.SetCursor (null, Vector2.zero, CursorMode.Auto);
	}

	public void onMouseExit() {
		Cursor.SetCursor (null, Vector2.zero, CursorMode.Auto);
	}
}
