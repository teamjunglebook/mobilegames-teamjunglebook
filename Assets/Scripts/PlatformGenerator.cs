using UnityEngine;
using System.Collections;

public class PlatformGenerator : MonoBehaviour {

	private float platformWidth;
	private int platformSelector;

	private float[] platformWidths;

	private float minHeight;
	private float maxHeight;
	private float heightChange;

	//private FruitGenerator theCoinGenerator;

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
	public ObjectPooler theObstaclePool;

	public EnemyGenerator theEnemyGenerator;
	public ObstacleGenerator theObstacleGenerator;
	public CoinGenerator theCoinGenerator;
    private EnergyManager theEnergyManager;

    public float upPlatformThreshold;
	//public GameController controller;
	private int framecount;
	float randomH;

	// Use this for initialization
	void Start () {
		
		// platformWidth = thePlatform.GetComponent<BoxCollider2D> ().size.x;

		platformWidths = new float[theObjectPools.Length];

		for (int i = 0; i < theObjectPools.Length; i++) {
			platformWidths[i] = theObjectPools[i].pooledObject.GetComponent<BoxCollider2D> ().size.x;
		}

		minHeight = transform.position.y + 3;
		maxHeight = maxHeightPoint.position.y;

		//theCoinGenerator = FindObjectOfType<FruitGenerator> ();
		PlayerPrefs.SetFloat ("PlatformHeight", 0);
		PlayerPrefs.SetFloat ("FruitHeight", 0);
        //controller = new GameController ();

        theEnergyManager = FindObjectOfType<EnergyManager>();
    }
	
	// Update is called once per frame
	void Update () {
		
		if (transform.position.x < generationPoint.position.x) {

			// Generating Upper platforms
			if (Random.Range (0f, 100f) < upPlatformThreshold) {
				distanceBetween = Random.Range (distanceBetweenMin, distanceBetweenMax);
				platformSelector = Random.Range (0, theObjectPools.Length);
			
				heightChange = transform.position.y + Random.Range (maxHeightChange, -maxHeightChange);

				if (heightChange > maxHeight) {
					heightChange = maxHeight;
				}

				if (heightChange < minHeight) {
					heightChange = minHeight;
				}

				float finalHeightChange = 0.0f;
				while((Physics2D.OverlapArea(new Vector2(transform.position.x + distanceBetween - 0.5f, heightChange + finalHeightChange + 0.5f), new Vector2(transform.position.x + platformWidths[platformSelector]+0.5f, heightChange +finalHeightChange - 0.5f))))
					finalHeightChange += 0.5f;
				transform.position = new Vector3 (transform.position.x + (platformWidths [platformSelector] / 2) + distanceBetween, heightChange + finalHeightChange, transform.position.z);

				GameObject newPlatform = theObjectPools [platformSelector].GetPooledObject ();
				newPlatform.transform.position = transform.position;
				newPlatform.transform.rotation = transform.rotation;
				newPlatform.SetActive (true);

                if ((Random.Range(1, 100) < 15) && (heightChange + finalHeightChange >= maxHeight))
                {
                    theCoinGenerator.SpawnHeart(new Vector3(transform.position.x + (platformWidths[platformSelector] / 2) + 4f, transform.position.y + 1.5f, transform.position.z));
                }


				// Select which game object to spawn on ground
				switch (Random.Range (2, 3)) {

					// Enemy kill box
					case 1:
						theEnemyGenerator.spawnEnemy(new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), false);
						break;

					// Coins
				    case 2:
					    float coinCountNumber = Random.Range (2, 5);
					    // if(!(Physics2D.OverlapArea(new Vector2(transform.position.x + distanceBetween - 0.5f, heightChange + finalHeightChange + 0.5f), new Vector2(transform.position.x + platformWidths[platformSelector]+0.5f, heightChange +finalHeightChange - 0.5f))))
						// theCoinGenerator.SpawnCoins (coinCountNumber, new Vector3 (transform.position.x, transform.position.y + 1.0f, transform.position.z));
					    break;

					// Obstacles
					case 3:
						theObstacleGenerator.spawnObstacle (new Vector3 (transform.position.x, transform.position.y + 0.5f, transform.position.z));
						break;
				}

				// New Platform position
				transform.position = new Vector3 (transform.position.x + Random.Range(2f, 30f), transform.position.y, transform.position.z);
			}

		}
	}

	public float GetHeightChange() {
		return heightChange;
	}

	public void OnCollisionEnter2D (Collision2D other) {
		if (other.gameObject.tag == "fruit") {
			other.gameObject.SetActive (false);
		}
	}
}
