using UnityEngine;
using System.Collections;

public class Ammo : MonoBehaviour {

	public PlayerControl player;

	void Start(){

		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerControl>();

	}

	void OnCollisionEnter2D(Collision2D collision) {

		if (collision.gameObject.tag == "Player") {
			player.Ammo += ( 1 + Mathf.RoundToInt( 1 / player.GetAccuracy() * 2 ));
			Destroy (this.gameObject);
		}
	}
}
