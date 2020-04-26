using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedDist : MonoBehaviour
{

    public Transform Object1;
    public Transform Object2;

    public float Distance = 0;
    public static float TimeBetweenObjects = 0f;
    public static float Speed = 0;
    public Text speedText;

    public static float gameTimer; //in seconds
    private string remainingTime;
    private int seconds;
    private int minutes;

    void Start()
    {
        Time.timeScale = 1.0f;
    }

    void Update()
    {
        TimeBetweenObjects = Time.timeScale = 1.0f;
        Distance = Vector3.Distance(Object1.transform.position, Object2.transform.position);
        Object1.transform.position = Vector3.Slerp(Object1.position, Object2.position, Speed);
        Speed = Distance / TimeBetweenObjects;
        speedText.text =  "Speed: " + Speed.ToString("##.##");
    }
   
}