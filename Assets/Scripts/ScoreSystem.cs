using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreSystem : MonoBehaviour
{
    public GameObject scoreText;
    public AudioSource collectSound;
    Vector3 originalPos;
    public GameObject objectToPlace;
    public int Value;
    void Start()
    {
        Value = PlayerPrefs.GetInt("value", Score.scoreValue);
    }
    void Awake()
    {
        PlayerPrefs.GetInt("value", Score.scoreValue);
    }
    void OnTriggerEnter(Collider other)
    {
        collectSound.Play();
        Score.scoreValue += 1;
        PlayerPrefs.SetInt("value", Score.scoreValue);
    }
}
