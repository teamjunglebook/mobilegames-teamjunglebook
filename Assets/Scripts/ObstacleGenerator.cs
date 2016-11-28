using UnityEngine;
using System.Collections;

public class ObstacleGenerator : MonoBehaviour {

	public ObstaclePooler[] obstaclepoolers;

	public void spawnObstacle (Vector3 position) {

		GameObject obstacle = obstaclepoolers [Random.Range (0, obstaclepoolers.Length)].GetPooledObject ();
		obstacle.transform.position = position;
		obstacle.SetActive (true);
	}
}
