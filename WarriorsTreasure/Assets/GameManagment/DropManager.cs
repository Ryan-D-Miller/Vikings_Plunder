using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropManager : MonoBehaviour {
    private static DropManager instance;

    public static DropManager Instance // creating a singleton Instance of the player so everything can be accessed within this code 
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<DropManager>();
                DontDestroyOnLoad(instance);
            }
            return instance;
        }
    }
    [SerializeField]
    private GameObject coinPrefab;

    public GameObject CointPrefab
    {
        get
        {
            return coinPrefab;
        }

    }
    [SerializeField]
    private GameObject treasureChestPrefab;

    public GameObject TreasureChestPrefab
    {
        get
        {
            return treasureChestPrefab;
        }
    }
    [SerializeField]
    private GameObject healthPotionPrefab;

    public GameObject HealthPotionPrefab
    {
        get
        {
            return healthPotionPrefab;
        }
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
