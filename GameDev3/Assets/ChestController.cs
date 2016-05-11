using UnityEngine;
using System.Collections;

public class ChestController : MonoBehaviour {

	public GameObject gameController;
	public AudioSource victorySound;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D(Collision2D collision) {

		if (collision.gameObject.tag == "Player" && !gameController.GetComponent<GameController>().getVictory()) {
			victorySound.Play ();
			gameController.GetComponent<GameController> ().setVictory (true);
			StartCoroutine (gameController.GetComponent<GameController>().LoadEnding ());
		}

	}

}
