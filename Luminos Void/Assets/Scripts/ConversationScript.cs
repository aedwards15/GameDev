using UnityEngine;
using System.Collections;

public class ConversationScript : MonoBehaviour {

	public float TimeForAnimation = 10f;
	GameObject convoObject;
	GameObject player;
	Vector3 cameraPos;
	GameObject cameraObj;
	CameraScript cameraScript;
	// Use this for initialization

	void Awake ()
	{


	}

	void Start () {
		convoObject = GameObject.FindGameObjectWithTag ("Conversation");
		player = GameObject.FindGameObjectWithTag ("Player");
		cameraScript = player.GetComponent<CameraScript> ();
		cameraScript.enabled = false;
		
		player.collider2D.enabled = false;
		
		//foreach (GameObject game in gameObjects)
			//game.renderer.material.color = new Color(game.renderer.material.color.r, game.renderer.material.color.g, game.renderer.material.color.b, 0);

	}
	
	// Update is called once per frame
	bool hasBeenCalled = false;
	void Update () {

		if (!hasBeenCalled)
		{

			Animator animate = convoObject.GetComponent<Animator>();
			animate.SetTrigger("BeginConversation");
			StartCoroutine(Conversation());
			hasBeenCalled = true;
		}
	}

	IEnumerator Conversation(){

		yield return new WaitForSeconds (TimeForAnimation);

		player.collider2D.enabled = true;
		cameraScript.enabled = true;
	}
}
