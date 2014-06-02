using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour 
{
	public bool followX = true;
	public bool followY = true;

	// Use this for initialization
	void Start () 
	{
//		player = GameObject.Find("Player").transform;
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		Vector3 move = this.transform.position;
		move.z = GameObject.Find("Main Camera").transform.position.z;

		if (followX && followY)
		{
			GameObject.Find("Main Camera").transform.position = move;
		}
		else if (followX)
		{
			move.y = GameObject.Find("Main Camera").transform.position.y;
			GameObject.Find("Main Camera").transform.position = move;
		}
		else if (followY)
		{
			move.x = GameObject.Find("Main Camera").transform.position.x;
			GameObject.Find("Main Camera").transform.position = move;
		}
	}

	void OnTriggerEnter2D(Collider2D otherCollider)
	{
		CameraCollisionScript collider = otherCollider.GetComponent<CameraCollisionScript>();
		
		if (collider != null)
		{
			if (collider.restrictX)
			{
				followX = false;
			}
			else if(collider.restrictY) 
			{
				followY = false;
			}
		}	
	}

	void OnTriggerExit2D(Collider2D otherCollider)
	{
		CameraCollisionScript collider = otherCollider.GetComponent<CameraCollisionScript>();

		if (collider != null)
		{
			if (collider.restrictX)
			{
				followX = true;
			}
			else if(collider.restrictY) 
			{
				followY = true;
			}
		}	
	}
}
