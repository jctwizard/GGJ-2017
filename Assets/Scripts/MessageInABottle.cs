using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageInABottle : MonoBehaviour {

	public GameObject message;
	public int messageChance = 20;

	// Use this for initialization
	void Start () 
	{
		if (Random.Range(0, messageChance) == 0)
		{
			message.GetComponent<Renderer>().enabled = true;
			tag = "Crabtastic";
		}
		else
		{
			message.GetComponent<Renderer>().enabled = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
