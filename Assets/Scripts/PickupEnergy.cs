using UnityEngine;
using System.Collections;

public class PickupEnergy : MonoBehaviour {

	public int energyToGive;

	private EnergyManager theEnergyManager;

	private AudioSource energySound;

	// Use this for initialization
	void Start () {
		theEnergyManager = FindObjectOfType<EnergyManager> ();

		energySound = GameObject.Find ("CoinSound").GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D (Collider2D other) {
		if (other.gameObject.name == "Player") {
			theEnergyManager.AddEnergy (energyToGive);
			gameObject.SetActive (false);

			if (energySound.isPlaying) {
				energySound.Stop ();
				energySound.Play ();
			}
			else {
				energySound.Play ();
			}
		}
	}
}
