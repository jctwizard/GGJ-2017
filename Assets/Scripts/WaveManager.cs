using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour {

    public GameObject aWave;
    public float startWavesNo = 8;
    public List<WaveObject> waves;

    private float baseHeight;
    public float xOffSet;

	// Use this for initialization
	void Start ()
    {

        baseHeight = transform.position.y;

		for(float i = 0; i < startWavesNo; i++)
        {
            Vector3 newWavesPosition;

            if (i == 0)
            {
                newWavesPosition = transform.position;
                newWavesPosition.x += xOffSet;
            }
            else
            {
                newWavesPosition = waves[waves.Count - 1].transform.position;
                newWavesPosition.x += waves[waves.Count - 1].GetComponent<Renderer>().bounds.size.x / 4;
            }
           
            newWavesPosition.x += aWave.GetComponent<Renderer>().bounds.size.x / 2;
            newWavesPosition.y = baseHeight;

            waves.Add(Instantiate(aWave, newWavesPosition, transform.rotation).GetComponent<WaveObject>());

            waves[waves.Count - 1].GetComponent<WaveMovement>().timeOffSet = i / 10.0f;
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        SpawnWaves();
        
	}

    void SpawnWaves()
    {
        for (int i = 0; i < waves.Count; i++)
        {
            if (waves[i].offScreen)
            {
                Destroy(waves[i].gameObject);
                waves.RemoveAt(i);
                Vector3 newWavesPosition = waves[waves.Count - 1].transform.position;
                newWavesPosition.x += waves[waves.Count - 1].GetComponent<Renderer>().bounds.size.x / 4;
                newWavesPosition.x += aWave.GetComponent<Renderer>().bounds.size.x / 2;
                newWavesPosition.y = baseHeight;


                waves.Add(Instantiate(aWave, newWavesPosition, transform.rotation).GetComponent<WaveObject>());

                waves[waves.Count - 1].GetComponent<WaveMovement>().timeOffSet = waves[waves.Count - 2].GetComponent<WaveMovement>().timeOffSet + 0.1f;

            }
        }
    }
}
