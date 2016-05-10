using UnityEngine;
using System.Collections;
using System.Diagnostics;

public class GameController : MonoBehaviour {

	public GameObject player, rock, chest;

	private Stopwatch stopWatch;
	private bool stopRocks, victory;

	// Use this for initialization
	void Start () {

		stopWatch = new Stopwatch();
		stopWatch.Start();
	
	}

	// Update is called once per frame
	void Update () {

		if (victory) {



		} else {

			float playerAverageSpeed = player.GetComponent<PlayerControl> ().GetAverageSpeed ();

			float adaptiveSpawnRate = Mathf.Round (14f / (playerAverageSpeed * 2));

			if ((stopWatch.ElapsedMilliseconds / 1000) % adaptiveSpawnRate == 0 && !stopRocks && playerAverageSpeed > 0.1) {
				InstantiateRock ();
				stopRocks = true;
				StartCoroutine (StopSpawningRocks (1));
			}

		}
	
	}

	IEnumerator StopSpawningRocks(float time){
		
		yield return new WaitForSeconds (time);
		stopRocks = false;


	}

	void InstantiateRock () {

		GameObject go = Instantiate(rock,
			new Vector2 (player.transform.position.x + Random.Range(0.0f, 2.0f), player.transform.position.y + 3.0f),
									player.transform.rotation)
			as GameObject;

	}

	public void setVictory (bool boolean) {

		victory = boolean;

	}

	public bool getVictory () {

		return victory;

	}

}
