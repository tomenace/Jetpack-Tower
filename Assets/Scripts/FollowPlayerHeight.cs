using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FollowPlayerHeight : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;

	private void Update()
	{
		transform.position = new Vector3(transform.position.x, playerTransform.position.y, transform.position.z);
	}
}
