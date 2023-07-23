using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance;

	#region Events

	#region Movement Events

	public event Action OnPlayerStartBoost; // pressed up key
	public event Action OnPlayerStopBoost; // released up key

	public event Action OnPlayerStartAscend; // velocity bigger than zero
	public event Action OnPlayerStartDescend; // velocity lower than zero

	#endregion

	#region Collision Events

	public event Action OnPlayerHitSpikes;

	public event EventHandler<OnCoinCollisionEventArgs> OnCoinCollision;
	public class OnCoinCollisionEventArgs : EventArgs
	{
		public bool isSpecialCoin;
	}

	public event Action OnEnterLevelSection;

	public event Action OnTouchLava;

	#endregion

	public event Action OnGameOver;

	#endregion


	private void Awake()
	{
		if(Instance == null)
		{
			Instance = this;
		}
		else if(Instance != this)
		{
			Destroy(gameObject);
		}
	}

	#region Event Triggers

	#region Movement Event Triggers

	public void TriggerPlayerStartBoost()
	{
		OnPlayerStartBoost?.Invoke();
	}
	public void TriggerPlayerStopBoost()
	{
		OnPlayerStopBoost?.Invoke();
	}
	public void TriggerPlayerStartAscend()
	{
		OnPlayerStartAscend?.Invoke();
	}
	public void TriggerPlayerStartDescend()
	{
		OnPlayerStartDescend?.Invoke();
	}

	#endregion

	#region Collision Event Triggers

	public void TriggerPlayerHitSpikes()
	{
		OnPlayerHitSpikes?.Invoke();
	}

	public void TriggerCoinCollision(bool isSpecialCoin)
	{
		OnCoinCollision?.Invoke(this, new OnCoinCollisionEventArgs { isSpecialCoin = isSpecialCoin });
	}

	public void TriggerEnterLevelSection()
	{
		OnEnterLevelSection?.Invoke();
	}

	public void TriggerEnterLava()
	{
		OnTouchLava?.Invoke();
	}

	#endregion

	public void TriggerGameOver()
	{
		OnGameOver?.Invoke();
	}

	#endregion

}
