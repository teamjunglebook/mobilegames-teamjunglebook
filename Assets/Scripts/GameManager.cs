using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	private Vector3 platformStartPoint;
	private Vector3 constantPlatformStartPoint;
	private Vector3 playerStartPoint;
	private PlatformDestroyer[] platformList;

	private ScoreManager theScoreManager;
	private EnergyManager theEnergyManager;

	public PlayerController thePlayer;
	public Transform platformGenerator;
	public Transform constantPlatformGenerator;

	public DeathMenu theDeathScreen;

	public AudioSource backgroundSound;

	public bool powerupReset;

	// Use this for initialization
	void Start () {
		platformStartPoint = platformGenerator.position;
		constantPlatformStartPoint = constantPlatformGenerator.position;
		playerStartPoint = thePlayer.transform.position;

		theScoreManager = FindObjectOfType<ScoreManager> ();
		theEnergyManager = FindObjectOfType<EnergyManager> ();

		backgroundSound = GameObject.Find ("TheJungleBook").GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void RestartGame () {

		theScoreManager.scoreIncreasing = false;
		theEnergyManager.energyDecreasing = false;

		thePlayer.gameObject.SetActive (false);

		theDeathScreen.gameObject.SetActive (true);

		// StartCoroutine ("RestartGameCo");
	}

	public void ResetGame() {
		
		theDeathScreen.gameObject.SetActive (false);

		platformList = FindObjectsOfType<PlatformDestroyer> ();
		for (int i = 0; i < platformList.Length; i++) {
			platformList [i].gameObject.SetActive (false);
		}

		thePlayer.transform.position = playerStartPoint;
		platformGenerator.position = platformStartPoint;
		constantPlatformGenerator.position = constantPlatformStartPoint;
		thePlayer.gameObject.SetActive (true);

		theScoreManager.scoreCount = 0;
		theScoreManager.scoreIncreasing = true;
		theEnergyManager.energyCount = 100;
		theEnergyManager.energyDecreasing = true;

		powerupReset = true;

		backgroundSound.Play ();
	}

	// Restart Game Co-routine
	/*
	 * public IEnumerator RestartGameCo () {

		theScoreManager.scoreIncreasing = false;
		theEnergyManager.energyDecreasing = false;

		thePlayer.gameObject.SetActive (false);
		yield return new WaitForSeconds (1f);

		platformList = FindObjectsOfType<PlatformDestroyer> ();
		for (int i = 0; i < platformList.Length; i++) {
			platformList [i].gameObject.SetActive (false);
		}

		thePlayer.transform.position = playerStartPoint;
		platformGenerator.position = platformStartPoint;
		thePlayer.gameObject.SetActive (true);

		theScoreManager.scoreCount = 0;
		theScoreManager.scoreIncreasing = true;
		theEnergyManager.energyCount = 100;
		theEnergyManager.energyDecreasing = true;
	}
	*/
}
