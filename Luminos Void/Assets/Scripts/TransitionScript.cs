using UnityEngine;
using System.Collections;

public class TransitionScript : MonoBehaviour {
	public float TimeForAnimation = 5f;
	public string LevelToLoad;

	// Use this for initialization
	void Start () {
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
		
		yield return new WaitForSeconds (TimeForAnimation);
		
		Application.LoadLevel(LevelToLoad);
	}
}
