using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainMenuUI : MonoBehaviour
{
    private const string HEIGHT_SCORE_TEXT = "Maximum Height Travelled: ";
    private const string COIN_SCORE_TEXT = "Maximum Coins Collected: ";


    [SerializeField] GlobalParameters globalParameters;

    [SerializeField] private TMP_Text heightScoreText;
    [SerializeField] private TMP_Text coinScoreText;

    public void ResetScore()
    {
        PlayerPrefs.SetInt(globalParameters.PlayerPrefHeight, 0);

        PlayerPrefs.SetInt(globalParameters.PlayerPrefCoins, 0);

        heightScoreText.text = HEIGHT_SCORE_TEXT + PlayerPrefs.GetInt(globalParameters.PlayerPrefHeight);

        coinScoreText.text = COIN_SCORE_TEXT + PlayerPrefs.GetInt(globalParameters.PlayerPrefCoins);
    }

	private void Start()
	{
		heightScoreText.text = HEIGHT_SCORE_TEXT + PlayerPrefs.GetInt(globalParameters.PlayerPrefHeight);

		coinScoreText.text = COIN_SCORE_TEXT + PlayerPrefs.GetInt(globalParameters.PlayerPrefCoins);
	}
}
