using UnityEngine;
using System.Collections;

public class CoinGenerator : MonoBehaviour {

	public ObjectPooler coinPool;

	public float distanceBetweenCoins;

	public void SpawnCoins (int coinCount, Vector3 startPosition) {

		float xPosition = startPosition.x - (coinCount - 1) / 2;

		for (int i = 0; i < coinCount; i++) {
			GameObject coin2 = coinPool.GetPooledObject ();
			coin2.transform.position = new Vector3 (xPosition, startPosition.y, startPosition.z);
			coin2.SetActive (true);
			xPosition++;
		}
	}
}
