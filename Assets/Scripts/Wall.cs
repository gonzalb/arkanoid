using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour {

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.name == "Ball")
		{
			//SoundManager.PlaySound("vaus");
		}		
	}
}
