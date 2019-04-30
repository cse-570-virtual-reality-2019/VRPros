using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LoadPreviousScene : MonoBehaviour
{
    public string SceneName;
    public Material daySkybox;
    public Material nightSkyBox;
    public Material rainSkyBox;
    public bool isRaining;
    public GameObject rain;
    public GameObject cloud;
    // Start is called before the first frame upda
    public void LoadScene() {
        SceneManager.LoadScene(SceneName, LoadSceneMode.Single);
    }

    public void LoadDaySkyBox()
    {
        RenderSettings.skybox = daySkybox;
        cloud.SetActive(true);
    }

    public void LoadNightSkyBox()
    {
        RenderSettings.skybox = nightSkyBox;
        cloud.SetActive(false);
    }

    public void toggleRaining()
    {
        rain.SetActive(isRaining);
        if (isRaining)
        {
            RenderSettings.skybox = rainSkyBox;
            //cloud.SetActive(true);
            cloud.SetActive(false);
        }
        else
        {
           
            LoadDaySkyBox();
        }
        isRaining = !isRaining;
    }
}
