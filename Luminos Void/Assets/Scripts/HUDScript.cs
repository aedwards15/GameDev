using UnityEngine;
using System.Collections;

public class HUDScript : MonoBehaviour 
{
	private int maxHealth;
	private HealthScript health;
	private float energy;

	// Use this for initialization
	void Start () 
	{
		health = transform.parent.GetComponent<HealthScript>();
//		health = GameObject.Find("Player").GetComponent<HealthScript>();
		maxHealth = health.hp;
	}
	
	// Update is called once per frame
	void Update () 
	{
		energy = (float)health.hp / maxHealth;
		transform.FindChild("Energy").transform.localScale = new Vector3 (energy, 1, 1);
		transform.FindChild("Energy").transform.localPosition = new Vector3(-1.28f + 1.28f * energy, 0, -0.1f);
//		transform.localScale = new Vector3 (energy, 1, 1);
//		transform.localPosition = new Vector3(-1.28f + 1.28f * energy, 0, -0.1f);
	}
}