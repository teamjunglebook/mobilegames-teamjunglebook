using UnityEngine;
using System.Collections;

public class EnemyGenerator : MonoBehaviour {

	public EnemyPooler[] enemypoolers;

	public void spawnEnemy (Vector3 position, bool safeMode) {

		GameObject enemy = enemypoolers[Random.Range (0, enemypoolers.Length)].GetPooledObject();
		enemy.transform.position = position;
		enemy.SetActive (true);

		if (safeMode)
			enemy.tag = "safeenemy";
		else
			enemy.tag = "killbox";
	}
}
