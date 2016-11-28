using UnityEngine;
using System.Collections;

public class SwimGameManager : MonoBehaviour {

	private Vector3 platformStartPoint;
	private Vector3 playerStartPoint;
	private PlatformDestroyer[] platformList;

	private ScoreManager theScoreManager;
	private OxygenManager theEnergyManager;

	public PlayerSwimController thePlayer;
	public Transform platformGenerator;

	public SwimDeathMenu theDeathScreen;

	public AudioSource backgroundSound;

	public bool powerupReset;

	// Use this for initialization
	void Start () {
		platformStartPoint = platformGenerator.position;
		playerStartPoint = thePlayer.transform.position;

		theScoreManager = FindObjectOfType<ScoreManager> ();
		theEnergyManager = FindObjectOfType<OxygenManager> ();

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
		thePlayer.gameObject.SetActive (true);

		theScoreManager.scoreCount = 0;
		theScoreManager.scoreIncreasing = true;
		theEnergyManager.energyCount = 100;
		theEnergyManager.energyDecreasing = true;

		powerupReset = true;

		backgroundSound.Play ();
	}
}
