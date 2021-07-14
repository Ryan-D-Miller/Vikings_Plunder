using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{
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
    public void ChangeSceneTutorial()
    {
        if (scene.name == "TitleScene")
        {
            SceneManager.LoadScene("Tutorial", LoadSceneMode.Single);
            GameManager.Instance.PlayerHealth = 50;
            GameManager.Instance.CollectTreasure = 0;
        }
        else if (scene.name == "Tutorial")
        {
            SceneManager.LoadScene("TitleScene", LoadSceneMode.Single);
        }

    }
}
