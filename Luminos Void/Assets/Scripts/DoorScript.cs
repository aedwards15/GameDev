using UnityEngine;
using System.Collections;

public class DoorScript : MonoBehaviour 
{
	private bool allDead;

	void Start () { }
	
	void Update () 
	{
		allDead = true;

		foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy")) 
		{
			if (enemy.collider2D.enabled)
			{
				allDead = false;
			}
		}

		if (allDead) 
		{
			this.gameObject.collider2D.enabled = false;
		}
		else
		{
			this.gameObject.collider2D.enabled = true;
		}
	}
}
