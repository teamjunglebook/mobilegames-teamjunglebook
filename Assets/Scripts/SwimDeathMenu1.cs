using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SwimDeathMenu1 : MonoBehaviour {

	public string mainMenuLevel;

	public void RestartGame () {
		FindObjectOfType<SwimGameManager> ().ResetGame();
	}

	public void QuitToMainMenu() {
		SceneManager.LoadScene (mainMenuLevel);
		// Application.LoadLevel (mainMenuLevel);
	}
}
