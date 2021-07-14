using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour {
    private float fillAmount;
    [SerializeField]
    private float lerpSpeed;

    [SerializeField]
    private Image content;
   /* public float Value
    {
        set
        {
            fillAmount = Map(GameManager.Instance.PlayerHealth, 0, 50, 0, 1);
        }
    }*/
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
       fillAmount = Map(GameManager.Instance.PlayerHealth, 0, 100, 0, 1);
        HandleBar();
	}
    private void HandleBar()
    {
        if (fillAmount != content.fillAmount)
        {
            content.fillAmount = Mathf.Lerp(content.fillAmount, fillAmount, Time.deltaTime * lerpSpeed);
        }
    }
    private float Map(float value, float inMin, float inMax, float outMin, float outMax)//value is your current value the in min is the minuim it can reach and in max is the maxuim
    {
        return (value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin; // this code basically allows you to have different variables for min and max of both input and out put
        //in the end just divides the current value by the maxium value//(80 - 0) * (1 - 0) / (100-0) + 0
    }
}
