using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Score : MonoBehaviour {
//public static int ScoreValue = 0;
//public static int HighScoreValue = 0;


	Text ScoreText;
	// Use this for initialization
	void Start () {
		ScoreText = GetComponent<Text>();
		//ScoreValue = 0;
	}
	
	// Update is called once per frame
	void Update () {
		FindObjectOfType<GameManager>().UpdateScore(ScoreText);
	}
}
