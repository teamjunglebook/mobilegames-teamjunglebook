using UnityEngine;
using System.Collections;

public class DeathMenu : MonoBehaviour {

	public string mainMenuLevel;

	public void RestartGame () {
		FindObjectOfType<GameManager> ().ResetGame();
	}

	public void QuitToMainMenu() {
		Application.LoadLevel (mainMenuLevel);
	}
}
