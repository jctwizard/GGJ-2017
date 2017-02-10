using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartScript : MonoBehaviour {

    public GameObject no3;
    public GameObject no2;
    public GameObject no1;

    public List<WaveManager> waveMangers;

    float timer = 0;

    public float time1;
    public float time2;
    public float time3;
    public float time4;

    bool done = false;

    
	// Use this for initialization
	void Start () {

        GetComponent<AudioSource>().Play();
	}
	
	// Update is called once per frame
	void Update ()
    {

        timer += Time.deltaTime;
        if (!done)
        {
            if (timer > time4)
            {
                no1.GetComponent<MeshRenderer>().enabled = false;
               
                done = true;
            }
            else if (timer > time3)
            {
                no2.GetComponent<MeshRenderer>().enabled = false;
                no1.GetComponent<MeshRenderer>().enabled = true;
            }
            else if (timer > time2)
            {
                no3.GetComponent<MeshRenderer>().enabled = false;
                no2.GetComponent<MeshRenderer>().enabled = true;
            }
            else if (timer > time1)
            {

                no3.GetComponent<MeshRenderer>().enabled = true;
                for (int i = 0; i < waveMangers.Count; i++)
                {
                    waveMangers[i].StartTheGame();
                }
            }
        }
	}
}
