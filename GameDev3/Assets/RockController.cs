using UnityEngine;
using System.Collections;

public class RockController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D(Collision2D collision) {

		if (collision.gameObject.tag == "Ground") {

			Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());

		}

		if (collision.gameObject.tag == "Player") {

			collision.gameObject.GetComponent<PlayerControl> ().Hurt ();

		}

	}

}
