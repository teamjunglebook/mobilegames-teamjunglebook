using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour {

	public string mainMenuLevel;

	public void RestartGame () {
		FindObjectOfType<GameManager> ().ResetGame();
	}

	public void QuitToMainMenu() {
		GameController.clearArguments ();
		SceneManager.LoadScene (mainMenuLevel);
		// Application.LoadLevel (mainMenuLevel);
	}
}
