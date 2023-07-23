using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
	[SerializeField] private GlobalParameters globalParameters;

	[SerializeField] private Transform player;
	[SerializeField] private int specialCoinAmount;

    public int heightTravelled;
    public int coinsCollected;

	private void OnDisable()
	{
		EventManager.Instance.OnCoinCollision -= EventManager_OnCoinCollision;
		EventManager.Instance.OnPlayerHitSpikes -= EventManager_OnPlayerHitSpikes;
		EventManager.Instance.OnTouchLava -= EventManager_OnTouchLava;
	}

	private void Start()
	{
		EventManager.Instance.OnCoinCollision += EventManager_OnCoinCollision;
		EventManager.Instance.OnPlayerHitSpikes += EventManager_OnPlayerHitSpikes;
		EventManager.Instance.OnTouchLava += EventManager_OnTouchLava;
	}

	private void Update()
	{
		heightTravelled = Mathf.FloorToInt(player.transform.position.y) + 5;
	}

	private void EventManager_OnTouchLava()
	{
		if (PlayerPrefs.GetInt(globalParameters.PlayerPrefHeight) < heightTravelled)
		{
			PlayerPrefs.SetInt(globalParameters.PlayerPrefHeight, heightTravelled);
		}
		if (PlayerPrefs.GetInt(globalParameters.PlayerPrefCoins) < coinsCollected)
		{
			PlayerPrefs.SetInt(globalParameters.PlayerPrefCoins, coinsCollected);
		}

		EventManager.Instance.TriggerGameOver();
	}

	private void EventManager_OnPlayerHitSpikes()
	{
		if(PlayerPrefs.GetInt(globalParameters.PlayerPrefHeight) < heightTravelled)
		{
			PlayerPrefs.SetInt(globalParameters.PlayerPrefHeight, heightTravelled);
		}
		if (PlayerPrefs.GetInt(globalParameters.PlayerPrefCoins) < coinsCollected)
		{
			PlayerPrefs.SetInt(globalParameters.PlayerPrefCoins, coinsCollected);
		}

		EventManager.Instance.TriggerGameOver();
	}

	private void EventManager_OnCoinCollision(object sender, EventManager.OnCoinCollisionEventArgs e)
	{
		if (e.isSpecialCoin)
		{
			coinsCollected += specialCoinAmount;
		}
		else
		{
			coinsCollected++;
		}
	}
}
