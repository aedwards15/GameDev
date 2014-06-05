using UnityEngine;
using System.Collections;

public class EnemyWalkerScript : EnemyScript {

	private bool hasSpawn;
	private MoveScript moveScript;
	private Animator animator;
	private GameObject player;
	private Transform healthBar;
	
	void Awake()
	{
		animator = GetComponent<Animator> ();
		moveScript = GetComponent<MoveScript> ();
		healthBar = transform.FindChild ("Health Bar");

		if (player == null)
			player = GameObject.FindGameObjectWithTag("Player");
	}

	void OnTriggerEnter2D(Collider2D otherCollider)
	{
		ShotScript shot = otherCollider.gameObject.GetComponent<ShotScript> ();
		
		if (shot != null)
		{
			HealthScript health = this.GetComponent<HealthScript>();

			if (health != null)
			{
				health.Damage (shot.damage);
			
				Destroy (shot.gameObject);

				if (health.hp <= 0)
					Dead();
			}
		}
	}

	// Use this for initialization
	void Start () {
		hasSpawn = false;
		
		collider2D.enabled = false;
		moveScript.enabled = false;
	}

	private float cooldown = 0f;
	void OnCollisionEnter2D(Collision2D collision)
	{
		PlayerScript playerCollide = collision.gameObject.GetComponent<PlayerScript> ();

		if (cooldown <= 0)
		{
			cooldown = .75f;
			if (playerCollide != null)
			{
				animator.SetTrigger("Attack");
				playedLeft = false;
				playedRight = false;
				HealthScript health = playerCollide.gameObject.GetComponent<HealthScript>();

				if (health != null)
				{
					health.Damage(1);
				}
			}
		}
		else
		{
			cooldown -= Time.deltaTime;
		}
	}


	void OnCollisionStay2D(Collision2D collision)
	{
		PlayerScript playerCollide = collision.gameObject.GetComponent<PlayerScript> ();

		if (cooldown <= 0)
		{
			cooldown = .75f;
			if (playerCollide != null)
			{
				HealthScript health = playerCollide.gameObject.GetComponent<HealthScript>();
				
				if (health != null)
				{
					health.Damage(1);
				}
			}
		}
		else
		{
			cooldown -= Time.deltaTime;
		}
	}

	void Dead()
	{
		animator.SetTrigger ("GoDie");
		collider2D.enabled = false;
		moveScript.speed = Vector2.zero;
		moveScript.direction = Vector2.zero;
		transform.position = new Vector3 (transform.position.x, transform.position.y, transform.position.z + 1);
		isDead = true;
	}

	private bool playedRight = false;
	private bool playedLeft = false;
	// Update is called once per frame
	void Update () {

		healthBar.rotation = Quaternion.Euler (0, 0, 0);
		if (hasSpawn && !isDead)
		{
			moveScript.direction = Vector3.Normalize(player.transform.position - this.transform.position);



			if (moveScript.direction.x < 0 && !playedLeft)
			{
				animator.SetTrigger("WalkLeft");
				playedLeft = true;
				playedRight = false;
			}
			else if (moveScript.direction.x > 0 && !playedRight)
			{
				animator.SetTrigger("WalkRight");
				playedRight = true;
				playedLeft = false;
			}

		}
		else if (!isDead)
		{
			if (renderer.IsVisibleFrom(Camera.main))
			{
				Spawn();
			}
		}
		else
		{
			moveScript.direction = Vector3.Normalize(player.transform.position - this.transform.position);
		}
	}
	
	private void Spawn()
	{
		hasSpawn = true;
		
		collider2D.enabled = true;
		
		moveScript.enabled = true;
	}
}
