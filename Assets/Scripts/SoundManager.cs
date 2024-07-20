using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

	public static AudioClip hitSound;
	public static AudioClip vausSound;
	public static AudioClip wallSound;
	public static AudioClip laserSound;
	public static AudioClip expandSound;
	public static AudioClip missSound;

	public static AudioClip gamestartSound;
	public static AudioClip gameoverSound;
	public static bool musicON = true;

	static AudioSource AudioSource;
	// Use this for initialization
	void Start () {

		hitSound = Resources.Load<AudioClip>("hit");
		vausSound = Resources.Load<AudioClip>("vaus");
		wallSound = Resources.Load<AudioClip>("wall");
		laserSound = Resources.Load<AudioClip>("laser");
		expandSound = Resources.Load<AudioClip>("expand");
		missSound = Resources.Load<AudioClip>("miss");

		gamestartSound = Resources.Load<AudioClip>("gamestart");
		gameoverSound = Resources.Load<AudioClip>("gameover");

		AudioSource = GetComponent<AudioSource>();
	}
	
	public static void PlaySound (string clip)
	{
		if (musicON)
		{
			switch (clip)
			{	
				case "hit":
					AudioSource.PlayOneShot(hitSound);
					break;	
				
				case "vaus":
					AudioSource.PlayOneShot(vausSound);
					break;	

				case "wall":
					AudioSource.PlayOneShot(wallSound);
					break;	

				case "laser":
					AudioSource.PlayOneShot(laserSound);
					break;	

				case "expand":
					AudioSource.PlayOneShot(expandSound);
					break;	
					
				case "miss":
					AudioSource.PlayOneShot(missSound);
					break;						

				case "gamestart":
					AudioSource.PlayOneShot(gamestartSound);
					break;	

				case "gameover":
					AudioSource.PlayOneShot(gameoverSound);
					break;	
			}
		}		
	}	
}
