using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour 
{
	public Ball ball { get; private set; }
	public Paddle paddle { get; private set; }
	public Brick[] bricks { get; private set; }
	public int level = 1;
	public int score = 0;
	public int highscore = 0;
	public int lives = 3;
	public PaddleLive[] paddleLives { get; private set; }
	public Transform[] capsules;

 	float timeLeft = 20.0f;

	private void Awake()
	{
		DontDestroyOnLoad(this.gameObject);		
		SceneManager.sceneLoaded += OnLevelLoaded;
	}

	private void Start()
	{
		NewGame();
	}

	private void Update()
	{		
		if (timeLeft < 0.0f)
		{
			CreateCapsule((int)Random.Range(0,100)%2);
			timeLeft = 20.0f;
		}		
		timeLeft -= Time.deltaTime; 
	}

	private void NewGame()
	{
		this.score = 0;
		this.lives = 3;
		GameOverText.Message="";
		LoadLevel(1);
	}

	private void LoadLevel(int level)
	{
		this.level = level;		
		SceneManager.LoadScene("Level0" + level);
	}

	private void OnLevelLoaded(Scene scene, LoadSceneMode mode)
	{
		this.ball = FindObjectOfType<Ball>();
		this.paddle = FindObjectOfType<Paddle>();
		this.bricks = FindObjectsOfType<Brick>();
		this.paddleLives = FindObjectsOfType<PaddleLive>();
	}

	public void Hit(Brick brick)
	{
		this.score += brick.points;

		if (Cleared())
		{
			LoadLevel(this.level + 1);
		}
	}

	public void UpdateScore(Text ScoreText)
	{
		ScoreText.text = score.ToString("00000") + "	" + highscore.ToString("00000");
	}

	private void ResetLevel()
	{
		GameOverText.Message="";
		this.ball.ResetBall();
		this.paddle.ResetPaddle();
	}

	private void GameOver()
	{
		//SceneManager.LoadScene("GameOverScene");
		PauseControl.PressPause();
		GameOverText.Message="	GAME OVER";
		if (score > highscore)
		{
			highscore = score;
		}
		SoundManager.PlaySound("gameover"); 
		Invoke("NewGame", 2f);
	}

	public void Miss()
	{
		SoundManager.PlaySound("miss"); 
		this.lives--;
		paddleLives[lives].DestroyPaddle();
		this.ball.BallOff();

		if (this.lives > 0)
		{
			Invoke("ResetLevel", 1.0f);
		} 
		else 
		{
			Invoke("GameOver", 1.0f);
		}
	}

	private bool Cleared()
	{
		for (int i = 0; i < this.bricks.Length ; i++)
		{
			if (this.bricks[i].gameObject.activeInHierarchy	&& !this.bricks[i].unbreakable)
			{
				return false;
			}
		}
		return true;
	}

	private void CreateCapsule(int i)
	{
		Instantiate(capsules[i],new Vector2(Random.Range(-16.0f, 16.0f), 15.0f), Quaternion.identity);
	}
}
