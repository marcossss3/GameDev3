using UnityEngine;
using System.Collections;

public class LavaController : MonoBehaviour {

	private Rigidbody2D rb;
	private PlayerControl pc;
	private Transform platforms;

	public AudioSource lavaSound;

	public GameObject player, ground, gameController;

	// Use this for initialization
	void Start () {
	
		rb = GetComponent<Rigidbody2D> ();

		pc = player.GetComponent<PlayerControl> ();
		platforms = ground.GetComponent<Transform> ();

	}
	
	// Update is called once per frame
	void Update () {

		float playerAverageSpeed = pc.GetAverageSpeed ();

		if (gameController.GetComponent<GameController> ().getVictory ()) {

			rb.velocity = new Vector2 (0f, 0f);

		}

		if (playerAverageSpeed > 0.1 && !gameController.GetComponent<GameController>().getVictory()) {
			
			rb.velocity = new Vector2 (rb.velocity.x, (playerAverageSpeed / 18) + 0.02f);

		}
	
	}

	void OnCollisionEnter2D(Collision2D collision) {

		if (collision.gameObject.tag == "Player") {

			lavaSound.Play ();

			Vector2 spawnPosition = new Vector2();

			// Loop through every platform
			for (int i = 0 ; i < platforms.childCount; i++) {

				// Find the platform that is in front of the player
				if (player.transform.position.x < platforms.GetChild (i).transform.position.x) {
					spawnPosition = new Vector2 (
						platforms.GetChild (i - 1).transform.position.x, 
						platforms.GetChild (i - 1).transform.position.y + 5f
					);
					break;
				}
					
			}

			pc.setKeysEnabled (false);
			pc.resetAnimations ();
			pc.Respawn (spawnPosition);
			pc.Hurt (-0.1f, 2.0f);

			transform.position = new Vector2 (transform.position.x, transform.position.y - 0.4f);

			StartCoroutine(DisableKeysTemporarily (2));

		}

		if (collision.gameObject.tag == "Rock") {

			lavaSound.Play ();

			Destroy (collision.gameObject);

		}

	}

	IEnumerator DisableKeysTemporarily(int time) {
		yield return new WaitForSeconds(time);
		pc.setKeysEnabled (true);
	}

}
