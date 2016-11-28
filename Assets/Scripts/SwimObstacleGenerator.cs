using UnityEngine;
using System.Collections;

public class SwimObstacleGenerator : MonoBehaviour {

	private FruitGenerator theOxygenGenerator;
	private SwimEnemyGenerator theSwimEnemyGenerator;
	private SwimFriendGenerator theSwimFriendGenerator;
    private  HeartGenerator theHeartGenerator;

	public Transform generationPoint;

	// Use this for initialization
	void Start () {
		theOxygenGenerator = FindObjectOfType<FruitGenerator> ();
		theSwimEnemyGenerator = FindObjectOfType<SwimEnemyGenerator> ();
		theSwimFriendGenerator = FindObjectOfType<SwimFriendGenerator> ();
        theHeartGenerator = FindObjectOfType<HeartGenerator> ();

		// 24 Oct 2016
		generationPoint.position = new Vector3 (generationPoint.position.x + 5, generationPoint.position.y, generationPoint.position.z);
	}

	// Update is called once per frame
	void Update () {

		float lastPositionX;

		if (transform.position.x < generationPoint.position.x) {

			switch(Random.Range(0, 4)) {

				// Generating Obstacles
				case 0:
					theSwimFriendGenerator.spawnObstacle (transform.position);
					break;

				// Generating Coins / Fruits
				case 1:
				theOxygenGenerator.SpawnCoins (Random.Range (1, 4), transform.position);
					break;

				// Generating Spikes / Enemies
				case 2:
					theSwimEnemyGenerator.spawnEnemy (transform.position);
					break;
                
                // Generate Hearts
                case 3:
                    if (Random.Range(1,100) < 15)
                    {
                        theHeartGenerator.SpawnHeart(transform.position);
                    }
                    break;
			}

			lastPositionX = transform.position.x;
			float newX = Random.Range (3f, 10f);
			float newY = Random.Range (-3.5f, 3.5f);;

			if (lastPositionX <= newX) {
				newX = 0;
			}
			// New Platform position
			transform.position = new Vector3 (transform.position.x + newX, newY, transform.position.z);
		}
	}
}
