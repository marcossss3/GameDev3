using UnityEngine;
using System.Collections;

public class Skeleton : MonoBehaviour {

	public Animator myAnimator;

	private GameObject player;

	public bool dead = false;

	// Use this for initialization
	void Start () {

		player = GameObject.FindGameObjectWithTag ("Player");

	}
	
	// Update is called once per frame
	void Update () {
		
		if (myAnimator.GetBool ("walking") && !dead) {
			transform.Translate (Vector2.left * Time.deltaTime);
		}

	}

	public void startWalking(){
		
		myAnimator.SetBool ("walking", true);

	}

	void OnCollisionEnter2D(Collision2D collision) {

		if (collision.gameObject.tag == "Player" && !player.GetComponent<PlayerControl>().invincible) {

			myAnimator.SetBool ("attack", true);
			knockPlayer ();

		}

		if (collision.gameObject.tag == "Skeleton" || collision.gameObject.tag == "Ammo" || collision.gameObject.tag == "JumpingPlatform") {

			Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());

		}

	}

	void OnTriggerEnter2D(Collider2D collider) {

		if (collider.gameObject.tag == "Boundary") {

			transform.eulerAngles = new Vector2 (0, transform.eulerAngles.y + 180);

		}

	}
		
	public void knockPlayer(){
		
		StartCoroutine(player.GetComponent<PlayerControl>().knockBack ());

	}
		
	public void Die(){

		Destroy (this.gameObject);

	}

}
