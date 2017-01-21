using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveObject : MonoBehaviour {


    public bool offScreen = false;
    public float horizontalSpeed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        Vector3 newPosition = transform.position;

        newPosition.x += horizontalSpeed * Time.deltaTime;

        transform.position = newPosition;
    }

    void OnBecameInvisible()
    {
        offScreen = true;
    }
}
