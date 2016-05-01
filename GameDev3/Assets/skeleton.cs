using UnityEngine;
using System.Collections;

public class skeleton : MonoBehaviour {

	public Animator myAnimator;
	public PlayerControl player;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (myAnimator.GetBool ("walking")) {
			transform.Translate (Vector2.left * Time.deltaTime);
		}
	}

	public void startWalking(){
		myAnimator.SetBool ("walking", true);
	}

	void OnCollisionEnter2D(Collision2D collision) {

		if (collision.gameObject.tag == "Player") {
			myAnimator.SetBool ("attack", true);
		}
	}

	public void knockPlayer(){
		player.knockBack ();
	}
}
