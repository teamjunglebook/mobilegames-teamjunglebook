using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

	public string playGameLevel;

	public void PlayGame() {
		Application.LoadLevel (playGameLevel);
	}

	public void ExitGame() {
		Application.Quit ();
	}
}
