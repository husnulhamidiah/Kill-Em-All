using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {
	
	public Vector2 speed = new Vector2(25, 25);

	private Vector2 movement;
	private Rigidbody2D rigidBodyComponent;

	void Awake() {
		HealthScript playerHealth = this.GetComponent<HealthScript>();
		var UIManager = FindObjectOfType<UIManagerScript> ();
		UIManager.UpdateLives (playerHealth.hp);
	}

	void Update () {
	
		float inputX = Input.GetAxis ("Horizontal");
		float inputY = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (speed.x * inputX, speed.y * inputY, 0);

		movement *= Time.deltaTime;

		transform.Translate (movement);

		bool shoot = Input.GetButtonDown ("Fire2");
		shoot |= Input.GetButtonDown ("Fire1");

		if (shoot) {
			WeaponScript weapon = GetComponent<WeaponScript>();
			if (weapon != null)
			{
				weapon.Attack(false);
				SoundEffectsHelper.Instance.MakePlayerShotSound();
			}
		}

		// 6 - Make sure we are not outside the camera bounds
		var dist = (transform.position - Camera.main.transform.position).z;
		var leftBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).x;
		var rightBorder = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, dist)).x;
		var topBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).y;
		var bottomBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, dist)).y;
		
		transform.position = new Vector3(
			Mathf.Clamp(transform.position.x, leftBorder, rightBorder),
			Mathf.Clamp(transform.position.y, topBorder, bottomBorder),
			transform.position.z
			);
	}

	void FixedUpdate()
	{
		// 4 - Move the game object
		if (rigidBodyComponent == null) rigidBodyComponent = GetComponent<Rigidbody2D>();
		rigidBodyComponent.velocity = movement;
	}

	void OnDestroy()
	{
		// Check that the player is dead, as we is also callled when closing Unity
		HealthScript playerHealth = this.GetComponent<HealthScript>();
		if (playerHealth != null && playerHealth.hp <= 0)
		{
			var UIManager = FindObjectOfType<UIManagerScript>();
			UIManager.ShowResultMenu(false);
		}
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		bool damagePlayer = false;
		
		// Collision with enemy
		EnemyScript enemy = collision.gameObject.GetComponent<EnemyScript>();
		if (enemy != null)
		{
			// Kill the enemy
			HealthScript enemyHealth = enemy.GetComponent<HealthScript>();
			if (enemyHealth != null) enemyHealth.Damage(enemyHealth.hp);
			
			damagePlayer = true;
		}
		
		// Damage the player
		if (damagePlayer)
		{
			HealthScript playerHealth = this.GetComponent<HealthScript>();
			if (playerHealth != null) playerHealth.Damage(1);
		}
	}
}
