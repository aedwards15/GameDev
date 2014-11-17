using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HealthScript : MonoBehaviour {
	
	public int hp = 1;
	public AudioClip damageClip;
	private AudioSource sound;

	public bool isEnemy = true;

	void Awake()
	{
	}
	
	public void Damage(int damageCount)
	{
		hp -= damageCount;

		if(!sound.isPlaying)
		{
			sound.Play ();
		}
	}

	// Use this for initialization
	void Start () 
	{
		sound = gameObject.AddComponent<AudioSource>();
		sound.clip = damageClip;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
