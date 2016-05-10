using UnityEngine;
using System.Collections;

public class FragmentController : MonoBehaviour {

	private Rigidbody2D rb;

	// Use this for initialization
	void Start () {

		rb = GetComponent<Rigidbody2D> ();

		rb.AddForce (new Vector2 (Random.Range(-200, 200), Random.Range(-200, 200)));
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D(Collision2D collision) {

		if (collision.gameObject.tag != "Ground") {
			Physics2D.IgnoreCollision (collision.collider, GetComponent<Collider2D> ());
		}
			
	}
		
}
