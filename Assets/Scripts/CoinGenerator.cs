using UnityEngine;
using System.Collections;

public class CoinGenerator : MonoBehaviour {

	public ObjectPooler coinPooler;

	public void SpawnCoins (int coinCount, Vector3 startPosition) {

		float xPosition = startPosition.x - (coinCount - 1) / 2;

		for (int i = 0; i < coinCount; i++) {
			GameObject coin = coinPooler.GetPooledObject ();
			coin.transform.position = new Vector3 (xPosition, startPosition.y, startPosition.z);
			coin.SetActive (true);
			xPosition++;
		}
	}

    public void SpawnHeart(Vector3 startPosition)
    {
        GameObject heart = coinPooler.GetPooledObject();
        heart.transform.position = startPosition;
        heart.SetActive(true);
    }
}
