using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerSwimController : MonoBehaviour {

	public string level1;
	private Rigidbody2D myRigidbody;
	//private Collider2D myCollider;
	private Animator myAnimator;

	private float speedMilestoneCount;

	private float moveSpeedStore;
	private float speedMilestoneCountStore;
	private float speedupMilestoneStore;

	public float moveSpeed;
	public float speedMultiplier;
	public float jumpForce;
	public float speedupMilestone;

	public int energyToSubtract;

	public float jumpTime;

	public SwimGameManager theGameManager;
	private ScoreManager theScoremanager;

	public AudioSource deathSound1;
	public AudioSource backgroundSound;
	public AudioSource powerDown;

	private OxygenManager theOxygenManager;

	public GameObject thePlayer;

	public GameObject pipe;

	private int pipeCount;

	// Use this for initialization
	void Start () {
		myRigidbody = GetComponent<Rigidbody2D> ();
		myAnimator = GetComponent<Animator> ();

		if (GameController.getVelocity () != 0) {
			moveSpeed = GameController.getVelocity ();
		}

		if (GameController.getNextMilestone () != 0) {
			speedupMilestone = GameController.getNextMilestone ();
		}

		if (GameController.getNextMilestone () != 0) {
			backgroundSound.time = GameController.getBgSoundTime ();
		} 

		speedMilestoneCount = speedupMilestone;

		moveSpeedStore = moveSpeed;
		speedMilestoneCountStore = speedMilestoneCount;
		speedupMilestoneStore = speedupMilestone;

		theOxygenManager = FindObjectOfType<OxygenManager> ();
		theScoremanager = FindObjectOfType<ScoreManager> ();

		pipeCount = 1;
	}
	
	// Update is called once per frame
	void Update () {

		if ((transform.position.x >= (100 * pipeCount)) && (transform.position.x <= (100 * pipeCount + 10))) {
			pipe.transform.position = new Vector3 (transform.position.x + 20, pipe.transform.position.y, pipe.transform.position.z);
			pipe.SetActive (true);
			pipeCount++;
		}

		if (transform.position.x > speedMilestoneCount) {
			speedMilestoneCount += speedupMilestone;
			speedupMilestone = speedupMilestone * speedMultiplier;
			moveSpeed = moveSpeed * speedMultiplier;
		}

		myRigidbody.velocity = new Vector2 (moveSpeed, myRigidbody.velocity.y);

		JumpControl ();
	}

	void OnCollisionEnter2D (Collision2D other) {
		if (other.gameObject.tag == "killbox") {
            /*
            theGameManager.RestartGame ();
			moveSpeed = moveSpeedStore;
			speedMilestoneCount = speedMilestoneCountStore;
			speedupMilestone = speedupMilestoneStore;

			deathSound1.Play ();
			//deathSound2.Play ();
			backgroundSound.Stop ();
            */
            deathSound1.Play();
            int heart = 0;
            if (PlayerPrefs.HasKey("Hearts"))
            {
                heart = PlayerPrefs.GetInt("Hearts");
            }
            int newHearts = heart - 1;
            if (newHearts <= 0)
            {
                newHearts = 0;
                theGameManager.RestartGame();
                moveSpeed = moveSpeedStore;
                speedMilestoneCount = speedMilestoneCountStore;
                speedupMilestone = speedupMilestoneStore;

                backgroundSound.Stop();
            }
            PlayerPrefs.SetInt("Hearts", newHearts);
            other.gameObject.SetActive(false);
            BlinkPlayer();
        }
		else if (other.gameObject.tag == "obstacle") {
			theOxygenManager.AddEnergy (energyToSubtract);
			powerDown.Play ();
			other.gameObject.SetActive (false);
			BlinkPlayer ();
		}
		else if (other.gameObject.tag == "obstacleplatform") {
			if (powerDown.isPlaying) {
				powerDown.Stop ();
				powerDown.Play ();
			}
			else {
				powerDown.Play ();
			}

			theOxygenManager.AddEnergy (energyToSubtract);
			BlinkPlayer ();
			thePlayer.transform.position = new Vector3 (thePlayer.transform.position.x , thePlayer.transform.position.y + 0.5f, thePlayer.transform.position.z);
		} 
		else if (other.gameObject.tag == "TeleportJungleWorld") {
			GameController.setArguments (moveSpeed, theScoremanager.scoreCount, theOxygenManager.energyCount, speedupMilestone, backgroundSound.time);
			SceneManager.LoadScene (level1);
			// Application.LoadLevel (level1);
		}
        else if (other.gameObject.tag == "heart")
        {
            int heart = 0;
            if (PlayerPrefs.HasKey("Hearts"))
            {
                heart = PlayerPrefs.GetInt("Hearts");
            }
            int newHearts = heart + 1;
            if (newHearts >= 5)
            {
                newHearts = 5;
            }
            PlayerPrefs.SetInt("Hearts", newHearts);
            other.gameObject.SetActive(false);
        }
    }

	public void killPlayer() {
		theGameManager.RestartGame ();
		moveSpeed = moveSpeedStore;
		speedMilestoneCount = speedMilestoneCountStore;
		speedupMilestone = speedupMilestoneStore;

		//deathSound1.Play ();
		//deathSound2.Play ();
		backgroundSound.Stop ();
	}

	public void JumpControl () {
		if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) {
			myRigidbody.velocity = new Vector2 (myRigidbody.velocity.x, jumpForce);
		}

		myAnimator.SetFloat ("Speed", myRigidbody.velocity.x);
	}

	void BlinkPlayer () {
		StartCoroutine(DoBlinks(5, 0.2f));
	}

	IEnumerator DoBlinks(int duration, float blinkTime) {
		int blinkColor = 255;
		SpriteRenderer tempSprite  = gameObject.GetComponent<SpriteRenderer>();
		while (duration > 0f) {
			duration -= 1;

			blinkColor = (blinkColor == 255) ? 50 : 255;

			//toggle renderer
			tempSprite.color = new Color32(255, 255, 255, (byte)blinkColor);

			//wait for a bit
			yield return new WaitForSeconds(blinkTime);
		}

		//make sure renderer is enabled when we exit
		tempSprite.color = new Color32(255, 255, 255, 255);
	}
}
