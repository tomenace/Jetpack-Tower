using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameSceneUI : MonoBehaviour
{
	[SerializeField] private GlobalParameters globalParameters;
	[SerializeField] private GameObject gameOverUI;
	[SerializeField] private TMP_Text heightHighscore;
	[SerializeField] private TMP_Text coinsHighscore;


	[SerializeField] ScoreManager scoreManager;

	private const string HEIGHT_DEFAULT_TEXT = "Height: ";
	private const string COIN_DEFAULT_TEXT = "Coins: ";

	[SerializeField] private TMP_Text heightText;
	[SerializeField] private TMP_Text coinText;

	[SerializeField] private TMP_Text escapeText;

	private void OnDisable()
	{
		EventManager.Instance.OnGameOver -= EventManager_OnGameOver;
	}

	private void Start()
	{
		heightText.text = HEIGHT_DEFAULT_TEXT + "0";
		coinText.text = COIN_DEFAULT_TEXT + "0";

		StartCoroutine(EscapeTextDisappear());

		EventManager.Instance.OnGameOver += EventManager_OnGameOver;
	}

	private void Update()
	{
		heightText.text = HEIGHT_DEFAULT_TEXT + scoreManager.heightTravelled;
		coinText.text = COIN_DEFAULT_TEXT + scoreManager.coinsCollected;
	}

	private void EventManager_OnGameOver()
	{
		heightHighscore.text += PlayerPrefs.GetInt(globalParameters.PlayerPrefHeight);
		coinsHighscore.text += PlayerPrefs.GetInt(globalParameters.PlayerPrefCoins);

		gameOverUI.SetActive(true);
	}

	private IEnumerator EscapeTextDisappear()
	{
		yield return new WaitForSeconds(3f);

		escapeText.gameObject.SetActive(false);
	}

}
