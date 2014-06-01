using UnityEngine;
using System.Collections;

public class TileScript : MonoBehaviour {
	
	public bool IsOn = false;

	public bool Flash = false;

	public float FlashFrequency = .25f;

	public float FlashDuration = 1f;

	private float flashTime;
	private float duration;
	
	private Color tileColor;
	// Use this for initialization
	void Start () {
		if (IsOn)
		{
			this.renderer.material.color = new Color(255, 255, 255, 255);
		}

		flashTime = 0f;
		duration = FlashDuration;
		tileColor = this.renderer.material.color;
	}

	// Update is called once per frame
	void Update () {
		if (Flash)
		{
			if (flashTime > 0)
			{
				if (duration <= 0)
				{
					this.renderer.material.color = tileColor;
					IsOn = false;
				}

				flashTime -= Time.deltaTime;
				duration -= Time.deltaTime;
			}
			else
			{
				flashTime = FlashFrequency;
				duration = FlashDuration;

				this.renderer.material.color = new Color(255, 255, 255, 255);
				IsOn = true;
			}
		}
	}
}
