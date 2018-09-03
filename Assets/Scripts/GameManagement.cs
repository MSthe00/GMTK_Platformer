using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManagement : MonoBehaviour {
    public GameObject levelDone;
    public GameObject levelFail;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void LevelComplete()
    {
        levelDone.SetActive(true);
        Invoke("StartNext", 3);
    }
    public void LevelFailed()
    {
        levelFail.SetActive(true);
        Invoke("Restart", 1);
    }
    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void StartNext()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
