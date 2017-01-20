using System.Collections;
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
	public Vector3 crabPosition;

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
		transform.position = crabPosition + new Vector3(0.0f, currentBob, 0.0f);
		transform.eulerAngles = new Vector3(0.0f, 0.0f, rotationOffset + Mathf.Sin(elapsedTime * rotateSpeed) * rotateAmount);
	}

	void HandleInput()
	{
		if (Input.GetKeyDown(upKey) && currentWave < (waves.Length - 1))
		{
			currentWave += 1;
			crabPosition = waves[currentWave].transform.position;
		}
		else if (Input.GetKeyDown(downKey) && currentWave > 0)
		{
			currentWave -= 1;
			crabPosition = waves[currentWave].transform.position;
		}
	}
}
