using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerScript : MonoBehaviour {

	public float speed = 10f;

	private float movementX;
	private float movementY;

	private Animator animator;
	private WeaponScript weapons;
	private List<Transform> childs;

	void Awake()
	{
		animator = GetComponent<Animator>();
		childs = new List<Transform> ();
		//weapons = GetComponentsInChildren<WeaponScript>();
		for (int i = 0; i < transform.childCount; i++)
		{
			Transform child = transform.GetChild(i);
			
			childs.Add(child);
		}
		weapons = childs[0].GetComponent<WeaponScript>();
	}

	//private List<Transform> childs;
	
	// Use this for initialization
	// Start () {
		
		/*childs = new List<Transform> ();
		for (int i = 0; i < transform.childCount; i++)
		{
			Transform child = transform.GetChild(i);
			
			childs.Add(child);
		}
		
		animator = childs [0].GetComponent<Animator> ();*/
	//}

	float SignedAngleBetween(Vector3 a, Vector3 b, Vector3 n){
		// angle in [0,180]
		float angle = Vector3.Angle(a,b);
		float sign = Mathf.Sign(Vector3.Dot(n,Vector3.Cross(a,b)));
		
		// angle in [-179,180]
		float signed_angle = angle * sign;
		
		// angle in [0,360]
		float angle360 =  ((signed_angle) + 360) % 360;
		
		return angle360;
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		HealthScript playerHealth = this.GetComponent<HealthScript>();
		if (playerHealth != null) playerHealth.Damage(1);
	}
	
	// Update is called once per frame
	void Update () {
		float inputX = Input.GetAxis ("Horizontal");
		float inputY = Input.GetAxis ("Vertical");

		bool shoot = Input.GetMouseButtonDown (0);
		
		if (shoot)
		{
			//Vector2 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y);

			//float result = Vector2.Angle(new Vector2(transform.position.x, position.y), position);

			// = Mathf.Atan2(position.x - transform.position.x, position.y - transform.position.y) * Mathf.Rad2Deg;

			Vector3 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
			diff.Normalize();
			
			float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
			childs[0].rotation = Quaternion.Euler(0f, 0f, rot_z - 90);

			//childs[0].rotation = Quaternion.Euler(0,0,angle);
			//float result = SignedAngleBetween(transform.position, position, new Vector3(0,0,0));


			//foreach (WeaponScript weapon in weapons)
			//{
			// Auto-fire
			if (weapons != null && weapons.CanAttack)
			{
				weapons.Attack(true);
			}
			//}
		}

		movementX = (speed * inputX);
		movementY = (speed * inputY);

		if (inputY > 0)
		{
			childs[0].position = new Vector3(transform.position.x, childs[0].position.y, transform.position.z + 1);
			//childs[0].rotation = Quaternion.Euler(0, 0, 90);
			animator.SetBool("IsWalkingAway", true);
			animator.SetBool("IsWalkingToward", false);
			animator.SetBool("IsWalkingLeft", false);
			animator.SetBool("IsWalkingRight", false);
		}
		else if (inputY < 0)
		{
			childs[0].position = new Vector3(transform.position.x, childs[0].position.y, transform.position.z);
			//childs[0].rotation = Quaternion.Euler(0, 0, -90);
			animator.SetBool("IsWalkingToward", true);
			animator.SetBool("IsWalkingAway", false);
			animator.SetBool("IsWalkingLeft", false);
			animator.SetBool("IsWalkingRight", false);
			//animator.SetTrigger("WalkToward");
		}
		else if (inputX > 0)
		{
			childs[0].position = new Vector3(transform.position.x + renderer.bounds.size.x, childs[0].position.y, transform.position.z);
			//childs[0].rotation = Quaternion.Euler(0, 0, 0);
			animator.SetBool("IsWalkingRight", true);
			animator.SetBool("IsWalkingAway", false);
			animator.SetBool("IsWalkingToward", false);
			animator.SetBool("IsWalkingLeft", false);
			//animator.SetTrigger("WalkRight");
		}
		else if (inputX < 0)
		{
			childs[0].position = new Vector3(transform.position.x - renderer.bounds.size.x, childs[0].position.y, transform.position.z);
			//childs[0].rotation = Quaternion.Euler(0, 0, 180);
			animator.SetBool("IsWalkingLeft", true);
			animator.SetBool("IsWalkingAway", false);
			animator.SetBool("IsWalkingToward", false);
			animator.SetBool("IsWalkingRight", false);
			//animator.SetTrigger("WalkLeft");
		}
		else
		{
			animator.SetTrigger("GoIdle");
			animator.SetBool("IsWalkingAway", false);
			animator.SetBool("IsWalkingToward", false);
			animator.SetBool("IsWalkingLeft", false);
			animator.SetBool("IsWalkingRight", false);
		}



		var dist = (transform.position - Camera.main.transform.position).z;
		
		var leftBorder = Camera.main.ViewportToWorldPoint(
			new Vector3(0, 0, dist)
			).x;
		
		var rightBorder = Camera.main.ViewportToWorldPoint(
			new Vector3(1, 0, dist)
			).x;
		
		var topBorder = Camera.main.ViewportToWorldPoint(
			new Vector3(0, 0, dist)
			).y;
		
		var bottomBorder = Camera.main.ViewportToWorldPoint(
			new Vector3(0, 1, dist)
			).y;
		
		transform.position = new Vector3(
			Mathf.Clamp(transform.position.x, leftBorder + .5f, rightBorder - .5f),
			Mathf.Clamp(transform.position.y, topBorder + .5f, bottomBorder - .5f),
			transform.position.z
			);
	}

	void FixedUpdate()
	{
		rigidbody2D.velocity = new Vector2(movementX, movementY);
	}
}
