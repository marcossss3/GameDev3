using UnityEngine;
using System.Collections;
using System.Diagnostics;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	public GameObject player, rock;
	public bool rocksActivated;

	private Stopwatch stopWatch;
	private bool stopRocks, victory;

	public Image victoryText;

	// Use this for initialization
	void Start () {

		stopWatch = new Stopwatch();
		stopWatch.Start();

		victoryText.enabled = false;
	
	}

	// Update is called once per frame
	void Update () {

		if (!victory && rocksActivated) {

			float playerAverageSpeed = player.GetComponent<PlayerControl> ().GetAverageSpeed ();

			float adaptiveSpawnRate = Mathf.Round (14f / ((playerAverageSpeed * 2) + 0.1f));

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
			new Vector2 (player.transform.position.x + Random.Range(0.0f, 3.0f), player.transform.position.y + 3.2f),
									player.transform.rotation)
			as GameObject;

	}

	public void setVictory (bool boolean) {

		victory = boolean;

	}

	public bool getVictory () {

		return victory;

	}

	public IEnumerator LoadEnding(){

		victoryText.enabled = true;
		yield return new WaitForSeconds(4);
		GetComponent<ScreenFader> ().BeginFade (1);
		yield return new WaitForSeconds (1f);

		if (SceneManager.GetActiveScene ().buildIndex == 1) {
			SceneManager.LoadScene (3);
		}

		if (SceneManager.GetActiveScene ().buildIndex == 2) {
			SceneManager.LoadScene (0);
		}

	}

}
