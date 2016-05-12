using UnityEngine;
using System.Diagnostics;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class PlayerControl : MonoBehaviour {

	private Rigidbody2D rb;
	private bool interact, keysEnabled;
	public bool invincible;

	private float initialX = -16.0f;
	private float invincibleTimeAfterHurt = 2;
	private int maxAmmo = 15;
	private int ammo = 15;
	private float ammoShot, ammoHit;

	private Stopwatch stopWatch;
	private Queue<float> lastTenSeconds;
	private float averageTenVelocity = 2.0f;


	public Animator anim;
	public GameObject bullet;
	public bool grounded;
	public AudioSource pistolSound, clickSound, ammoSound, jumpSound, hurtSound;

	public float speed = 4.0f;
	public float jumpPower = 350f;

	public Camera camera;

	bool jumped;
	bool hurt;
	public Image ammoBar;
	public Image healthBar;
	private float health = 1f; 	//from 0 to 1 percernt.

	void Start() {
		
		rb = GetComponent<Rigidbody2D> ();

		lastTenSeconds = new Queue<float> (10);

		keysEnabled = true;
		grounded = true;

		stopWatch = new Stopwatch();
		stopWatch.Start();

		InvokeRepeating ("UpdateQueue", 0, 1.0f);

	}

	void UpdateQueue(){

		float currentX = transform.position.x;

		lastTenSeconds.Enqueue (currentX);

		if ((stopWatch.ElapsedMilliseconds / 1000) > 9.0) {

			float lastTenDistance = lastTenSeconds.Dequeue ();

			float tenDistance = currentX - lastTenDistance;

			if (tenDistance < 0)
				averageTenVelocity = 0;
			else
				averageTenVelocity = tenDistance / 10f;

		}

	}

	public float Health {
		get {
			return health;
		}
		set {
			if (health <= 0.00001) {
				return;
			}
			health = value;
			healthBar.fillAmount = value;
			if (value <= 0.000001) {
				Die ();
			}
		}
	}

	public int Ammo {
		get {
			return ammo;
		}
		set {
			if (value > maxAmmo) {
				ammo = maxAmmo;
				ammoBar.fillAmount = 1f;
			} else {
				ammo = value;
				ammoBar.fillAmount = ((float)ammo / (float)maxAmmo);
			}
		}
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
				jumpSound.Play ();
				rb.AddForce (transform.up * jumpPower);
				grounded = false;
				anim.SetBool ("playerAirborne", true);
			}

			if (Input.GetMouseButtonDown (0) && grounded && !anim.GetBool("playerRunning")) {
				
				if (camera.ScreenToWorldPoint (Input.mousePosition).x > rb.position.x)
					transform.eulerAngles = new Vector2 (0, 0);

				if (camera.ScreenToWorldPoint (Input.mousePosition).x < rb.position.x)
					transform.eulerAngles = new Vector2 (0, 180);
				
				anim.SetBool ("playerShooting", true);
				Shoot ();

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

		/*
		if (transform.position.x > (initialX + 0.5f) && stopWatch.ElapsedMilliseconds/1000 > 0.1f)
			return (transform.position.x - (initialX)) / (stopWatch.ElapsedMilliseconds / 1000);
		else
			return 0;
		*/

		// Check that player has moved a certain distance from the starting position
		if (transform.position.x > (initialX + 0.5f))
			return averageTenVelocity;
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

	public void Hurt(float healthPerCent, float hurtTime) {
		hurtSound.Play ();
		TriggerHurt (hurtTime);
		Health = Health + healthPerCent;
	}

	IEnumerator HurtBlinker(float hurtTime) {

		invincible = true;

		anim.SetBool("playerBlinking", true);

		// Waiting for invincibility to end
		yield return new WaitForSeconds(hurtTime);

		// Stop blinking animation and re-enable collision
		anim.SetBool ("playerBlinking", false);

		invincible = false;

	}

	void Shoot (){
		
		if (ammo > 0) {

			pistolSound.Play ();

			var pos = Input.mousePosition;
			pos.z = transform.position.z - Camera.main.transform.position.z;
			pos = Camera.main.ScreenToWorldPoint (pos);

			var q = Quaternion.FromToRotation (Vector3.up, pos - transform.position);
			GameObject go = Instantiate (bullet, transform.position, q) as GameObject;
			Rigidbody2D gorb = go.GetComponent<Rigidbody2D> ();

			gorb.AddForce (gorb.transform.up * 500);

			//bulletInstance.GetComponent<Rigidbody2D> ().velocity = new Vector2 (10.0f, bulletInstance.GetComponent<Rigidbody2D> ().velocity.y + 0.2f);

			Ammo--;
			ammoShot++;

		} else {

			clickSound.Play ();

		}
	}

	public IEnumerator knockBack(){
		if (!invincible) {
			Hurt(-0.2f, 1f);//Amount of damage to health in percentage.
			rb.velocity = new Vector2 (-5f, rb.velocity.y);
			yield return new WaitForSeconds (0.5f);
			rb.velocity = new Vector2 (0, rb.velocity.y);
		}
	}
		
	public float getHealth(){
		return health;
	}

	public void Die(){
		anim.SetBool ("playerDie", true);
		keysEnabled = false;
		//TODO fade to black

	}

	public IEnumerator returnToMenu(){
		yield return new WaitForSeconds(3);
		GameObject.Find ("GameController").GetComponent<ScreenFader> ().BeginFade (1);
		yield return new WaitForSeconds (1f);
		SceneManager.LoadScene ("Menu");
	}

	public void DestroyPlayer(){

		gameObject.GetComponent<SpriteRenderer> ().enabled = false;
		gameObject.GetComponent<Rigidbody2D> ().isKinematic = true;
		gameObject.GetComponent<Collider2D> ().enabled = false;
		StartCoroutine(returnToMenu());

	}

	public void HitSuccess(){

		ammoHit++;

	}

	public float GetAccuracy(){
		if (ammoShot < 1) {
			return 1f;
		}
		return ammoHit / ammoShot;
	}

	public void PickUpAmmo(int ammo){

		ammoSound.Play ();
		Ammo += (ammo);

	}

}
