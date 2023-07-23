using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalParameters : ScriptableObject
{
    private const string PLAYER_PREF_HEIGHT = "ppHeight";
    public string PlayerPrefHeight { get { return PLAYER_PREF_HEIGHT; } }

    private const string PLAYER_PREF_COINS = "ppCoins";
    public string PlayerPrefCoins { get { return PLAYER_PREF_COINS; } }
}
