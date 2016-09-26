﻿using UnityEngine;
using System.Collections;

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

	// Use this for initialization
	void Start () {
		myRigidbody = GetComponent<Rigidbody2D> ();
		//myCollider = GetComponent<Collider2D> ();
		myAnimator = GetComponent<Animator> ();

		jumpTimeCounter = jumpTime;

		speedMilestoneCount = speedupMilestone;

		moveSpeedStore = moveSpeed;
		speedMilestoneCountStore = speedMilestoneCount;
		speedupMilestoneStore = speedupMilestone;

		stoppedJumping = true;
	}
	
	// Update is called once per frame
	void Update () {

		// grounded = Physics2D.IsTouchingLayers (myCollider, whatIsGround);

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
			theGameManager.RestartGame ();
			moveSpeed = moveSpeedStore;
			speedMilestoneCount = speedMilestoneCountStore;
			speedupMilestone = speedupMilestoneStore;

			deathSound1.Play ();
			//deathSound2.Play ();
			backgroundSound.Stop ();
		}
	}

	public void JumpMowgli () {
		
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

		if (!stoppedJumping) {
			if (jumpTimeCounter > 0) {
				myRigidbody.velocity = new Vector2 (myRigidbody.velocity.x, jumpForce);
				jumpTimeCounter -= Time.deltaTime;
			}
		}

		/*if (Input.GetKeyUp (KeyCode.Space) || Input.GetMouseButtonUp (0)) {
			jumpTimeCounter = 0;
			stoppedJumping = true;
		}*/

		if (grounded) {
			jumpTimeCounter = jumpTime;
			doubleJump = true;
		}

		myAnimator.SetFloat ("Speed", myRigidbody.velocity.x);
		myAnimator.SetBool ("Grounded", grounded);
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
	}
}
