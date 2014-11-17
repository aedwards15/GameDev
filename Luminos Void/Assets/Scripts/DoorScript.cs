using UnityEngine;
using System.Collections;

public class DoorScript : MonoBehaviour 
{
	private bool allDead;
	private Animator animator;
	private EnemyScript enemyScript;

	void Awake()
	{
		animator = GetComponent<Animator> ();
	}

	void Start () { }

	private bool TriggerSet = false;
	private bool DoorOpen = false;
	void Update () 
	{
		allDead = true;

		foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy")) 
		{
			enemyScript = enemy.GetComponent<EnemyScript>();
			if (!enemyScript.isDead)
			{
				allDead = false;
			}
		}

		if (allDead) 
		{
			this.gameObject.collider2D.enabled = false;

			if (!TriggerSet)
			{
				animator.SetTrigger("DoorOpen");
				DoorOpen = true;
				TriggerSet = true;
			}
		}
		else
		{

			this.gameObject.collider2D.enabled = true;

			if (DoorOpen)
			{
				animator.SetTrigger("DoorClose");
				DoorOpen = false;
				TriggerSet = false;
			}
		}
	}
}
