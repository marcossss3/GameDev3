using UnityEngine;
using System.Collections;

public class SpawnTrigger : MonoBehaviour {

	public GameObject spawnArea, skeleton;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D(Collision2D collision) {

		if (collision.gameObject.tag == "Player") {

			float adaptiveSpawnLimit = Mathf.Round ((collision.gameObject.GetComponent<PlayerControl> ().getHealth () * 10) / 2);

			for (int i = 0; i < adaptiveSpawnLimit; i++) {
				Instantiate (skeleton, new Vector2(spawnArea.transform.position.x - i, spawnArea.transform.position.y), spawnArea.transform.rotation);
			}
				
			Destroy (gameObject);

		}

	}

}
