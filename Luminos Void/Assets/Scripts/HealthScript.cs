using UnityEngine;
using System.Collections;

public class HealthScript : MonoBehaviour {

	public int hp = 1;

	public bool isEnemy = true;

	public void Damage(int damageCount)
	{
		hp -= damageCount;

		if (hp <= 0 && !isEnemy)
		{
			Destroy(gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D otherCollider)
	{
		ShotScript shot = otherCollider.gameObject.GetComponent<ShotScript> ();

		if (shot != null)
		{
			if (isEnemy)
			{
				Damage (shot.damage);
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
