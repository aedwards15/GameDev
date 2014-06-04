using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerScript : MonoBehaviour {

	public float speed = 10f;

	private float movementX;
	private float movementY;

	private Animator animator;
	private WeaponScript weaponScript;
	private Transform weapon;
	private Transform weaponObject;

	private Color litUp;
	private Color normalLight;

	private bool isDead = false;
	private HealthScript playerHealth;
	
	public Sprite[] weaponSprites;

	void Awake()
	{
		playerHealth = this.GetComponent<HealthScript> ();
		animator = GetComponent<Animator>();

		normalLight = this.renderer.material.color;
		litUp = new Color (255, 255, 255, 255);
		//weapons = GetComponentInChildren<WeaponScript>();
		weapon = transform.Find ("Weapon");
		weaponObject = weapon.transform.Find ("WeaponObject");
		weaponScript = weapon.GetComponentInChildren<WeaponScript> ();
		//weaponSprites = Resources.LoadAll<Sprite>("shrimp_0");
	}

	private void Dead()
	{
		animator.ResetTrigger("WalkAway");
		animator.ResetTrigger("WalkToward");
		animator.ResetTrigger("WalkRight");
		animator.ResetTrigger("WalkLeft");
		animator.ResetTrigger ("GoIdle");
		animator.SetTrigger ("DieRight");
		collider2D.enabled = false;
		//moveScript.speed = Vector2.zero;
		//moveScript.direction = Vector2.zero;
		transform.position = new Vector3 (transform.position.x, transform.position.y, transform.position.z + 1);
		isDead = true;
	}

	// Update is called once per frame
	void Update () {

		if (!isDead)
		{
			Shoot();

			Move();

			if (playerHealth.hp <= 0)
				Dead();
		}
		else
		{
			movementX = 0;
			movementY = 0;
		}
	}

	private void Shoot()
	{
		bool shoot = Input.GetMouseButtonDown (0);
		
		if (shoot)
		{
			if (weaponScript != null && weaponScript.CanAttack)
			{
				Vector3 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - weapon.transform.position;
				diff.Normalize();
				
				float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
				weaponObject.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
				
				//SoundEffectsHelper.Instance.MakePlayerShotSound();
				weaponScript.Attack(false);
			}
		}
	}

	bool PlayedLeft = false;
	bool PlayedRight = false;
	bool PlayedToward = false;
	bool PlayedAway = false;
	private void Move()
	{
		float inputX = Input.GetAxis ("Horizontal");
		float inputY = Input.GetAxis ("Vertical");
		
		movementX = (speed * inputX);
		movementY = (speed * inputY);
		
		if (inputY > 0)
		{
			//weapon.position = new Vector3(transform.position.x, weapon.position.y, transform.position.z + 1);

			//weapon.GetComponent<SpriteRenderer>().sprite = weaponSprites[2];
			
			if (!PlayedAway)
			{
				animator.ResetTrigger("GoIdle");
				animator.ResetTrigger("WalkToward");
				animator.ResetTrigger("WalkRight");
				animator.ResetTrigger("WalkLeft");
				PlayedAway = true;
				PlayedLeft = false;
				PlayedRight = false;
				PlayedToward = false;
				animator.SetTrigger("WalkAway");
			}
		}
		else if (inputY < 0)
		{
			//weapon.position = new Vector3(transform.position.x, weapon.position.y, transform.position.z);

			//weapon.GetComponent<SpriteRenderer>().sprite = weaponSprites[0];

			if (!PlayedToward)
			{
				animator.ResetTrigger("GoIdle");
				animator.ResetTrigger("WalkAway");
				animator.ResetTrigger("WalkRight");
				animator.ResetTrigger("WalkLeft");
				PlayedAway = false;
				PlayedLeft = false;
				PlayedRight = false;
				PlayedToward = true;
				animator.SetTrigger("WalkToward");
			}
		}
		else if (inputX > 0)
		{
			//childs[0].position = new Vector3(transform.position.x + renderer.bounds.size.x, childs[0].position.y, transform.position.z);

			//weapon.GetComponent<SpriteRenderer>().sprite = weaponSprites[0];

			if (!PlayedRight)
			{
				animator.ResetTrigger("GoIdle");
				animator.ResetTrigger("WalkToward");
				animator.ResetTrigger("WalkAway");
				animator.ResetTrigger("WalkLeft");
				PlayedAway = false;
				PlayedLeft = false;
				PlayedRight = true;
				PlayedToward = false;
				animator.SetTrigger("WalkRight");
			}
		}
		else if (inputX < 0)
		{
			//childs[0].position = new Vector3(transform.position.x - renderer.bounds.size.x, childs[0].position.y, transform.position.z);

			//weapon.GetComponent<SpriteRenderer>().sprite = weaponSprites[1];

			if (!PlayedLeft)
			{
				animator.ResetTrigger("GoIdle");
				animator.ResetTrigger("WalkToward");
				animator.ResetTrigger("WalkRight");
				animator.ResetTrigger("WalkAway");
				PlayedAway = false;
				PlayedLeft = true;
				PlayedRight = false;
				PlayedToward = false;
				animator.SetTrigger("WalkLeft");
			}
		}
		else
		{
			animator.ResetTrigger("WalkAway");
			animator.ResetTrigger("WalkToward");
			animator.ResetTrigger("WalkRight");
			animator.ResetTrigger("WalkLeft");
			PlayedAway = false;
			PlayedLeft = false;
			PlayedRight = false;
			PlayedToward = false;
			animator.SetTrigger("GoIdle");
		}

		var dist = (transform.position - Camera.main.transform.position).z;
		
		var leftBorder = Camera.main.ViewportToWorldPoint(
			new Vector3(0, 0, dist)
			).x;
		
		var rightBorder = Camera.main.ViewportToWorldPoint(
			new Vector3(1, 0, dist)
			).x;
		
		var topBorder = Camera.main.ViewportToWorldPoint(
			new Vector3(0, 0, dist)
			).y;
		
		var bottomBorder = Camera.main.ViewportToWorldPoint(
			new Vector3(0, 1, dist)
			).y;
		
		transform.position = new Vector3(
			Mathf.Clamp(transform.position.x, leftBorder + .5f, rightBorder - .5f),
			Mathf.Clamp(transform.position.y, topBorder + .5f, bottomBorder - .5f),
			transform.position.z
			);
	}

	void FixedUpdate()
	{
		rigidbody2D.velocity = new Vector2(movementX, movementY);
	}



	void OnCollisionExit2D(Collision2D collision)
	{
		TileScript tile = collision.gameObject.GetComponent<TileScript> ();
		
		if (tile != null)
		{
			this.renderer.material.color = normalLight;
		}
	}

	void OnTriggerStay2D(Collider2D otherCollider)
	{
		TileScript tile = otherCollider.gameObject.GetComponent<TileScript> ();

		if (tile != null)
		{
			if (tile.IsOn)
			{
				this.renderer.material.color = litUp;
			}
			else
			{
				this.renderer.material.color = normalLight;
			}
		}
	}

	void OnTriggerExit2D(Collider2D collider)
	{
		TileScript tile = collider.gameObject.GetComponent<TileScript> ();
		
		if (tile != null)
		{
			this.renderer.material.color = normalLight;
		}
	}
}
