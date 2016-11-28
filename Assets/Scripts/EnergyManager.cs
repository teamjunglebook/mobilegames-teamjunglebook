using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnergyManager : MonoBehaviour {

	public Text energyText;

	public float energyCount;

	public float energyPerSecond;

	public bool energyDecreasing;

	public PlayerController thePlayerController;

	public RectTransform energyTransform;
	private float cachedY;
	private float minXPos;
	private float maxXPos;
	public Image visualEnergy;

	// Use this for initialization
	void Start () {

		if (GameController.getEnergy () != 0) {
			energyCount = GameController.getEnergy ();
		}
		cachedY = energyTransform.position.y;
		maxXPos = energyTransform.position.x;
		minXPos = energyTransform.position.x - energyTransform.rect.width;
	}
	
	// Update is called once per frame
	void Update () {
		//Modified
		if (energyDecreasing) {
			energyCount -= energyPerSecond * Time.deltaTime;
		}

		if (energyCount <= 0) {
            int hearts = PlayerPrefs.GetInt("Hearts");
            if (hearts > 1)
            {
                PlayerPrefs.SetInt("Hearts", (hearts - 1));
                energyCount += 50;
            }
            else
            {
                energyCount = 0;
                thePlayerController.killPlayer();
            }
		}

		energyText.text = Mathf.Round (energyCount) + "%";
		handleEnergyBar (energyCount);
	}

	public void AddEnergy (int energyToAdd) {
		energyCount += energyToAdd;

		if (energyCount > 100) {
			energyCount = 100;
		}
	}

	private float MapEnergy(float x, float inMin, float inMax, float outMin, float outMax) {
		return (x - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
	}

	private void handleEnergyBar (float currentEnergyValue) {
		float currentXValue = MapEnergy (currentEnergyValue, 0, 100, minXPos, maxXPos);
		energyTransform.position = new Vector3 (currentXValue, cachedY);

		if (currentEnergyValue > 50) {
			visualEnergy.color = new Color32 ((byte)MapEnergy(currentEnergyValue, 50, 100, 255, 0), 255, 0, 255);
		} else {
			visualEnergy.color = new Color32 (255, (byte)MapEnergy(currentEnergyValue, 0, 50, 0, 255), 0, 255);
		}
	}
}
