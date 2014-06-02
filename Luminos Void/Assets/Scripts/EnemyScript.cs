using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour {

	public enum EnemyType { Type1, Type2, Type3 };
	public bool enemy = true;
	private bool hasSpawn;
	private MoveScript moveScript;
	private WeaponScript[] weapons;
	private bool hasWeapons;

	void Awake()
	{
		weapons = GetComponentsInChildren<WeaponScript> ();

		if (weapons.Length > 0)
			hasWeapons = true;
		else
			hasWeapons = false;

		moveScript = GetComponent<MoveScript> ();
	}

	// Use this for initialization
	void Start () {
		hasSpawn = false;
		
		collider2D.enabled = false;
		moveScript.enabled = false;
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
		
		moveScript.enabled = true;
	}
}
