using UnityEngine;
using System.Collections;

public class LevelTriggerScript : MonoBehaviour 
{
	public string level;

	// Use this for initialization
	void Start () { }
	
	// Update is called once per frame
	void Update () { }

	void OnTriggerEnter2D(Collider2D otherCollider)
	{
		PlayerScript player = otherCollider.gameObject.GetComponent<PlayerScript>();
		
		if (player != null)
		{
			Application.LoadLevel(level);
		}
	}
}
