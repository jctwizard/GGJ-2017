using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour {

    public GameObject aWave;
    public List<WaveObject> waves;

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
                Destroy(waves[i].gameObject);
                waves.RemoveAt(i);
                Vector3 newWavesPosition = waves[waves.Count-1].transform.position;
                newWavesPosition.x += waves[waves.Count-1].GetComponent<Renderer>().bounds.size.x / 4;
                newWavesPosition.x += aWave.GetComponent<Renderer>().bounds.size.x / 2;
                newWavesPosition.y = 0;


                waves.Add(Instantiate(aWave,newWavesPosition, transform.rotation).GetComponent<WaveObject>());

                waves[waves.Count - 1].GetComponent<WaveMovement>().timeOffSet = waves[waves.Count - 2].GetComponent<WaveMovement>().timeOffSet + 0.1f;

            }
        }	
	}
}
