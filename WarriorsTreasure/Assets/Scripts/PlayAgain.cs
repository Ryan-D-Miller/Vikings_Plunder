using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayAgain : MonoBehaviour {
    Scene scene;
    // Use this for initialization
    void Start ()
    {
        scene = SceneManager.GetActiveScene();
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}
    public void startOver()
    {
        SceneManager.LoadScene("TitleScene", LoadSceneMode.Single);
    }
}
