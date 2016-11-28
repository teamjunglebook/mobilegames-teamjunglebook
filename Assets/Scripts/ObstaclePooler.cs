using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObstaclePooler : MonoBehaviour {

	public GameObject pooledObstacle;
	List<GameObject> pooledObstacles;
	public int totalFruits;

	// Use this for initialization
	void Start () {
		pooledObstacles = new List<GameObject> ();

		for (int i = 0; i < totalFruits; i++) {
			GameObject obj = (GameObject) Instantiate (pooledObstacle);
			obj.SetActive (false);
			pooledObstacles.Add (obj);
		}
	}

	public GameObject GetPooledObject () {
		for (int i = 0; i < pooledObstacles.Count; i++) {
			if (!pooledObstacles[i].activeInHierarchy) {
				return pooledObstacles[i];
			}
		}

		GameObject obj = (GameObject) Instantiate (pooledObstacle);
		obj.SetActive (false);
		pooledObstacles.Add (obj);

		return obj;
	}

}
