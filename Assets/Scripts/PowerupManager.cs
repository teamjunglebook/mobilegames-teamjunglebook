using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PowerupManager : MonoBehaviour {

	private bool doublePoints;
	private bool safeMode;
	private bool extraEnergy;
	private bool flyingMode;
	private bool powerupActive;

	private float powerupLengthCounter;

	private ScoreManager theScoreManager;
	//private ConstantGroundGenerator thePlatformGenerator;
	private EnergyManager theEnergyManager;
	private GameManager theGameManager;

	public GameObject thePlayer;

	private float normalPointsPerSecond;
	private RigidbodyConstraints2D normalConstraints;

	private PlatformDestroyer[] spikeList;

	public AudioSource ballooForm;
	public AudioSource flyingForm;

	public Text powerupTimer;

	// Use this for initialization
	void Start () {
		theScoreManager = FindObjectOfType<ScoreManager> ();
		//thePlatformGenerator = FindObjectOfType<ConstantGroundGenerator> ();
		theEnergyManager = FindObjectOfType<EnergyManager> ();
		theGameManager = FindObjectOfType<GameManager> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (powerupActive) {
			//Modify this with - sign.
			powerupLengthCounter -= Time.deltaTime;

			if (theGameManager.powerupReset) {
				powerupLengthCounter = 0;
				theGameManager.powerupReset = false;
			}
				
			if (safeMode) {
				//thePlatformGenerator.randomSpikeThreshold = 0f;
			}

			if (flyingMode) {
				if (Input.GetKeyDown (KeyCode.Space) || Input.GetMouseButtonDown (0)) {
					powerupLengthCounter = 0;
				}
			}

			if (extraEnergy) {
				theEnergyManager.AddEnergy (20);
				extraEnergy = false;
                powerupTimer.text = "";
            }

			if (doublePoints) {
                //theScoreManager.scoreCount += 200;
                powerupTimer.text = "";
            }


			if (powerupLengthCounter <= 0) {
				theScoreManager.pointsPerSecond = normalPointsPerSecond;
				thePlayer.GetComponent<Rigidbody2D> ().constraints = normalConstraints;
				theEnergyManager.energyDecreasing = true;
				spikeList = FindObjectsOfType<PlatformDestroyer> ();
				for (int i = 0; i < spikeList.Length; i++) {
					if (spikeList [i].gameObject.name.Contains ("KingLouie") ||
					    spikeList [i].gameObject.name.Contains ("Snake") ||
					    spikeList [i].gameObject.name.Contains ("Hyna")) {
						//spikeList [i].gameObject.SetActive (false);
						spikeList [i].gameObject.tag = "killbox";
					}
				}

				safeMode = false;
				flyingMode = false;

				powerupActive = false;
				powerupTimer.text = "";
			} else {

				if (flyingMode || safeMode)
					powerupTimer.text = ((int)(powerupLengthCounter) + 1).ToString();
			}
		}
	}

	public void ActivatePowerup (bool safe, bool flying, bool extra, bool points, float time) {
		safeMode = safe;
		flyingMode = flying;
		extraEnergy = extra;
		doublePoints = points;

		powerupLengthCounter = time;

		normalPointsPerSecond = theScoreManager.pointsPerSecond;
		normalConstraints = thePlayer.GetComponent<Rigidbody2D> ().constraints;

		if (safeMode) {
			ballooForm.Play ();
			spikeList = FindObjectsOfType<PlatformDestroyer> ();
			for (int i = 0; i < spikeList.Length; i++) {
				if (spikeList [i].gameObject.name.Contains ("KingLouie") ||
				    spikeList [i].gameObject.name.Contains ("Snake") ||
				    spikeList [i].gameObject.name.Contains ("Hyna")) {
					//spikeList [i].gameObject.SetActive (false);
					spikeList [i].gameObject.tag = "safeenemy";
				}
			}
		} 
		else if (flyingMode) {
			flyingForm.Play ();
			thePlayer.transform.position = new Vector3 (thePlayer.transform.position.x , 3.3f, thePlayer.transform.position.z);
			thePlayer.GetComponent<Rigidbody2D> ().constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;

			theEnergyManager.energyDecreasing = false;
		}
		powerupActive = true;

	}

	public bool GetSafeMode() {
		return safeMode;
	}

	public bool GetFlyingMode() {
		return flyingMode;
	}
}
