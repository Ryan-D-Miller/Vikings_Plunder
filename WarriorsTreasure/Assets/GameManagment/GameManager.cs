using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;
   


    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {

                GameObject go = new GameObject();
                go.name = "Game Manager";
                instance = go.AddComponent<GameManager>();

                DontDestroyOnLoad(go);
            }
            return instance;
        }
    }


   // public int CollectedTreasure { get; set; }
    public float PlayerHealth { get; set; }
    public float CollectTreasure { get; set; }
   /* private int collectTreasure;
    public int CollectTreasure
    {
        get
        {
            return collectTreasure;
        }

        set
        {
            collectTreasure = value;
        }
    }*/


    void Awake()
    {
        if (instance == null)
        {
            instance = this;

            DontDestroyOnLoad(this.gameObject);
            PlayerHealth = 100;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        //PlayerHealth = Player.Instance.Health;
        PlayerHealth = 100;
    }
}
