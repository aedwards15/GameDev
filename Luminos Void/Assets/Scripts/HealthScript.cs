﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HealthScript : MonoBehaviour {
	
	public int hp = 1;
	
	public bool isEnemy = true;


	
	void Awake()
	{
	}
	
	public void Damage(int damageCount)
	{
		hp -= damageCount;
		
		if (hp <= 0) 
		{
			//SpecialEffectsHelper.Instance.Explosion(transform.position);
			
			//SoundEffectsHelper.Instance.MakeExplosionSound();
			
			Destroy (gameObject);
		}
	}
	
	void OnTriggerEnter2D(Collider2D otherCollider)
	{
		ShotScript shot = otherCollider.gameObject.GetComponent<ShotScript> ();
		
		if (shot != null)
		{
			if (shot.isEnemyShot != isEnemy)
			{
				Damage (shot.damage);
				
				Destroy (shot.gameObject);
			}
		}
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
