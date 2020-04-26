using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Back : MonoBehaviour
{
    public string SceneToLoad = "";
    public float _delay = 5f;
    public bool LoadAtStart = false;

    public void Start()
    {
        Time.timeScale = 1.0f;
        if (LoadAtStart == true)
        {
            Load_Scene_WithTiming();
        }
    }


    public IEnumerator LoadWithTimingCoroutine()
    {
        yield return new WaitForSeconds(_delay);
        SceneManager.LoadScene(SceneToLoad);
    }


    public void Load_Scene()
    {
        SceneManager.LoadScene(SceneToLoad);
    }


    public void Load_Scene_WithTiming()
    {
        StartCoroutine(LoadWithTimingCoroutine());
    }

}