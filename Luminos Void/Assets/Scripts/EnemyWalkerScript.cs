using UnityEngine;
using System.Collections;

public class EnemyWalkerScript : MonoBehaviour {

	public int Health = 5;

	private bool hasSpawn;
	private MoveScript moveScript;
	private Animator animator;
	private GameObject player;
	
	void Awake()
	{
		animator = GetComponent<Animator> ();
		moveScript = GetComponent<MoveScript> ();

		if (player == null)
			player = GameObject.FindGameObjectWithTag("Player");
	}

	public void Damage(int damageCount)
	{
		Health -= damageCount;
		
		if (Health <= 0) 
		{
			//SpecialEffectsHelper.Instance.Explosion(transform.position);
			
			//SoundEffectsHelper.Instance.MakeExplosionSound();

			Dead();
		}
	}

	void OnTriggerEnter2D(Collider2D otherCollider)
	{
		ShotScript shot = otherCollider.gameObject.GetComponent<ShotScript> ();
		
		if (shot != null)
		{
			Damage (shot.damage);
			
			Destroy (shot.gameObject);
		}
	}

	// Use this for initialization
	void Start () {
		hasSpawn = false;
		
		collider2D.enabled = false;
		moveScript.enabled = false;
	}

	void Dead()
	{
		animator.SetTrigger ("GoDie");
		collider2D.enabled = false;
		moveScript.enabled = false;
		transform.position = new Vector3 (transform.position.x, transform.position.y, transform.position.z + 1);
	}
	
	// Update is called once per frame
	void Update () {
		
		if (hasSpawn)
		{
			moveScript.direction = Vector3.Normalize(player.transform.position - this.transform.position);
		}
		else
		{
			if (renderer.IsVisibleFrom(Camera.main))
			{
				Spawn();
			}
		}
	}
	
	private void Spawn()
	{
		hasSpawn = true;
		
		collider2D.enabled = true;
		
		moveScript.enabled = true;
	}
}
