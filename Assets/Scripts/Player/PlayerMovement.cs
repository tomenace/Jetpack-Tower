using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	private const float MIN_CAMERA_VELOCITY_OFFSET = 0.05f;

	[SerializeField] private SpriteRenderer sr;
	[SerializeField] private Rigidbody2D rb;
    [SerializeField] private float xMovementSpeed;
	[SerializeField] private float yMovementSpeed;
	[SerializeField] private float yMovementMultiplier;
	[SerializeField] private float xMaxSpeed;
	[SerializeField] private float yMaxSpeed;
	[SerializeField] private float cameraChangeSmoothingOffset;
	[SerializeField] private float xVelocityDecreaseSpeed;

	private bool hasMovedUp;
	private bool ascendEventTriggered;
	private bool descendEventTriggered;
	private bool canMove;

	private void OnDisable()
	{
		EventManager.Instance.OnPlayerHitSpikes -= EventManager_OnPlayerHitSpikes;
		EventManager.Instance.OnTouchLava -= EventManager_OnTouchLava;
	}

	private void Start()
	{
		canMove = true;

		EventManager.Instance.OnPlayerHitSpikes += EventManager_OnPlayerHitSpikes;
		EventManager.Instance.OnTouchLava += EventManager_OnTouchLava;
	}

	private void Update()
	{
		if (!canMove) return;

		HandleMovement();
		HandleSpriteDirection();
		HandleCameraEvents();
	}

	private void EventManager_OnTouchLava()
	{
		canMove = false;
		rb.gravityScale = 0;
		rb.velocity = Vector2.zero;
		Debug.Log("touched lava, movement frozen");
	}

	private void EventManager_OnPlayerHitSpikes()
	{
		canMove = false;
		rb.gravityScale = 0;
		rb.velocity = Vector2.zero;
		Debug.Log("spike collision detected, movement frozen");
	}

	private void HandleSpriteDirection()
	{
		if (rb.velocity.x < 0)
		{
			sr.flipX = true;
		}
		else if(rb.velocity.x > 0)
		{
			sr.flipX = false;
		}
	}

	private void HandleMovement()
	{
		if (Input.GetKey(KeyCode.LeftArrow))
		{
			if(rb.velocity.x < xMaxSpeed)
			{
				rb.velocity = new Vector2(rb.velocity.x + xMovementSpeed * Time.deltaTime *  -1, rb.velocity.y);
			}
		}
		else if (Input.GetKey(KeyCode.RightArrow))
		{
			if (rb.velocity.x < xMaxSpeed)
			{
				rb.velocity = new Vector2(rb.velocity.x + xMovementSpeed * Time.deltaTime, rb.velocity.y);
			}
		}
		else
		{
			if (rb.velocity.x > 0)
			{
				rb.velocity = new Vector2(rb.velocity.x - Time.deltaTime * xVelocityDecreaseSpeed, rb.velocity.y);
				if (rb.velocity.x < 0)
				{
					rb.velocity = new Vector2(0, rb.velocity.y);
				}
			}
			else if (rb.velocity.x < 0)
			{
				rb.velocity = new Vector2(rb.velocity.x + Time.deltaTime * xVelocityDecreaseSpeed, rb.velocity.y);
				if (rb.velocity.x > 0)
				{
					rb.velocity = new Vector2(0, rb.velocity.y);
				}
			}
		}

		if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.Space))
		{
			if (!hasMovedUp) hasMovedUp = true;
			if(rb.velocity.y < yMaxSpeed)
			{
				float totalYVelocity = rb.velocity.y < 0 ? yMovementSpeed * yMovementMultiplier : yMovementSpeed;

				// below is the equivelant of the lambda expression above
				/*if (rb.velocity.y < 0)
				{
					rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y + yMovementSpeed * yMovementMultiplier * Time.deltaTime);
				}
				else
				{
					rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y + yMovementSpeed * Time.deltaTime);
				}*/

				rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y + totalYVelocity * Time.deltaTime);
			}
			else
			{
				rb.velocity = new Vector2(rb.velocity.x, yMaxSpeed);
			}
		}
	}

	private void HandleCameraEvents()
	{
		if (Input.GetKeyDown(KeyCode.UpArrow))
		{
			EventManager.Instance.TriggerPlayerStartBoost();
		}

		if (Input.GetKeyUp(KeyCode.UpArrow))
		{
			EventManager.Instance.TriggerPlayerStopBoost();
		}

		if(!ascendEventTriggered && rb.velocity.y > MIN_CAMERA_VELOCITY_OFFSET + cameraChangeSmoothingOffset)
		{
			if (hasMovedUp) EventManager.Instance.TriggerPlayerStartAscend();
			descendEventTriggered = false;
			ascendEventTriggered = true;
		}

		if(!descendEventTriggered && rb.velocity.y < -MIN_CAMERA_VELOCITY_OFFSET - cameraChangeSmoothingOffset)
		{
			if (hasMovedUp) EventManager.Instance.TriggerPlayerStartDescend();
			ascendEventTriggered = false;
			descendEventTriggered = true;
		}
	}
}
