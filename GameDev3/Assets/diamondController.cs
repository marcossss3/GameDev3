using UnityEngine;
using System.Collections;

public class diamondController : MonoBehaviour {

	public GameObject gameController;
	public AudioSource victorySound;

	void OnTriggerEnter2D(Collider2D collider) {

		if (collider.gameObject.tag == "Player" && !gameController.GetComponent<GameController>().getVictory()) {

			gameObject.GetComponent<SpriteRenderer> ().enabled = false;
			victorySound.Play ();
			gameController.GetComponent<GameController> ().setVictory (true);
			StartCoroutine (gameController.GetComponent<GameController>().LoadEnding ());

		}

	}
}
