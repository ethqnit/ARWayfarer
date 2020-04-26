using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System;
using UnityEngine.UI;
using ChartboostSDK;
using UnityEngine.SceneManagement;

public class ARTapToPlaceObject : MonoBehaviour
{
    public static ARTapToPlaceObject Instance { set; get; }
    public GameObject objectToPlace;
    public GameObject placementIndicator;
    public GameObject Canvas;

    public float latitude;
    public float longitude;
    public float latitudes;
    public float longitudes;
    public Text gpsText;
    public static bool gpsStarted = false;

    private ARRaycastManager arOrigin;
    private Pose placementPose;
    private bool placementPoseIsValid = false;
    public static float gameTimer; //in seconds
    public float Current = 0f;
    public float Starting = 10f;
    private string remainingTime;
    private int seconds;
    private int minutes;
    public float t;
    public Text TimersText;
    public Text IdleCountText;
    public float timeToGo;
    public AudioSource dingTong;
    public int Value;
    public Text HighScore;
    public GameObject NewRecord;

    void Start()
    {
      Value = PlayerPrefs.GetInt("Value", Score.scoreValue);
        objectToPlace.SetActive(false);
        Current = Starting;
        SpeedDist.Speed = 0;
        Screen.sleepTimeout = 0;
        arOrigin = FindObjectOfType<ARRaycastManager>();
        Time.timeScale = 1.0f;
        if (Input.location.isEnabledByUser)
            StartCoroutine(StartLocationService());
    }

    private IEnumerator StartLocationService()
    {

        Input.location.Start();
        while (Input.location.status == LocationServiceStatus.Initializing)
        {
            yield return new WaitForSeconds(1);

        }

        if (Input.location.status == LocationServiceStatus.Running)
        {
            yield return new WaitForSeconds(0.01f);
        }
    }

    void Update()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        UpdatePlacementPose();
        UpdatePlacementIndicator();
        Input.location.Start();

        IdleCountText.text = "Idle Count: " + Current.ToString("#");

        gpsText.text = "Lat: " + latitude + "\n Lon: " + longitude;

        t = gameTimer + Time.timeSinceLevelLoad;
        seconds = Mathf.CeilToInt(gameTimer + Time.timeSinceLevelLoad) % 60;
        minutes = Mathf.CeilToInt(gameTimer + Time.timeSinceLevelLoad) / 60;
        remainingTime = string.Format("{0:00}:{1:00}", minutes, seconds);
        TimersText.GetComponent<Text>().text = remainingTime.ToString();

        if (SpeedDist.Speed < 0.1f)
        {
            Time.timeScale = 0.5f;
            latitudes = Input.location.lastData.latitude;
            longitudes = Input.location.lastData.longitude;
            Current -= 1 * Time.deltaTime;

            if (Current <= 0)
            {
                Current = 0;
                gameOver();
               

            }
        }

        if (SpeedDist.Speed > 0.1f)
        {
            Current = 30f;
            dingTong.Play();
            Time.timeScale = 2.0f;
            latitude = Input.location.lastData.latitude;
            longitude = Input.location.lastData.longitude;
            objectToPlace.SetActive(false);
        }

        if(longitudes != longitude || latitudes != latitude)
        {
            objectToPlace.SetActive(true);
        }
        else
        {
            objectToPlace.SetActive(false);
        }
        
    }
  

    public void PlaceObject()
    {
        Instantiate(objectToPlace, placementPose.position, placementPose.rotation);
    }

    private void UpdatePlacementIndicator()
    {
        if (placementPoseIsValid)
        {
            placementIndicator.SetActive(true);
            placementIndicator.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
        }
        else
        {
            placementIndicator.SetActive(false);
        }
    }

    private void UpdatePlacementPose()
    {
        var screenCenter = Camera.current.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        var hits = new List<ARRaycastHit>();
        arOrigin.Raycast(screenCenter, hits, TrackableType.Planes);

        placementPoseIsValid = hits.Count > 0;
        if (placementPoseIsValid)
        {
            placementPose = hits[0].pose;

            var cameraForward = Camera.current.transform.forward;
            var cameraBearing = new Vector3(cameraForward.x, 0, cameraForward.z).normalized;
            placementPose.rotation = Quaternion.LookRotation(cameraBearing);
        }

    }
    public void gameOver()
    {
        Chartboost.cacheInterstitial(CBLocation.GameOver);
        Chartboost.showInterstitial(CBLocation.GameOver);
        Time.timeScale = 0;
        Canvas.SetActive(true);
        Value = PlayerPrefs.GetInt("Value");
        PlayerPrefs.GetInt("value", Score.scoreValue);
        PlayerPrefs.SetInt("value", Score.scoreValue);
        HighScore.text = "HighScore: " + Score.scoreValue;
        GameObject.FindGameObjectWithTag("Inactive").SetActive(false);


        if (Score.scoreValue > Value)
        {
            PlayerPrefs.SetInt("value", Score.scoreValue);
            PlayerPrefs.SetInt("Value", Score.scoreValue);

            if (PlayerPrefs.GetInt("value", Score.scoreValue) == PlayerPrefs.GetInt("Value", Score.scoreValue))
            {
                NewRecord.SetActive(true);
            }
        }
        else
        {
            NewRecord.SetActive(false);
        }
    }
}
