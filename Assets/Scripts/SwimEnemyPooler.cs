using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SwimEnemyPooler : MonoBehaviour {

	public GameObject pooledEnemy;
	List<GameObject> pooledEnemies;
	public int totalEnemies;

	// Use this for initialization
	void Start () {
		pooledEnemies = new List<GameObject> ();

		for (int i = 0; i < totalEnemies; i++) {
			GameObject obj = (GameObject) Instantiate (pooledEnemy);
			obj.SetActive (false);
			pooledEnemies.Add (obj);
		}
	}

	public GameObject GetPooledObject () {
		for (int i = 0; i < pooledEnemies.Count; i++) {
			if (!pooledEnemies[i].activeInHierarchy) {
				return pooledEnemies[i];
			}
		}

		GameObject obj = (GameObject) Instantiate (pooledEnemy);
		obj.SetActive (false);
		pooledEnemies.Add (obj);

		return obj;
	}
}
