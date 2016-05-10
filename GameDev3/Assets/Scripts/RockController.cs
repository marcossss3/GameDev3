using UnityEngine;
using System.Collections;

public class RockController : MonoBehaviour {

	public GameObject fragments;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D(Collision2D collision) {

		if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Skeleton") {

			Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());

		}

		if (collision.gameObject.tag == "Player") {

			collision.gameObject.GetComponent<PlayerControl> ().Hurt (-0.4f, 1f);

		}

	}

	public void Disintegrate(){

		Instantiate (fragments, gameObject.transform.position, gameObject.transform.rotation);
		Destroy (gameObject);

	}

}
