using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

	private Rigidbody2D myRigidbody;
	//private Collider2D myCollider;
	private Animator myAnimator;

	private float jumpTimeCounter;

	private bool stoppedJumping;
	private bool doubleJump;

	private float speedMilestoneCount;

	private float moveSpeedStore;
	private float speedMilestoneCountStore;
	private float speedupMilestoneStore;

	public bool grounded;
	public LayerMask whatIsGround;
	public Transform groundCheck;
	public float groundCheckRadius;

	public float moveSpeed;
	public float speedMultiplier;
	public float jumpForce;
	public float speedupMilestone;

	public float jumpTime;

	public GameManager theGameManager;

	public AudioSource jumpSound;
	public AudioSource deathSound1;
	public AudioSource deathSound2;
	public AudioSource backgroundSound;
	public AudioSource killEnemy;
	public AudioSource powerDown;

	public int energyToSubtract;
	private EnergyManager theEnergyManager;

	private PowerupManager thePowerupManager;

	private ScoreManager theScoremanager;

	public string level2;

	public int coinValue;

	public GameObject directionalLight;

	// Use this for initialization
	void Start () {
		myRigidbody = GetComponent<Rigidbody2D> ();
		//myCollider = GetComponent<Collider2D> ();
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

		jumpTimeCounter = jumpTime;

		speedMilestoneCount = speedupMilestone;

		moveSpeedStore = moveSpeed;
		speedMilestoneCountStore = speedMilestoneCount;
		speedupMilestoneStore = speedupMilestone;

		stoppedJumping = true;

		theEnergyManager = FindObjectOfType<EnergyManager> ();
		thePowerupManager = FindObjectOfType<PowerupManager> ();
		theScoremanager = FindObjectOfType<ScoreManager> ();
	}
	
	// Update is called once per frame
	void Update () {

		// grounded = Physics2D.IsTouchingLayers (myCollider, whatIsGround);

		//directionalLight.GetComponent<Light>().intensity = (float)(2.0 - transform.position.x * 0.005);

		grounded = Physics2D.OverlapCircle (groundCheck.position, groundCheckRadius, whatIsGround);

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

            /*theGameManager.RestartGame ();
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
			theEnergyManager.AddEnergy (energyToSubtract);
			powerDown.Play ();
			other.gameObject.SetActive (false);
			BlinkPlayer ();
		}
		else if (other.gameObject.tag == "safeenemy") {
			killEnemy.Play ();
			other.gameObject.SetActive (false);
		}
		else if (other.gameObject.tag == "TeleportWaterWorld") {
			GameController.setArguments (moveSpeed, theScoremanager.scoreCount, theEnergyManager.energyCount, speedupMilestone, backgroundSound.time);
			SceneManager.LoadScene (level2);
			// Application.LoadLevel (level2);
		}
		else if (other.gameObject.tag == "heart") {
            int heart = 0;
            if (PlayerPrefs.HasKey("Hearts")) {
                heart = PlayerPrefs.GetInt("Hearts");
            }
            int newHearts = heart + 1;
            if (newHearts >= 5) {
                newHearts = 5;
            }
            PlayerPrefs.SetInt("Hearts", newHearts);
            other.gameObject.SetActive (false);
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
			if (grounded) {
				myRigidbody.velocity = new Vector2 (myRigidbody.velocity.x, jumpForce);
				stoppedJumping = false;
				jumpSound.Play ();
			}

			if (!grounded && doubleJump) {
				myRigidbody.velocity = new Vector2 (myRigidbody.velocity.x, jumpForce);
				jumpTimeCounter = jumpTime;
				doubleJump = false;
				stoppedJumping = false;
				jumpSound.Play ();
			}
		}

		if ((Input.GetKey (KeyCode.Space) || Input.GetMouseButton (0)) && !stoppedJumping) {
			if (jumpTimeCounter > 0) {
				myRigidbody.velocity = new Vector2 (myRigidbody.velocity.x, jumpForce);
				jumpTimeCounter -= Time.deltaTime;
			}
		}

		if (Input.GetKeyUp (KeyCode.Space) || Input.GetMouseButtonUp (0)) {
			jumpTimeCounter = 0;
			stoppedJumping = true;
		}

		if (grounded) {
			jumpTimeCounter = jumpTime;
			doubleJump = true;
		}

		myAnimator.SetFloat ("Speed", myRigidbody.velocity.x);
		myAnimator.SetBool ("Grounded", grounded);
		myAnimator.SetBool ("SafeMode", thePowerupManager.GetSafeMode());
		myAnimator.SetBool ("FlyingMode", thePowerupManager.GetFlyingMode());
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
