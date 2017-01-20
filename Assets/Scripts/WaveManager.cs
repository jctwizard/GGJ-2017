using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour {

    public GameObject aWave;
    List<WaveObject> waves;

    WaveObject lastObject;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
	
        for (int i = 0; i < waves.Count; i++)
        {
            if(waves[i].offScreen)
            {
                waves.RemoveAt(i);
                Vector3 newWavesPosition = waves[waves.Count].transform.position;
                newWavesPosition.x += waves[waves.Count].GetComponent<Renderer>().bounds.size.x / 2;
                newWavesPosition.x += aWave.GetComponent<Renderer>().bounds.size.x / 2;
               
               // Instantiate(aWave,newWavesPosition);
            }
        }	
	}
}
