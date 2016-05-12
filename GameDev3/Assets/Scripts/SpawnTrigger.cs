using UnityEngine;
using System.Collections;

public class SpawnTrigger : MonoBehaviour {

	public GameObject spawnArea, skeleton;
	public AudioSource spawnSound;

	private bool activated;

	// Use this for initialization
	void Start () {
	
		activated = false;

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D collider) {

		if (collider.gameObject.tag == "Player" && !activated) {

			float adaptiveSpawnLimit = Mathf.Round ((collider.gameObject.GetComponent<PlayerControl> ().Health * 10) / 2);

			spawnSound.Play ();

			for (int i = 0; i < adaptiveSpawnLimit; i++) {
				Instantiate (skeleton, new Vector2(spawnArea.transform.position.x - i, spawnArea.transform.position.y), spawnArea.transform.rotation);
			}

			activated = true;

		}

	}

}
