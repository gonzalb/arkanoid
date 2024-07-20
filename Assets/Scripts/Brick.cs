using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour 
{
	public SpriteRenderer SpriteRenderer { get; private set; }	
	public Sprite[] states;
	public int health { get; private set; }	
	public int points = 100;
	public bool unbreakable;

	private void Awake()
	{
		this.SpriteRenderer = GetComponent<SpriteRenderer>();
	}

	private void Start()
	{
		if (!this.unbreakable)
		{
			this.health = this.states.Length;
			this.SpriteRenderer.sprite = this.states[this.health - 1];
		}
	}

	private void HitBall()
	{
		if (this.unbreakable)
		{
			return;
		}
		
		this.health--;
		SoundManager.PlaySound("hit");
		if (this.health <= 0)
		{
			this.gameObject.SetActive(false);
		}
		else
		{
			this.SpriteRenderer.sprite = this.states[this.health - 1];			
		}

		FindObjectOfType<GameManager>().Hit(this);
	}

	private void HitBullet()
	{
		if (this.unbreakable)
		{
			return;
		}
		
		this.health--;
		//SoundManager.PlaySound("hit");
		if (this.health <= 0)
		{
			this.gameObject.SetActive(false);
		}
		else
		{
			this.SpriteRenderer.sprite = this.states[this.health - 1];			
		}
		FindObjectOfType<GameManager>().Hit(this);
	}



	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.name == "Ball")
		{
			HitBall();
		}
		else if (collision.gameObject.tag == "Bullet")
		{
			HitBullet();
		}

	}	
}
