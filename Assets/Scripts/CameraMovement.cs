using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraMovement : MonoBehaviour
{
	private enum FollowState
	{
		Idle,
		MoveUp,
		MoveDown
	}

    [SerializeField] private Transform playerTransform;

    [SerializeField] private float yOffsetUp;
    [SerializeField] private float yOffsetDown;

	[SerializeField] private float downMovementScreenY = 0.25f;
	[SerializeField] private float upMovementScreenY = 0.75f;

	private CinemachineVirtualCamera virtualCamera;

	[SerializeField] private FollowState followState;
	private float cameraXPosition;
	private float cameraZPosition;

	private void Start()
	{
		virtualCamera = GetComponent<CinemachineVirtualCamera>();

		cameraXPosition = transform.position.x;
		cameraZPosition = transform.position.z;

		EventManager.Instance.OnPlayerStartAscend += EventManager_OnPlayerStartAscend;
		EventManager.Instance.OnPlayerStartDescend += EventManager_OnPlayerStartDescend;
		EventManager.Instance.OnPlayerStartBoost += EventManager_OnPlayerStartBoost;
		EventManager.Instance.OnPlayerStopBoost += EventManager_OnPlayerStopBoost;
		
	}

	private void EventManager_OnPlayerStopBoost()
	{
		followState = FollowState.Idle;
	}

	private void EventManager_OnPlayerStartBoost()
	{
		followState = FollowState.Idle;
	}

	private void EventManager_OnPlayerStartDescend()
	{
		virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>().m_ScreenY = downMovementScreenY;


		followState = FollowState.MoveDown;
	}

	private void EventManager_OnPlayerStartAscend()
	{
		virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>().m_ScreenY = upMovementScreenY;


		followState = FollowState.MoveUp;
	}

	private void OnDisable()
	{
		EventManager.Instance.OnPlayerStartAscend -= EventManager_OnPlayerStartAscend;
		EventManager.Instance.OnPlayerStartDescend -= EventManager_OnPlayerStartDescend;
		EventManager.Instance.OnPlayerStartBoost -= EventManager_OnPlayerStartBoost;
		EventManager.Instance.OnPlayerStopBoost -= EventManager_OnPlayerStopBoost;
	}

	private void Update()
	{
		switch (followState)
		{
			default:
			case FollowState.Idle:
				break;
			case FollowState.MoveUp:
				if (playerTransform.position.y > transform.position.y - yOffsetDown)
				{
					transform.position = new Vector3(cameraXPosition, playerTransform.position.y + yOffsetDown, cameraZPosition);
				}
				break;
			case FollowState.MoveDown:
				if (playerTransform.position.y < transform.position.y + yOffsetUp)
				{
					transform.position = new Vector3(cameraXPosition, playerTransform.position.y - yOffsetUp, cameraZPosition);
				}
				break;
		}



	}
}
