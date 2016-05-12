using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CutSceneController : MonoBehaviour {

	public GameObject player, jumpingPlat;

	// Use this for initialization
	void Start () {

		player.GetComponent<PlayerControl> ().setKeysEnabled (false);

		player.GetComponent<Animator> ().SetBool ("playerRunning", true);
		jumpingPlat.GetComponent<JumpingPlat> ().setPower (16.0f);
	
	}

	// Update is called once per frame
	void Update () {

		if (player.GetComponent<Animator>().GetBool("playerRunning")) {
			player.transform.Translate (Vector3.right * 3f * Time.deltaTime); 
		}
	
	}

	public IEnumerator LoadLevel(int level){

		yield return new WaitForSeconds(1);
		GetComponent<ScreenFader> ().BeginFade (1);
		yield return new WaitForSeconds (1f);
		SceneManager.LoadScene (2);

	}


}
