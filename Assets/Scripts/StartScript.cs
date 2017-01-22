using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScript : MonoBehaviour {

	public TextMesh HighscoreText;

	// Use this for initialization
	void Start () {
		HighscoreText.text = (PlayerPrefs.GetInt("Highscore")).ToString();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.anyKeyDown || Input.touchCount > 0)
		{
			SceneManager.LoadScene("Main");
		}
	}
}
