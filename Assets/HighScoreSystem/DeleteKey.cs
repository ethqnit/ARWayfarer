using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DeleteKey : MonoBehaviour
{
    public void DelKey()
    {
        {
            Score.scoreValue = 0;
        }
    }
     public void DelKey2()
   {
    Application.Quit();
   }
    public void DelAll()
    {
        PlayerPrefs.DeleteAll();
    }
}
