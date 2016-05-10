using UnityEngine;
using System.Collections;

public class Ammo : MonoBehaviour {

	public PlayerControl player;
	int bullets;

	void Start(){
		bullets = 3;
	}

	void OnCollisionEnter2D(Collision2D collision) {

		if (collision.gameObject.tag == "Player") {
			player.Ammo += bullets;
			Destroy (this.gameObject);
		}
	}
}
