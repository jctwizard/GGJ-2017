﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrabController : MonoBehaviour 
{
	public int currentWave = 0;
	public Transform[] waves;
	public KeyCode upKey = KeyCode.UpArrow;
	public KeyCode downKey = KeyCode.DownArrow;
	public float elapsedTime = 0.0f;
	public float bobSpeed = 2.0f;
	public float bobAmount = 0.2f;
	public float rotateSpeed = 2.0f;
	public float rotateAmount = 20.0f;
	public float rotationOffset = 90.0f;
	private Vector3 crabPosition;

	public float jumpHeight = 2.0f;
	public float jumpDuration = 0.5f;
	private float jumpTime = 0.0f;
	private bool jumping = false;
	private Vector3 jumpStartPosition, jumpTargetPosition;

	float touchStart = 0.0f;

	void Start () 
	{
		if (waves.Length == 0)
		{
			Debug.Log("Add wave transforms to character controller bottom to top!");
		}

		crabPosition = waves[currentWave].transform.position;
	}

	void Update () 
	{
		elapsedTime += Time.deltaTime;

		HandleInput();
		UpdateMovement();
	}

	void UpdateMovement()
	{
		float currentBob = Mathf.Sin(elapsedTime * bobSpeed) * bobAmount;
		transform.eulerAngles = new Vector3(0.0f, 0.0f, rotationOffset + Mathf.Sin(elapsedTime * rotateSpeed) * rotateAmount);
	
		if (jumping)
		{
			jumpTime += Time.deltaTime;

			if (jumpTime > jumpDuration)
			{
				jumping = false;
				crabPosition = jumpTargetPosition;
			}
			else
			{
				float t = jumpTime / jumpDuration;
				float crabHeight = Mathf.Sin(t * Mathf.PI) * jumpHeight;
				Vector3 jumpPosition = Vector3.Lerp(jumpStartPosition, jumpTargetPosition, t);

				transform.position = jumpPosition + new Vector3(0.0f, crabHeight, 0.0f);
			}
		}
		else
		{
			transform.position = crabPosition + new Vector3(0.0f, currentBob, 0.0f);
		}
	}

	void HandleInput()
	{
		if (!jumping)
		{
			if (Input.GetKey(upKey) && currentWave < (waves.Length - 1))
			{
				currentWave += 1;
				Jump();
			}
			else if (Input.GetKey(downKey) && currentWave > 0)
			{
				currentWave -= 1;
				Jump();
			}

			if (Input.touchCount > 0)
			{
				Touch touch = Input.GetTouch(0);

				if (touch.phase == TouchPhase.Began)
				{
					touchStart = touch.position.y;
				}
				else if (touch.phase == TouchPhase.Ended)
				{
					if (touch.position.y - touchStart > 10.0f)
					{
						currentWave += 1;
						Jump();
					}
					else if (touch.position.y - touchStart < -10.0f)
					{
						currentWave -= 1;
						Jump();
					}
				}
			}
		}
	}

	void Jump()
	{
		jumping = true;
		jumpTime = 0.0f;
		jumpStartPosition = crabPosition;
		jumpTargetPosition = waves[currentWave].transform.position;
	}
}
