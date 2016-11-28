using UnityEngine;
using System.Collections;

public class ScrollBackground : MonoBehaviour {

	public float bgScrollSpeed;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 offset = new Vector2 (Time.time * bgScrollSpeed, 0);
		GetComponent<Renderer> ().material.mainTextureOffset = offset;
	}
}
