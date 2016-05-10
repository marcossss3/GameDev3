using UnityEngine;
using System.Collections;
using System.Diagnostics;

public class GameController : MonoBehaviour {

	public GameObject player, rock;

	private Stopwatch stopWatch;
	private bool stopRocks;

	// Use this for initialization
	void Start () {

		stopWatch = new Stopwatch();
		stopWatch.Start();
	
	}

	// Update is called once per frame
	void Update () {

		if ((stopWatch.ElapsedMilliseconds / 1000) % 4 == 0 && !stopRocks) {
			InstantiateRock ();
			stopRocks = true;
			StartCoroutine(StopSpawningRocks (1));
		}
	
	}

	IEnumerator StopSpawningRocks(float time){
		
		yield return new WaitForSeconds (time);
		stopRocks = false;


	}

	void InstantiateRock () {

		GameObject go = Instantiate(rock,
									new Vector2 (player.transform.position.x + 2.0f, player.transform.position.y + 3.0f),
									player.transform.rotation)
			as GameObject;

	}

}
