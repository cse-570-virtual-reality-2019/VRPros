using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.IO;
using UnityEngine.SceneManagement;
using System.Globalization;

public class GetLongLatEvent : MonoBehaviour
{
    public InputField longText;
    public InputField latText;

    public Toggle toggleLatLong;


    public Text errorText;

    public GameObject background;

    private string longitude, latitude;
    private string URL;

    private bool flaglat;

    void Start() {
        background.SetActive(false);
        if (toggleLatLong.isOn) {
            flaglat = true;
        }
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
            MapLocationHolder.FILENAME ="runtimeFile";

            StartCoroutine(StartDownload());
        }

    }

    public void add(float d) {
        string data;
        float lati;
        if (toggleLatLong.isOn)
        {
            data = latText.text;
            if (data.Length == 0)
            {
                data = "0.0";
            }
            lati = float.Parse(data, CultureInfo.InvariantCulture.NumberFormat);

            lati = lati + d;
            data = "" + lati;
            latText.text = data;
        }
        else {
            data = longText.text;
            if (data.Length == 0)
            {
                data = "0.0";
            }
            lati = float.Parse(data, CultureInfo.InvariantCulture.NumberFormat);
            lati = lati + d;
            data = "" + lati;
            longText.text = data;
        }
    }


    IEnumerator StartDownload()
    {
        string url = URL;
        background.SetActive(true);
        string fileSavePath = Path.Combine(Application.dataPath, "Resources");
       fileSavePath = Path.Combine(fileSavePath,MapLocationHolder.FILENAME+".txt");

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
        { Debug.Log(uwr.error);
            errorText.text = "Network Error";
            yield return null;
        }
        else
            Debug.Log("Download saved to: " + fileSavePath.Replace("/", "\\") + "\r\n" + uwr.error);

        SceneManager.LoadScene("MainScene", LoadSceneMode.Single);
    }
}
