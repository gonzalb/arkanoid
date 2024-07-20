using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleLive : MonoBehaviour {

	private void Awake()
	{
		DontDestroyOnLoad(this.gameObject);		
	}
	
	public void DestroyPaddle()
	{		
		this.gameObject.SetActive(false);		
	}
}
