using UnityEngine;
using System.Collections;

public class HealthPackScript : MonoBehaviour 
{
	public int replenishHealth = 1;
	private int maxHealth;

	// Use this for initialization
	void Start () 
	{
		maxHealth = GameObject.Find("Player").GetComponent<HealthScript>().hp;
	}
	
	// Update is called once per frame
	void Update () { }

	void OnTriggerEnter2D(Collider2D otherCollider)
	{
		PlayerScript player = otherCollider.GetComponent<PlayerScript>();
		HealthScript health = otherCollider.GetComponent<HealthScript>();

		if (player != null)
		{
			if (health.hp < maxHealth)
			{
				health.hp += replenishHealth;
				if(health.hp > maxHealth)
				{
					health.hp = maxHealth;
				}
			}
			Destroy(this.gameObject);
		}
	}
}
