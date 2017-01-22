using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour {

    public GameObject aWave;
	public GameObject[] obstaclesToSpawn;
	public GameObject[] pickupsToSpawn;

    public float startWavesNo = 8;
    public List<WaveObject> waves;
    public List<WaveObject> obstacles;

    private float baseHeight;
    public float xOffSet;

	public bool spawnObjects = true;
	public int objectSpawnOdds = 20;
    public int minSpawnOdds = 7;

    public float objectSpeed = -3;
    public float speedIncrmenter = 0.5f;
    public float maxSpeed;

    public float diffcultyTimeIncrements = 3;
    public float timeIncrementsMin = 1;
    private float diffcultyTimer = 0;

    bool gameOn = true;

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
        if (gameOn)
        {
            SpawnObstciles();
            DeleteObstciles();
            SpeedTimer();
        }
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
                waves[waves.Count - 1].horizontalSpeed = objectSpeed;

           
            }
        }
    }

    void SpawnObstciles()
    {
        if (spawnObjects)
        {
            diffcultyTimer += Time.deltaTime;

            if (diffcultyTimer > diffcultyTimeIncrements)
            { 
          
                int decider = Random.Range(0, 4);
                Vector3 newObstcalPosition = waves[waves.Count - 1].transform.position;

                if (decider == 1)
                {
                    newObstcalPosition.y += 3;
                    newObstcalPosition.z += 5;

                    int obstacleIndex = Random.Range(0, obstaclesToSpawn.Length);

                    obstacles.Add(Instantiate(obstaclesToSpawn[obstacleIndex], newObstcalPosition, transform.rotation).GetComponent<WaveObject>());
                    obstacles[obstacles.Count - 1].horizontalSpeed = objectSpeed;
                }
                else if (decider == 2)
                {
                    newObstcalPosition.y += 4;
                    newObstcalPosition.z += 5;
                    newObstcalPosition.x += Random.Range(0, 2);
                    int pickupIndex = Random.Range(0, pickupsToSpawn.Length);

                    obstacles.Add(Instantiate(pickupsToSpawn[pickupIndex], newObstcalPosition, transform.rotation).GetComponent<WaveObject>());
                    obstacles[obstacles.Count - 1].GetComponent<WaveMovement>().timeOffSet = waves[waves.Count - 1].GetComponent<WaveMovement>().timeOffSet;
                    obstacles[obstacles.Count - 1].GetComponent<WaveMovement>().waveSize = waves[waves.Count - 1].GetComponent<WaveMovement>().waveSize + 0.1f;
                    obstacles[obstacles.Count - 1].horizontalSpeed = objectSpeed;
                }

                diffcultyTimer = 0;

                if (diffcultyTimeIncrements > timeIncrementsMin)
                {
                    diffcultyTimeIncrements -= 0.1f;
                }
            }
        }
    }

    void DeleteObstciles()
    {
        for (int i = 0; i < obstacles.Count; i++)
        {
            if(obstacles[i].offScreen)
            {
                Destroy(obstacles[i].gameObject);
                obstacles.RemoveAt(i);
            }
        }
    }

    void SpeedTimer()
    {

        if (objectSpeed > maxSpeed)
        {
            objectSpeed -= speedIncrmenter * Time.deltaTime;

            UpdatedToNewSpeed();
        }
    }

    void UpdatedToNewSpeed()
    {
        for(int i = 0; i < obstacles.Count; i++)
        {
            obstacles[i].horizontalSpeed = objectSpeed;
        }

        for(int i = 0; i < waves.Count; i++)
        {
            waves[i].horizontalSpeed = objectSpeed;
        }
    }

    public void StartTheGame()
    {
        gameOn = true;
    }
}
