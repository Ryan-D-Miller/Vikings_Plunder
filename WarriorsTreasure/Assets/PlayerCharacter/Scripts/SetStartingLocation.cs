using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SetStartingLocation : MonoBehaviour {
    Scene scene;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void Awake()
    {
        scene = SceneManager.GetActiveScene();
        if (scene.name == "Level1")
        {
            transform.position = new Vector3(-14, -3, 0);
        }
        else if (scene.name == "Tutorial")
        {
            transform.position = new Vector3(-11, -3, 0);
        }
        else if(scene.name == "BossLevel")
        {
            transform.position = new Vector3(-13, -6, 0);
        }
    }
}
