using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void playLevel(){
		SceneManager.LoadScene ("SceneMarcos");
	}

	public void loadLevel(int i){

		SceneManager.LoadScene (i);

	}

	public void extiGame(){
		Application.Quit ();
	}
}
