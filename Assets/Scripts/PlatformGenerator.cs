using UnityEngine;
using System.Collections;

public class PlatformGenerator : MonoBehaviour {

	private float platformWidth;
	private int platformSelector;

	private float[] platformWidths;

	private float minHeight;
	private float maxHeight;
	private float heightChange;

	private CoinGenerator theCoinGenerator;

	public Transform maxHeightPoint;

	public ObjectPooler[] theObjectPools;

	//public GameObject[] thePlatforms;

	public GameObject thePlatform;
	public Transform generationPoint;
	public float distanceBetween;
	public float distanceBetweenMin;
	public float distanceBetweenMax;

	public float maxHeightChange;

	public float randomCoinThreshold;

	public float randomSpikeThreshold;
	public ObjectPooler theSpikePool;

	// Use this for initialization
	void Start () {
		
		// platformWidth = thePlatform.GetComponent<BoxCollider2D> ().size.x;

		platformWidths = new float[theObjectPools.Length];

		for (int i = 0; i < theObjectPools.Length; i++) {
			platformWidths[i] = theObjectPools[i].pooledObject.GetComponent<BoxCollider2D> ().size.x;
		}

		minHeight = transform.position.y;
		maxHeight = maxHeightPoint.position.y;

		theCoinGenerator = FindObjectOfType<CoinGenerator> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.x < generationPoint.position.x) {
			distanceBetween = Random.Range (distanceBetweenMin, distanceBetweenMax);

			platformSelector = Random.Range (0, theObjectPools.Length);

			heightChange = transform.position.y + Random.Range (maxHeightChange, -maxHeightChange);

			if (heightChange > maxHeight) {
				heightChange = maxHeight;
			}

			if (heightChange < minHeight) {
				heightChange = minHeight;
			}

			transform.position = new Vector3 (transform.position.x + (platformWidths[platformSelector] / 2) + distanceBetween, heightChange, transform.position.z);

			// Instantiate (thePlatforms[platformSelector], transform.position, transform.rotation);

			GameObject newPlatform = theObjectPools[platformSelector].GetPooledObject();

			newPlatform.transform.position = transform.position;
			newPlatform.transform.rotation = transform.rotation;
			newPlatform.SetActive (true);

			if (Random.Range (0f, 100f) < randomCoinThreshold) {
				theCoinGenerator.SpawnCoins (Random.Range(1,4), new Vector3 (transform.position.x, transform.position.y + 1f, transform.position.z));
			}

			if (Random.Range (0f, 100f) < randomSpikeThreshold) {
				GameObject newSpike = theSpikePool.GetPooledObject ();

				float spikeXPosition = Random.Range (-platformWidths[platformSelector] / 2f + 1f, platformWidths[platformSelector] / 2f - 1f);

				Vector3 spikePosition = new Vector3 (spikeXPosition, 0.5f, 0f);
				newSpike.transform.position = transform.position + spikePosition;
				newSpike.transform.rotation = transform.rotation;
				newSpike.SetActive (true);
			}

			transform.position = new Vector3 (transform.position.x + (platformWidths[platformSelector] / 2), transform.position.y, transform.position.z);

			if (Random.Range (0f, 100f) < randomCoinThreshold) {
				theCoinGenerator.SpawnCoins (Random.Range(1,2), new Vector3 (transform.position.x, transform.position.y + 2f, transform.position.z));
			}
		}
	}
}
