using UnityEngine;
using System.Collections;

public class diamondController : ChestController {

	void OnCollisionEnter2D(Collision2D collision) {

		if (collision.gameObject.tag == "Player" && !gameController.GetComponent<GameController>().getVictory()) {
			victorySound.Play ();
			gameController.GetComponent<GameController> ().setVictory (true);
			Destroy (gameObject);
			StartCoroutine (gameController.GetComponent<GameController>().LoadEnding ());
		}

	}
}
