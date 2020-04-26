using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class Buttons : MonoBehaviour
{
    public InputField iField;
    public GameObject inputField;
    string myName;
    public Button bns;
    public int Value;
    public int Value2;

    void Start()
    {
        myName = iField.text;
        Score.scoreValue = PlayerPrefs.GetInt("value");
       // Value = PlayerPrefs.GetInt("value2");
    }
    void Awake()
    {
        PlayerPrefs.SetInt("value", Score.scoreValue);
    }

    public void BTTN()
    {
        PlayerPrefs.SetInt("value", Score.scoreValue);
        myName = iField.text;
        Highscores.AddNewHighscore(myName, Score.scoreValue);
        inputField.SetActive(false);
        bns.interactable = false;
    }
}
