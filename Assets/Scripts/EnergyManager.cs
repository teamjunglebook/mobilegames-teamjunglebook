using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnergyManager : MonoBehaviour {

	public Text energyText;

	public float energyCount;

	public float energyPerSecond;

	public bool energyDecreasing;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (energyDecreasing) {
			energyCount -= energyPerSecond * Time.deltaTime;
		}

		energyText.text = "Energy: " + Mathf.Round (energyCount);
	}

	public void AddEnergy (int energyToAdd) {
		energyCount += energyToAdd;

		if (energyCount > 100) {
			energyCount = 100;
		}
	}
}
