using UnityEngine;
using System.Collections;

public class JumpingPlat : MonoBehaviour {

	public Animator springAnimator;
	public Animator playerAnimator;
	public AudioSource springSound;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D(Collision2D collision) {

		if (collision.gameObject.tag == "Player") {

			springSound.Play ();
			springAnimator.SetTrigger("move");

			collision.gameObject.GetComponent<PlayerControl> ().jumpSound.Play ();
			collision.rigidbody.velocity = new Vector2 (collision.rigidbody.velocity.x, 10.0f);
			//collision.rigidbody.AddForce (transform.up * 350);

			//Animator playerAnimator = collision.gameObject.GetComponent<Animator> ();
			collision.gameObject.GetComponent<PlayerControl>().grounded = false;
			playerAnimator.SetBool ("playerFalling", false);
			playerAnimator.SetBool ("playerAirborne", true);
			
		}

	}

}
