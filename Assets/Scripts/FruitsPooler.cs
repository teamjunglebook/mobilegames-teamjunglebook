using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FruitsPooler : MonoBehaviour {

	public GameObject pooledFruit;
	List<GameObject> pooledFruits;
	public int totalFruits;

	// Use this for initialization
	void Start () {
		pooledFruits = new List<GameObject> ();

		for (int i = 0; i < totalFruits; i++) {
			GameObject obj = (GameObject) Instantiate (pooledFruit);
			obj.SetActive (false);
			pooledFruits.Add (obj);
		}
	}

	public GameObject GetPooledObject () {
		for (int i = 0; i < pooledFruits.Count; i++) {
			if (!pooledFruits[i].activeInHierarchy) {
				return pooledFruits[i];
			}
		}

		GameObject obj = (GameObject) Instantiate (pooledFruit);
		obj.SetActive (false);
		pooledFruits.Add (obj);

		return obj;
	}


}
