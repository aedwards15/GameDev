using UnityEngine;
using System.Collections;

public class PlayAgainGuiScript : MonoBehaviour 
{
	public GUISkin myGuiSkin;
	public float scaleX = 0.5f;
	public float scaleY = 0.5f;

	void OnGUI()
	{
		GUI.skin = myGuiSkin;
		
		const int buttonWidth = 500;
		const int buttonHeight = 100;

		if (
			GUI.Button(
			new Rect(
			((float)Screen.width * scaleX) - (buttonWidth / 2),
			((float)Screen.height * scaleY) - (buttonHeight / 2),
			buttonWidth,
			buttonHeight
			),
			"PLAY AGAIN"
			)
			)
		{
			Application.LoadLevel("Start");
		}
	}
}