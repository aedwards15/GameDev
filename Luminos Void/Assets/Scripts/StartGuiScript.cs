using UnityEngine;
using System.Collections;

public class StartGuiScript : MonoBehaviour 
{
	public GUISkin myGuiSkin;
	public float scaleX = 0.5f;
	public float scaleY = 0.5f;
	private bool displayGui = false;
	private float Timer = 0.0f;
	
	void Update ()
	{
		Timer += Time.deltaTime;

		if (Input.GetButtonDown("Fire1") || Input.GetButtonDown("Fire2")) 
		{
			displayGui = true;
		}
	}

	void OnGUI()
	{
		GUI.skin = myGuiSkin;
		
		const int buttonWidth = 200;
		const int buttonHeight = 50;

		if (Timer > 8 || displayGui)
		{
			if (
				GUI.Button(
				new Rect(
				((float)Screen.width * scaleX) - (buttonWidth / 2),
				((float)Screen.height * scaleY) - (buttonHeight / 2),
				buttonWidth,
				buttonHeight
				),
				"START"
				)
				)
			{
				Application.LoadLevel("OpeningScene");
			}
		}
	}
}