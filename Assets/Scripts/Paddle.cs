using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour {

	public SpriteRenderer SpriteRenderer { get; private set; }	
	public Sprite vausSprite;
	public Sprite largeVausSprite;
	public Sprite laserSprite;
	public new Rigidbody2D rigidbody { get; private set; }
	public Vector2 direction { get; private set; }
	public float speed = 40f ;
	public float maxBoundAngle = 75f;
	public bool playGameStart = false;
	public bool laserActive = false;
	public Projectile laserPrefab1;
	public Projectile laserPrefab2;

	public void Awake()
	{
		this.rigidbody = GetComponent<Rigidbody2D>();
		this.SpriteRenderer = GetComponent<SpriteRenderer>();
		playGameStart = true;
	}

	private void Start()
	{		
		this.SpriteRenderer.sprite = vausSprite;		
	}

	public void ResetPaddle()
	{
		this.transform.position = new Vector2(0f, this.transform.position.y);
		this.rigidbody.velocity = Vector2.zero;
		playGameStart = true;
		laserActive = false;
		Normal();	
	}

	private void Update()
	{
		if (playGameStart)
		{
			playGameStart = false;
			SoundManager.PlaySound("gamestart"); 
		}
		if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
		{
			this.direction = Vector2.left;
		}
		else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
		{
			this.direction = Vector2.right;
		}
		else 
		{
			this.direction = Vector2.zero;
		}	
		if (Input.GetKeyDown(KeyCode.Space))
		{
			Shoot();	
		}	
	}

	private void Shoot()
	{
		if (laserActive)
		{
			SoundManager.PlaySound("laser");
			Instantiate(this.laserPrefab1, new Vector3(this.transform.position.x - 1f, this.transform.position.y + 1f, this.transform.position.z) , Quaternion.identity);
			Instantiate(this.laserPrefab2, new Vector3(this.transform.position.x + 1f, this.transform.position.y + 1f, this.transform.position.z), Quaternion.identity);
		}
	}


	private void FixedUpdate()
	{
		if (this.direction != Vector2.zero)
		{
			this.rigidbody.AddForce(this.direction * this.speed);
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		Ball ball = collision.gameObject.GetComponent<Ball>();
		if (ball != null)
		{
			SoundManager.PlaySound("vaus");

			Vector3 paddlePosition = this.transform.position;
			Vector2 contactPoint = collision.contacts[0].point; 

			float offset = paddlePosition.x - contactPoint.x;
			float width = collision.otherCollider.bounds.size.x / 2;

			float currentAngle = Vector2.SignedAngle(Vector2.up, ball.rigidbody.velocity);
			float bounceAngle = (offset / width) * this.maxBoundAngle; 
			float newAngle = Mathf.Clamp(currentAngle + bounceAngle, -this.maxBoundAngle, this.maxBoundAngle);
			
			Quaternion rotation = Quaternion.AngleAxis( newAngle, Vector3.forward);

			ball.rigidbody.velocity = rotation * Vector2.up * ball.rigidbody.velocity.magnitude ; 
		}

		Laser laser = collision.gameObject.GetComponent<Laser>();
		if (laser != null)
		{							
			laser.gameObject.SetActive(false); //destroy LCapsule (laser)
			Laser();
		}

		Enlarge enlarge = collision.gameObject.GetComponent<Enlarge>();
		if (enlarge != null)
		{
			enlarge.gameObject.SetActive(false); //destroy ECapsule (laser)
			Expand();
		}
	}

	public void Laser()
	{
		laserActive = true;
		this.SpriteRenderer.sprite = laserSprite;
		this.GetComponent<BoxCollider2D>().size = new Vector2(3.75f, 0.5f);
	}

	public void Expand()
	{
		laserActive = false;
		this.GetComponent<BoxCollider2D>().size = new Vector2(5.75f, 0.5f);
        this.SpriteRenderer.sprite = largeVausSprite;	
		SoundManager.PlaySound("expand");
	}

	public void Normal()
	{
		this.GetComponent<BoxCollider2D>().size = new Vector2(3.75f, 0.5f);
        this.SpriteRenderer.sprite = vausSprite;	
	}
}
