using UnityEngine;
using System.Collections;

public class TransitionScript : MonoBehaviour {
	public float TimeForAnimation = 5f;
	public string LevelToLoad;
	public string RoomWhatToWhat = "";

	private Animator animator;

	// Use this for initialization
	void Start () {
		animator = this.GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	bool hasBeenCalled = false;
	void Update () {
		
		if (!hasBeenCalled)
		{
			StartCoroutine(Transition());
			hasBeenCalled = true;
		}
	}
	
	IEnumerator Transition(){

		if (RoomWhatToWhat != "")
			animator.SetTrigger (RoomWhatToWhat);

		yield return new WaitForSeconds (TimeForAnimation);
		
		Application.LoadLevel(LevelToLoad);
	}
}
