using UnityEngine;
using System.Collections;

public class ChestController : MonoBehaviour {

	public GameObject gameController;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D(Collision2D collision) {

		if (collision.gameObject.tag == "Player") {
			gameController.GetComponent<GameController> ().setVictory (true);
		}

	}

}
