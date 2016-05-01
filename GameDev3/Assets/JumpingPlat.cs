using UnityEngine;
using System.Collections;

public class JumpingPlat : MonoBehaviour {

	public Animator springAnimator;
	public Animator playerAnimator;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D(Collision2D collision) {

		if (collision.gameObject.tag == "Player") {
			springAnimator.SetTrigger("move");

			collision.rigidbody.velocity = new Vector2 (collision.rigidbody.velocity.x, 10.0f);

			//Animator playerAnimator = collision.gameObject.GetComponent<Animator> ();
			collision.gameObject.GetComponent<PlayerControl>().grounded = false;
			playerAnimator.SetBool ("playerAirborne", true);
			
		}
	}
}
