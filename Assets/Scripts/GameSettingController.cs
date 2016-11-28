using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameSettingController : MonoBehaviour {

	public Slider volumeCotrol;

	// Use this for initialization
	void Start () {
		AudioListener.volume = PlayerPrefs.GetFloat ("volume");
		volumeCotrol.value = PlayerPrefs.GetFloat ("volume");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void VolumeControl() {
		PlayerPrefs.SetFloat ("volume", volumeCotrol.value);
		AudioListener.volume = volumeCotrol.value;
	}
}
