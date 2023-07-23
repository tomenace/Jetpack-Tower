using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCinemachineValues : MonoBehaviour
{
	private enum SmoothCameraTransition
	{
		Idle,
		MoveUp,
		MoveDown,
	}
	
	[SerializeField] private float downMovementScreenY = 0.25f;
	[SerializeField] private float upMovementScreenY = 0.75f;
	[SerializeField] private float cameraSmoothTransitionSpeedMultiplier = 0.8f;

	private CinemachineFramingTransposer cinemachineFramingTransposer;
	private SmoothCameraTransition cameraTransition;

	private void Start()
	{
		cinemachineFramingTransposer = GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineFramingTransposer>();
		EventManager.Instance.OnPlayerStartAscend += EventManager_OnPlayerStartAscend;
		EventManager.Instance.OnPlayerStartDescend += EventManager_OnPlayerStartDescend;
	}

	private void Update()
	{
		handleSmoothCameraTransition();
	}

	private void handleSmoothCameraTransition()
	{
		switch (cameraTransition)
		{
			// Camera should not change y position.
			default:
			case SmoothCameraTransition.Idle:
				break;

			// Move y position up
			case SmoothCameraTransition.MoveUp:
				cinemachineFramingTransposer.m_ScreenY -= Time.deltaTime * cameraSmoothTransitionSpeedMultiplier;

				if (cinemachineFramingTransposer.m_ScreenY <= downMovementScreenY)
				{
					cinemachineFramingTransposer.m_ScreenY = downMovementScreenY;
					cameraTransition = SmoothCameraTransition.Idle;
				}
				break;

			// move y position down
			case SmoothCameraTransition.MoveDown:
				cinemachineFramingTransposer.m_ScreenY += Time.deltaTime * cameraSmoothTransitionSpeedMultiplier;

				if (cinemachineFramingTransposer.m_ScreenY >= upMovementScreenY)
				{
					cinemachineFramingTransposer.m_ScreenY = upMovementScreenY;
					cameraTransition = SmoothCameraTransition.Idle;
				}
				break;
		}
	}

	private void EventManager_OnPlayerStartDescend()
	{
		cameraTransition = SmoothCameraTransition.MoveUp;
	}

	private void EventManager_OnPlayerStartAscend()
	{
		cameraTransition = SmoothCameraTransition.MoveDown;
	}

	private void OnDisable()
	{
		EventManager.Instance.OnPlayerStartAscend -= EventManager_OnPlayerStartAscend;
		EventManager.Instance.OnPlayerStartDescend -= EventManager_OnPlayerStartDescend;
	}
}
