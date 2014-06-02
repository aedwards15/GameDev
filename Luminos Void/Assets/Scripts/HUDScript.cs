using UnityEngine;
using System.Collections;

public class HUDScript : MonoBehaviour 
{	
	private float originalWidth = 720;
	private float originalHeight = 1280;
	private Vector3 scale;

	// load a custom gui skin
	public GUISkin myGuiSkin;

	// load textures
	public Texture2D bgImage; 
	public Texture2D fgImage; 
	
	// a float between 0.0 and 1.0
//	private float playerEnergy = 1; 

	// Scale screen movement to player
	public float moveX = 37.5f;
	public float moveY = 118.8f;

	// Width and height of health bar
	public int barWidth = 96;
	private int barHeight = 32;

	// x and y position for health bar
	private int x = 0;
	public int y = -150;

	private HealthScript health;

	public float maxHp = 25;
	
	void OnGUI () 
	{
		x = -barWidth/2;
		scale.x = Screen.width / originalWidth;
		scale.y = Screen.height / originalHeight;
		scale.z = 1;

		Matrix4x4 svMat = GUI.matrix;
		GUI.matrix = Matrix4x4.TRS( Vector3.zero, Quaternion.identity, scale);

		health = GetComponent<HealthScript>();
//		playerEnergy = (float)health.hp / maxHp;
		GUI.skin = myGuiSkin;
		barHeight = barWidth/8;

//		GUI.Box (new Rect (0,0,w,h), bgImage);
//		GUI.Box (new Rect (0,0,w,h), fgImage);

//		playerEnergy = (transform.position.y - 12) / -12; 
		// Create one Group to contain both images
		// Adjust the first 2 coordinates to place it somewhere else on-screen
		GUI.BeginGroup (new Rect (this.transform.position.x * moveX + 360 + x, // + Screen.width / 2 + x,
		                          this.transform.position.y * -moveY + 640 + y, // + Screen.height / 2 + y,
		                          barWidth,
		                          barHeight));
		
		// Draw the background image
		GUI.Box (new Rect (0,0,barWidth,barHeight), bgImage);
		
		// Create a second Group which will be clipped
		// We want to clip the image and not scale it, which is why we need the second Group
		GUI.BeginGroup (new Rect (0,0,((float)health.hp / maxHp) * barWidth, barHeight));
		
		// Draw the foreground image
		GUI.Box (new Rect (0,0,barWidth,barHeight), fgImage);
		
		// End both Groups
		GUI.EndGroup ();
		
		GUI.EndGroup ();

		GUI.matrix = svMat;
	}
	
}