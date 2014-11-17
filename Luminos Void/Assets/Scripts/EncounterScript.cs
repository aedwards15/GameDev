using UnityEngine;
using System.Collections;

public class EncounterScript : MonoBehaviour {

	public float TimeForAnimation = 5f;
	public Transform BossPrefab;

	private GameObject doctor;
	private Vector3 docPosition;
	ConversationScript convoScript;
	void Awake () {
		doctor = GameObject.FindGameObjectWithTag ("Doctor");
		docPosition = doctor.transform.position;
		convoScript = transform.GetComponentInParent<ConversationScript> ();
	}

	// Use this for initialization
	void Start () {
	
	}

	private bool hasBeenTriggered = false;
	private bool hasBeenCalled = false;
	// Update is called once per frame
	void Update () {

		if (!hasBeenCalled && hasBeenTriggered)
		{
			Animator animate = doctor.GetComponent<Animator> ();
			convoScript.enabled = true;
			animate.SetTrigger("BeginEncounter");
			StartCoroutine(Conversation());
			hasBeenCalled = true;
		}
	}
	
	IEnumerator Conversation(){
		
		yield return new WaitForSeconds (TimeForAnimation);

		var boss = Instantiate(BossPrefab, docPosition, Quaternion.Euler(0, 0, 0)) as Transform;

		GameObject player = GameObject.FindGameObjectWithTag ("Player");
		PlayerScript play = player.GetComponent<PlayerScript> ();
		play.Frozen = false;
		Destroy (doctor);
		Destroy (gameObject);
		//cameraScript.enabled = true;
	}

	void OnTriggerEnter2D(Collider2D collider)
	{
		PlayerScript player = collider.gameObject.GetComponent<PlayerScript> ();

		if (player != null)
		{
			player.Frozen = true;
			hasBeenTriggered = true;
		}

	}
}
