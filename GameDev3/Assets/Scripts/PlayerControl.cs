using UnityEngine;
using System.Collections;
using System.Diagnostics;

public class PlayerControl : MonoBehaviour {

	private Rigidbody2D rb;
	private bool interact, keysEnabled, invincible;
	public bool grounded;

	private float initialX = -16.0f;
	private float invincibleTimeAfterHurt = 2;

	private Stopwatch stopWatch;


	public Animator anim;
	public float speed = 4.0f;
	public float jumpPower = 350f;

	bool jumped;

	void Start() {
		
		rb = GetComponent<Rigidbody2D> ();

		keysEnabled = true;
		grounded = true;

		stopWatch = new Stopwatch();
		stopWatch.Start();

	}

	void Update() {
		
		Movement(); //call the function every frame

	}

	void Movement() {

		if (keysEnabled) {

			if (rb.velocity.y < 0 && !grounded) {
				anim.SetBool ("playerAirborne", false);
				anim.SetBool ("playerFalling", true);
			}

			if (Input.GetKey (KeyCode.D)) {
				transform.Translate (Vector3.right * speed * Time.deltaTime); 
				transform.eulerAngles = new Vector2 (0, 0); // Sets the rotation of the gameobject
				if(grounded)
					anim.SetBool("playerRunning", true);
			}
			if (Input.GetKey (KeyCode.A)) {
				transform.Translate (Vector3.right * speed * Time.deltaTime);
				transform.eulerAngles = new Vector2 (0, 180); // Sets the rotation of the gameobject
				if(grounded)
					anim.SetBool("playerRunning", true);
			}

			if (Input.GetKeyDown (KeyCode.W) && grounded) {
				rb.AddForce (transform.up * jumpPower);
				grounded = false;
				anim.SetBool ("playerAirborne", true);
			}

			if (Input.GetMouseButtonDown (0) && grounded && !anim.GetBool("playerRunning")) {
				anim.SetBool ("playerShooting", true);
			}

			if (Input.GetKeyUp (KeyCode.D) || Input.GetKeyUp (KeyCode.A)) {
				anim.SetBool ("playerRunning", false);
			}

		}

	}

	public void StopFiring(){

		anim.SetBool ("playerShooting", false);

	}

	void OnCollisionEnter2D(Collision2D collision) {

		if (collision.gameObject.tag == "Ground") {

			grounded = true;
			anim.SetBool ("playerFalling", false);
			anim.SetBool ("playerAirborne", false);

		}

	}

	public float GetAverageSpeed(){

		if (transform.position.x > (initialX+0.5f))
			return (transform.position.x - (initialX)) / (stopWatch.ElapsedMilliseconds / 1000);
		else
			return 0;

	}

	public void resetAnimations() {

		anim.SetBool ("playerRunning", false);
		anim.SetBool ("playerFalling", false);
		anim.SetBool ("playerAirborne", false);
		anim.SetBool ("playerShooting", false);

	}

	public void Respawn (Vector2 spawn) {

		transform.position = spawn;

	}

	public void setKeysEnabled(bool boolean){

		keysEnabled = boolean;

	}

	private void TriggerHurt(float hurtTime) {

		StartCoroutine (HurtBlinker(hurtTime));

	}

	public void Hurt() {

		TriggerHurt (invincibleTimeAfterHurt);

	}

	IEnumerator HurtBlinker(float hurtTime) {

		invincible = true;

		// Dizzy to just blink
		yield return new WaitForSeconds(0.1F);
		anim.SetBool("Blink", true);

		// Waiting for invincibility to end
		yield return new WaitForSeconds(hurtTime);

		// Stop blinking animation and re-enable collision
		anim.SetBool ("Blink", false);

		invincible = false;

	}

	public void knockBack(){
		rb.AddForce(new Vector2(-5, 1));
	}
}
