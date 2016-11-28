using UnityEngine;
using System.Collections;

public class Powerups : MonoBehaviour {

	public bool doublePoints;
	public bool safeMode;
	public bool extraEnergy;
	public bool flyingMode;

	public float powerupLength;

	private PowerupManager thePowerupManager;

	public Sprite[] powerupSprites;

	// Use this for initialization
	void Start () {
		thePowerupManager = FindObjectOfType<PowerupManager> ();
	}

	void Awake () {
		int powerupSelector = Random.Range (0,3);

		switch (powerupSelector) {
			case 0:
				safeMode = true;
				break;
			case 1:
				flyingMode = true;
				break;
			case 2:
				extraEnergy = true;
				break;
			case 3:
				doublePoints = true;
				break;
		}

		GetComponent<SpriteRenderer> ().sprite = powerupSprites [powerupSelector];
	}

	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.name == "Player") {
			thePowerupManager.ActivatePowerup (safeMode, flyingMode, extraEnergy, doublePoints, powerupLength);
		}

		gameObject.SetActive (false);
	}
}
