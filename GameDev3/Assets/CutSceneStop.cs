using UnityEngine;
using System.Collections;

public class CutSceneStop : MonoBehaviour {

	public GameObject player, sceneController;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D collider) {

		if (collider.gameObject.tag == "Player") {

			player.GetComponent<Animator> ().SetBool ("playerRunning", false);
			player.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0f, 0f);

			StartCoroutine(sceneController.GetComponent<CutSceneController>().LoadLevel (2));

		}

	}

}
