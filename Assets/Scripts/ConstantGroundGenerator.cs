using UnityEngine;
using System.Collections;

public class ConstantGroundGenerator : MonoBehaviour {

	public float platformGapThreshold;
	public float powerupHeight;
	public Transform generationPoint;
	private FruitGenerator theFruitGenerator;
	public EnemyGenerator theEnemyGenerator;
	public ObstacleGenerator theObstacleGenerator;
	public ObjectPooler theConstantPlatformPool;
	public ObjectPooler powerupPool;
	private int fruitCount, enemyCount, obstacleCount, powerupCount;

	//public GameController controller;
	private Collider[] hitCollider;

	private PowerupManager thePowerupManager;

	void Start () {
		theFruitGenerator = FindObjectOfType<FruitGenerator> ();
		theEnemyGenerator = FindObjectOfType<EnemyGenerator> ();
		theObstacleGenerator = FindObjectOfType<ObstacleGenerator> ();
		//controller = new GameController ();
		fruitCount = enemyCount = obstacleCount = powerupCount = 0;

		thePowerupManager = FindObjectOfType<PowerupManager> ();
	}
		
	void Update () {
		
		if (transform.position.x < generationPoint.position.x) {

			float gap = 0;
			if (Random.Range (0f, 100f) < platformGapThreshold) {
				gap = Random.Range (1f, 5f);
			}
			// Generating Constant Ground
			transform.position = new Vector3 (transform.position.x + 5 + gap, transform.position.y, transform.position.z);
			GameObject constantPlatform = theConstantPlatformPool.GetPooledObject ();
			constantPlatform.transform.position = transform.position;
			constantPlatform.transform.rotation = transform.rotation;
			constantPlatform.SetActive (true);

			// Select which game object to spawn on ground
			switch (Random.Range (1,6)) {
				//Fruits
			case 1:
				fruitCount++;
				if (fruitCount <= 2) {
					float randomHeight = Random.Range (1f, 6f);
					theFruitGenerator.SpawnCoins (Random.Range (2, 6), new Vector3 (transform.position.x, transform.position.y + randomHeight, transform.position.z));
				} else
					fruitCount = 0;
				break;

				// Enemy kill box
			case 2:
				enemyCount++;
				if (enemyCount <= 2) {
					theEnemyGenerator.spawnEnemy (new Vector3 (transform.position.x, transform.position.y + 0.5f, transform.position.z), thePowerupManager.GetSafeMode());
				}
				else
					enemyCount = 0;
				break;

				// Obstacles
			case 3:
				obstacleCount++;
				if (obstacleCount <= 2)
					theObstacleGenerator.spawnObstacle (new Vector3 (transform.position.x, transform.position.y + 0.5f, transform.position.z));
				else
					obstacleCount=0;
				break;

				// Powerups
			case 4:
				powerupCount++;
				if (powerupCount <= 2) {
					GameObject newPowerup = powerupPool.GetPooledObject ();

					newPowerup.transform.position = transform.position + new Vector3 (transform.position.x, Random.Range (1f, powerupHeight), 0f);
					//float finalMaxHeight = 0.0f;
					if (!(Physics2D.OverlapArea (new Vector2 (newPowerup.transform.position.x - 0.5f, newPowerup.transform.position.y + 0.5f), new Vector2 (newPowerup.transform.position.x + 0.5f, newPowerup.transform.position.y  + 0.5f))))
						newPowerup.SetActive (true);
				} else
					powerupCount = 0;
				break;
			}
		}
	}
}
