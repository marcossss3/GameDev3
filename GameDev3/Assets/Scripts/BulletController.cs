using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour {

	public GameObject player;

	private bool wasted;

	// Use this for initialization
	void Start () {

		player = GameObject.FindGameObjectWithTag ("Player");
	
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

				collision.gameObject.GetComponent<Skeleton> ().Defeat ();

				player.GetComponent<PlayerControl> ().HitSuccess ();

				Destroy (gameObject);

			} else {
				Physics2D.IgnoreCollision (collision.collider, GetComponent<Collider2D> ());
			}

		}

		else if (collision.gameObject.tag == "Rock" && !wasted) {

			collision.gameObject.GetComponent<RockController> ().Disintegrate ();

			player.GetComponent<PlayerControl> ().HitSuccess ();

			Destroy (gameObject);

		}

	}

}
