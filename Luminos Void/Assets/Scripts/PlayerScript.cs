using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerScript : MonoBehaviour {

	public float speed = 10f;

	private float movementX;
	private float movementY;

	private Animator animator;
	private WeaponScript weapons;
	private List<Transform> childs;

	private Color litUp;
	private Color normalLight;

	void Awake()
	{
		animator = GetComponent<Animator>();
		childs = new List<Transform> ();

		normalLight = this.renderer.material.color;
		litUp = new Color (255, 255, 255, 255);
		//weapons = GetComponentsInChildren<WeaponScript>();
		for (int i = 0; i < transform.childCount; i++)
		{
			Transform child = transform.GetChild(i);
			
			childs.Add(child);
		}
		weapons = childs[0].GetComponent<WeaponScript>();
	}

	// Update is called once per frame
	void Update () {
		Shoot();

		Move();
	}

	private void Shoot()
	{
		bool shoot = Input.GetMouseButtonDown (0);
		
		if (shoot)
		{
			if (weapons != null && weapons.CanAttack)
			{
				Vector3 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
				diff.Normalize();
				
				float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
				childs[0].rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
				
				//SoundEffectsHelper.Instance.MakePlayerShotSound();
				weapons.Attack(false);
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
			childs[0].position = new Vector3(transform.position.x, childs[0].position.y, transform.position.z + 1);
			
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
			childs[0].position = new Vector3(transform.position.x, childs[0].position.y, transform.position.z);
			
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
			childs[0].position = new Vector3(transform.position.x + renderer.bounds.size.x, childs[0].position.y, transform.position.z);
			
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
			childs[0].position = new Vector3(transform.position.x - renderer.bounds.size.x, childs[0].position.y, transform.position.z);
			
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

	void OnCollisionEnter2D(Collision2D collision)
	{
		EnemyScript enemy = collision.gameObject.GetComponent<EnemyScript> ();

		if (enemy != null)
		{
			HealthScript playerHealth = this.GetComponent<HealthScript>();
			if (playerHealth != null) 
				playerHealth.Damage(1);
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
}
