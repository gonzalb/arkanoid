using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

	public new Rigidbody2D rigidbody { get; private set; }
	public float speed = 20f ;


	public void Awake()
	{
		this.rigidbody = GetComponent<Rigidbody2D>();
	}

	private void Start()
	{
		ResetBall();
	}

	public void ResetBall()
	{
		this.transform.position = Vector2.zero;
		this.rigidbody.velocity = Vector2.zero;
		BallOn();
		Invoke("SetRandomTrajectory", 2f);
	}

	private void SetRandomTrajectory()
	{
		Vector2 force = Vector2.zero;
		force.x = Random.Range(-1f, 1f);
		force.y = -1f;

		this.rigidbody.AddForce(force.normalized * this.speed);
	}

	private void FixedUpdate()
    {
        rigidbody.velocity = rigidbody.velocity.normalized * speed;
    }

	public void BallOff()
	{
		this.gameObject.SetActive(false);
	}

	public void BallOn()
	{
		this.gameObject.SetActive(true);
	}
}
