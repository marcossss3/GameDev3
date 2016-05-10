using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour {

	private bool wasted;

	// Use this for initialization
	void Start () {


	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D(Collision2D collision) {

		if (collision.gameObject.tag == "Ground") {

			wasted = true;

		}

		if (collision.gameObject.tag != "Ground" && wasted ) {

			Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());

		}

		else if (collision.gameObject.tag == "Skeleton" && !wasted) {

			if (!collision.gameObject.GetComponent<Skeleton> ().dead) {

				collision.gameObject.GetComponent<Rigidbody2D> ().isKinematic = true;
				collision.gameObject.GetComponent<Animator> ().SetBool ("defeated", true);
				collision.gameObject.GetComponent<Skeleton> ().dead = true;

				Destroy (gameObject);

			} else {
				Physics2D.IgnoreCollision (collision.collider, GetComponent<Collider2D> ());
			}

		}

		else if (collision.gameObject.tag == "Rock" && !wasted) {

			collision.gameObject.GetComponent<RockController> ().Disintegrate ();
			Destroy (gameObject);

		}

	}

}
