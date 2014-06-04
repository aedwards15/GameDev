using UnityEngine;
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
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
