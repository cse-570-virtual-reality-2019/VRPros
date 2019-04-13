using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.IO;
using UnityEngine.SceneManagement;

public class GetLongLatEvent : MonoBehaviour
{
    public InputField longText;
    public InputField latText;

    public Text errorText;

    public GameObject background;

    private string longitude, latitude;
    private string URL;

    void Start() {
        background.SetActive(false);
    }

    public void getLongAndLat() {
        longitude = longText.text;
        latitude = latText.text;

        if (longitude.Length < 5 || latitude.Length < 5)
        {
            errorText.text = "Please Enter a valid value!!!";
            return;
        }
        else {
            MapLocationHolder.LONGITUDE = longitude;
            MapLocationHolder.LATITUDE = latitude;

            MapLocationHolder holder = new MapLocationHolder();
            holder.calLongLat();
            URL = "https://api.openstreetmap.org/api/0.6/map?bbox="+holder.getminlongitde()+","+holder.getminimumlatitude()+","+holder.getmaxlongitude()+","+holder.getmaxlatitude();
            MapLocationHolder.FILENAME = holder.getminimumlatitude() + holder.getmaxlatitude();

            StartCoroutine(StartDownload());
        }

    }

    IEnumerator StartDownload()
    {
        string url = URL;
        background.SetActive(true);
        string fileSavePath = Path.Combine(Application.dataPath, "Resources");
       fileSavePath = Path.Combine(fileSavePath,MapLocationHolder.FILENAME);

        //Create Directory if it does not exist
        if (!Directory.Exists(Path.GetDirectoryName(fileSavePath)))
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fileSavePath));
        }

        var uwr = new UnityWebRequest(url);
        uwr.method = UnityWebRequest.kHttpVerbGET;
        var dh = new DownloadHandlerFile(fileSavePath);
        dh.removeFileOnAbort = true;
        uwr.downloadHandler = dh;
        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError || uwr.isHttpError)
            Debug.Log(uwr.error);
        else
            Debug.Log("Download saved to: " + fileSavePath.Replace("/", "\\") + "\r\n" + uwr.error);

        SceneManager.LoadScene("MainScene", LoadSceneMode.Single);
    }
}
