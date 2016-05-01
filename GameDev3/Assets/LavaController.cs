using UnityEngine;
using System.Collections;

public class LavaController : MonoBehaviour {

	private Rigidbody2D rb;

	public GameObject player;

	// Use this for initialization
	void Start () {
	
		rb = GetComponent<Rigidbody2D> ();

	}
	
	// Update is called once per frame
	void Update () {

		float playerAverageSpeed = player.GetComponent<PlayerControl> ().GetAverageSpeed ();

		Debug.Log (playerAverageSpeed);

		if (playerAverageSpeed > 0.2) {
			
			rb.velocity = new Vector2 (rb.velocity.x, playerAverageSpeed / 15);

		}
	
	}

}
