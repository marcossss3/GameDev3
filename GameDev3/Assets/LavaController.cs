using UnityEngine;
using System.Collections;

public class LavaController : MonoBehaviour {

	private Rigidbody2D rb;
	private PlayerControl pc;
	private Transform platforms;

	public GameObject player, ground;

	// Use this for initialization
	void Start () {
	
		rb = GetComponent<Rigidbody2D> ();

		pc = player.GetComponent<PlayerControl> ();
		platforms = ground.GetComponent<Transform> ();

	}
	
	// Update is called once per frame
	void Update () {

		float playerAverageSpeed = pc.GetAverageSpeed ();

		if (playerAverageSpeed > 0.2) {
			
			rb.velocity = new Vector2 (rb.velocity.x, playerAverageSpeed / 15);

		}
	
	}

	void OnCollisionEnter2D(Collision2D collision) {

		if (collision.gameObject.tag == "Player") {

			Vector2 spawnPosition = new Vector2();

			// Loop through every platform
			for (int i = 0 ; i < platforms.childCount; i++) {

				// Find the platform that is in front of the player
				if (player.transform.position.x < platforms.GetChild(i).transform.position.x) {
					spawnPosition = new Vector2 (
						platforms.GetChild (i - 1).transform.position.x, 
						platforms.GetChild (i - 1).transform.position.y + 4f
					);
					break;
				}

			}

			pc.setKeysEnabled (false);
			pc.resetAnimations ();
			pc.Respawn (spawnPosition);
			pc.Hurt ();

			StartCoroutine(DisableKeysTemporarily (2));

		}

	}

	IEnumerator DisableKeysTemporarily(int time) {
		yield return new WaitForSeconds(time);
		pc.setKeysEnabled (true);
	}

}
