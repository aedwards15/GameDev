using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyTentacledScript : MonoBehaviour {

	public int Health = 5;
	private bool hasSpawn;

	private List<Transform> tentacles;
	private int hp;
	
	void Awake()
	{
		hp = Health;
		tentacles = new List<Transform> ();
		for (int i = 0; i < transform.childCount; i++)
		{
			Transform child = transform.GetChild(i);
			
			tentacles.Add(child);
		}
	}

	int i = 0;
	public void Damage(int damageCount)
	{
		Health -= damageCount;
		
		if (Health <= 0) 
		{
			//SpecialEffectsHelper.Instance.Explosion(transform.position);
			
			//SoundEffectsHelper.Instance.MakeExplosionSound();

			if (i < tentacles.Count -1)
			{
				Destroy (tentacles[i].gameObject);
				Health = hp;
				i++;
			}
			else
			{
				Destroy (tentacles[i].gameObject);
				Dead();
			}
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
	}

	void Dead()
	{
		collider2D.enabled = false;	
		transform.position = new Vector3 (transform.position.x, transform.position.y, transform.position.z + 1);
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
