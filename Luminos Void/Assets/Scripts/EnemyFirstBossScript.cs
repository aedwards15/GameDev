using UnityEngine;
using System.Collections;

public class EnemyFirstBossScript : EnemyScript {

	public int MeleeDamage = 5;
	public int RangedDamage = 5;
	public float SpawnEnemyX = 0f;
	public float SpawnEnemyY = 0f;
	public float SpawnEnemyZ = 5f;
	private MoveScript moveScript;
	private Animator animator;
	private GameObject player;
	private Transform healthBar;
	private Vector2 speed;
	private WeaponScript weaponScript;
	private Transform weapon;
	private Transform weaponObject;
	private Vector3 SpawnEnemyLoc;

	void Awake () {
		SpawnEnemyLoc = new Vector3 (SpawnEnemyX, SpawnEnemyY, SpawnEnemyZ);
		animator = GetComponent<Animator> ();
		moveScript = GetComponent<MoveScript> ();
		speed = moveScript.speed;
		healthBar = transform.FindChild ("Health Bar");
		weapon = transform.Find ("Weapon");
		weaponObject = weapon.transform.Find ("WeaponObject");
		weaponScript = weapon.GetComponentInChildren<WeaponScript> ();
		

	}

	
	private Vector3 scale;
	// Use this for initialization
	void Start () {
		scale = transform.localScale;
	}
	
	// Update is called once per frame
	private bool playedRight = false;
	private bool playedLeft = false;
	public float AttackFrequency = 3f;
	void Update () {
		player = GameObject.FindGameObjectWithTag("Player");
		healthBar.rotation = Quaternion.Euler (0, 0, 0);
		if (!isDead)
		{
			moveScript.direction = Vector3.Normalize(player.transform.position - this.transform.position);

			if (moveScript.direction.x < 0 && !playedLeft)
			{
				transform.localScale = new Vector3(scale.x * -1, scale.y, scale.z);
				animator.SetTrigger("WalkLeft");
				playedLeft = true;
				playedRight = false;
			}
			else if (moveScript.direction.x > 0 && !playedRight)
			{
				if (transform.localScale.x < 0)
					transform.localScale = new Vector3(scale.x * -1, scale.y, scale.z);

				animator.SetTrigger("WalkRight");
				playedRight = true;
				playedLeft = false;
			}

			if (AttackFrequency <= 0)
			{
				float attack = Random.Range(0, 10);
				if (attack < 5f)
				{
					AttackRanged();
				}
				else
				{
					AttackSummon();
				}

				
				AttackFrequency = Random.Range(.5f, 6f);
			}
			else
			{
				AttackFrequency -= Time.deltaTime;
			}
		}
		else
		{
			//moveScript.direction = Vector3.Normalize(player.transform.position - this.transform.position);
		}
	}

	private void AttackRanged()
	{
		moveScript.speed = Vector2.zero;
		animator.SetTrigger("AttackRanged");
		StartCoroutine(RangedAnimation());
		if (weaponScript != null && weaponScript.CanAttack)
		{
			Vector3 diff = Vector3.Normalize(player.transform.position - weapon.transform.position);
			
			float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
			weaponObject.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
			
			//SoundEffectsHelper.Instance.MakePlayerShotSound();
			weaponScript.Attack(true);
		}
	}

	IEnumerator RangedAnimation(){
		
		yield return new WaitForSeconds (2f);
		moveScript.speed = speed;
	}

	public Transform enemy1;
	public Transform enemy2;
	public Transform enemy3;
	private void AttackSummon()
	{
		moveScript.speed = Vector2.zero;
		animator.SetTrigger("AttackSummon");
		StartCoroutine(SummonAnimation());

		float rand = Random.Range (0, 9);
		Transform enemyTransform;

		if (rand < 3)
		{
			enemyTransform = Instantiate(enemy1, SpawnEnemyLoc, Quaternion.Euler(0, 0, 0)) as Transform;

		}
		else if (rand < 6)
		{
			enemyTransform = Instantiate(enemy2, SpawnEnemyLoc, Quaternion.Euler(0, 0, 0)) as Transform;
		}
		else
		{
			enemyTransform = Instantiate(enemy3, SpawnEnemyLoc, Quaternion.Euler(0, 0, 0)) as Transform;
		}

		EnemyScript summonEnemyScript = enemyTransform.GetComponent<EnemyScript>();
		summonEnemyScript.shouldSpawn = true;
		MoveScript summonMoveScript = enemyTransform.GetComponent<MoveScript>();
		summonMoveScript.speed = new Vector2 (4, 4);
	}

	IEnumerator SummonAnimation(){
		
		yield return new WaitForSeconds (2f);
		moveScript.speed = speed;
	}

	void Dead()
	{
		animator.SetTrigger ("Die");
		collider2D.enabled = false;
		moveScript.speed = Vector2.zero;
		moveScript.direction = Vector2.zero;
		transform.position = new Vector3 (transform.position.x, transform.position.y, transform.position.z + 1);
		isDead = true;
	}

	void OnTriggerEnter2D(Collider2D otherCollider)
	{
		ShotScript shot = otherCollider.gameObject.GetComponent<ShotScript> ();
		
		if (shot != null && !shot.isEnemyShot)
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
					health.Damage(5);
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
					health.Damage(5);
				}
			}
		}
		else
		{
			cooldown -= Time.deltaTime;
		}
	}
	

}
