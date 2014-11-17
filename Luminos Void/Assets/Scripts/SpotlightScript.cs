using UnityEngine;
using System.Collections;

public class SpotlightScript : MonoBehaviour 
{
	public bool UseRandom = true;
	
	public bool IsOn = false;
	
	public bool Flash = false;
	
	public float FlashFrequency = .25f;
	
	public float FlashDuration = 1f;
	
	private float flashTime;
	private float duration;
	private GameObject light;
	
	// Use this for initialization
	void Start () 
	{

		light = transform.FindChild ("light").gameObject;
		
		if (UseRandom)
		{
			float val = Random.Range(0, 10);
			
			if (val <= 7f)
				IsOn = true;
			else
			{
				Flash = true;
				
				val = Random.Range(0.1f, 3);
				FlashFrequency = val;
				
				val = Random.Range(0.01f, FlashFrequency);
				FlashDuration = val;
			}
		}
		
		if (IsOn)
		{
			light.SetActive(true);
		}
		else
		{
			light.SetActive(false);
		}
		
		flashTime = 0f;
		duration = FlashDuration;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Flash)
		{
			if (flashTime > 0)
			{
				if (duration <= 0)
				{
					light.SetActive(false);
					IsOn = false;
				}
				
				flashTime -= Time.deltaTime;
				duration -= Time.deltaTime;
			}
			else
			{
				flashTime = FlashFrequency;
				duration = FlashDuration;

				light.SetActive(true);
				IsOn = true;
			}
		}
	}
}
