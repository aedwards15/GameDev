using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyTentacledScript : EnemyScript {

	private bool hasSpawn;

	private Transform tentacle;
	
	void Awake()
	{
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
				{
					tentacle = transform.Find("Tentacle");

					if (tentacle != null)
					{
						health.hp = 5;
						Destroy(tentacle.gameObject);
					}
					else
					{
						Dead ();
					}
				}
			}
		}
	}
	
	// Use this for initialization
	void Start () {
		hasSpawn = false;
		
		collider2D.enabled = false;
	}

	void Dead()
	{
		collider2D.enabled = false;	
		transform.position = new Vector3 (transform.position.x, transform.position.y, transform.position.z + 1);
		isDead = true;
	}
	
	// Update is called once per frame
	void Update () {
		
		if (hasSpawn == false)
		{
			if (renderer.IsVisibleFrom(Camera.main))
			{
				Spawn();
			}
		}
		else
		{
			
		}
	}
	
	private void Spawn()
	{
		hasSpawn = true;
		
		collider2D.enabled = true;
	}
}
