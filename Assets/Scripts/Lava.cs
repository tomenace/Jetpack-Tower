using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour
{
	[SerializeField] private Transform playerTransform;

    [SerializeField] private float moveSpeed;
	[SerializeField] private float maxDistance;

	private bool isPlayerAlive = true;

	private void OnDisable()
	{
		EventManager.Instance.OnPlayerHitSpikes -= EventManager_OnPlayerHitSpikes;
		EventManager.Instance.OnTouchLava -= EventManager_OnTouchLava;
	}

	private void Start()
	{
		EventManager.Instance.OnPlayerHitSpikes += EventManager_OnPlayerHitSpikes;
		EventManager.Instance.OnTouchLava += EventManager_OnTouchLava;
	}

	private void Update()
	{
		if (isPlayerAlive)
		{
			if(playerTransform.position.y - maxDistance > transform.position.y)
			{
				transform.position = new Vector3(0, playerTransform.position.y - maxDistance, 0);
			}

			transform.position = new Vector3(0, transform.position.y + moveSpeed * Time.deltaTime, 0);
		}
	}

	private void EventManager_OnTouchLava()
	{
		isPlayerAlive = false;
	}

	private void EventManager_OnPlayerHitSpikes()
	{
		isPlayerAlive = false;
	}
}
