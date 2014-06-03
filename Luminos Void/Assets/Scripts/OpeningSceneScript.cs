using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OpeningSceneScript : MonoBehaviour {
	
	private Animator animator;
	private List<Transform> childs;

	// Use this for initialization
	void Start () {

		childs = new List<Transform> ();
		for (int i = 0; i < transform.childCount; i++)
		{
			Transform child = transform.GetChild(i);

			childs.Add(child);
		}

		animator = childs [0].GetComponent<Animator> ();

		animator.SetTrigger ("BeginAnimation");
		StartCoroutine (Wait ());
	}
	
	// Update is called once per frame
	void Update () {

	}
	
	IEnumerator Wait() {
		yield return new WaitForSeconds(13.5f); //this will wait 5 seconds 
		
		Application.LoadLevel("Room1");
	}
}