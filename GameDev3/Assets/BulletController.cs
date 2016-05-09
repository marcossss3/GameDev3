using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D(Collision2D collision) {

		if (collision.gameObject.tag == "Skeleton") {

			if (!collision.gameObject.GetComponent<skeleton> ().dead) {

				collision.gameObject.GetComponent<Rigidbody2D> ().isKinematic = true;
				collision.gameObject.GetComponent<Animator> ().SetBool ("defeated", true);
				collision.gameObject.GetComponent<skeleton> ().dead = true;

			}

		}

	}

}
