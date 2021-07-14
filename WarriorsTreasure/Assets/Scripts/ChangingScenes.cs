using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangingScenes : MonoBehaviour
{
    private bool onExit = false;
    Scene scene;
    // Use this for initialization
    // Update is called once per frame
    private void Start()
    {
        scene = SceneManager.GetActiveScene();
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow) && onExit == true)
        {
            ChangeScene();
        }
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")// when the player enters one of the entrance or exits currently the game pauses
        {
            onExit = true;
        }
    }
    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")// when the player enters one of the entrance or exits currently the game pauses
        {
            
            onExit = false;
            Debug.Log("onExit is " + onExit);
        }
    }
    public void ChangeScene()
    {
        if(scene.name == "TitleScene")
        {
            SceneManager.LoadScene("Level1", LoadSceneMode.Single);
			Cursor.visible = false;
            GameManager.Instance.PlayerHealth = 100;
            GameManager.Instance.CollectTreasure = 0;
        }
        else if(scene.name == "Level1")
        {
            SceneManager.LoadScene("BossLevel", LoadSceneMode.Single);
			Cursor.visible = false;
            
        }
        else if(scene.name == "Tutorial")
        {
            SceneManager.LoadScene("TitleScene", LoadSceneMode.Single);
			Cursor.visible = true;
        }
        else if(scene.name == "BossLevel")
        {
            SceneManager.LoadScene("Winning", LoadSceneMode.Single);
			Cursor.visible = true;
        }
        else if(scene.name == "Winning")
        {
            SceneManager.LoadScene("TitleScene", LoadSceneMode.Single);
            Cursor.visible = true;
        }
        
    }
}
