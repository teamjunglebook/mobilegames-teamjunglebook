using UnityEngine;
using System.Collections;

public class FruitGenerator : MonoBehaviour {

	public FruitsPooler[] fruitpoolers;

	public float distanceBetweenCoins;

	public void SpawnCoins (int coinCount, Vector3 startPosition) {

		float xPosition = startPosition.x - (coinCount - 1) / 2;
		float yPosition = startPosition.y;

		//float finalHeightChange = 0.0f;
		while ((Physics2D.OverlapArea (new Vector2 (xPosition - 0.7f, yPosition + 0.5f), new Vector2 (xPosition + coinCount + 0.7f, yPosition - 0.5f)))) {
			yPosition += 0.5f;
		
		}
		for (int i = 0; i < coinCount; i++) {
			GameObject coin2 = fruitpoolers [Random.Range (0, fruitpoolers.Length - 1 )].GetPooledObject ();
			coin2.transform.position = new Vector3 (xPosition, yPosition, startPosition.z);
			coin2.SetActive (true);
			xPosition++;
		}
	}
}
