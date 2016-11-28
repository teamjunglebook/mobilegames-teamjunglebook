using UnityEngine;
using System.Collections;

public class PickupOxygen : MonoBehaviour {

	public int energyToGive;

	private OxygenManager theEnergyManager;

	private AudioSource energySound;

	// Use this for initialization
	void Start () {
		theEnergyManager = FindObjectOfType<OxygenManager> ();

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
