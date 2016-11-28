using UnityEngine;
using System.Collections;

public class SwimEnemyGenerator : MonoBehaviour {

	public SwimEnemyPooler[] enemypoolers;

	public void spawnEnemy (Vector3 position) {

		GameObject enemy = enemypoolers[Random.Range (0, enemypoolers.Length)].GetPooledObject();
		enemy.transform.position = position;
		enemy.SetActive (true);
	}
}
