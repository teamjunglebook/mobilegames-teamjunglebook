using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SwimDeathMenu : MonoBehaviour {

	public string mainMenuLevel;

	public void RestartGame () {
		FindObjectOfType<SwimGameManager> ().ResetGame();
	}

	public void QuitToMainMenu() {
		GameController.clearArguments ();
		SceneManager.LoadScene (mainMenuLevel);
		// Application.LoadLevel (mainMenuLevel);
	}
}
