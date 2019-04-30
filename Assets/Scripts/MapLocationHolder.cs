using System.Collections;
using System.Collections.Generic;
using System.Globalization;

public class MapLocationHolder 
{
    public static string LONGITUDE;
    public static string LATITUDE;

    public static string FILENAME;

    private string maxLat;
    private string minLat;

    private string minLong;
    private string maxLong;

    public float latdiff = 0.010f;

   public float longdiff = 0.020f;
    public void calLongLat() {
        float minlong, minlat, maxlong, maxlat, lon, lat;
        lon = float.Parse(LONGITUDE, CultureInfo.InvariantCulture.NumberFormat);
        lat = float.Parse(LATITUDE, CultureInfo.InvariantCulture.NumberFormat);

        minlat = lat - latdiff;
        maxlat = lat + latdiff;
        minLat = "" + minlat;
        maxLat = "" + maxlat;

        minlong = lon - longdiff;
        maxlong = lon + longdiff;

        minLong = "" + minlong;
        maxLong = "" + maxlong;


    }

    public string getminimumlatitude() {
        return minLat;
    }

    public string getminlongitde() {
        return minLong;
    }

    public string getmaxlatitude() {
        return maxLat;
    }

    public string getmaxlongitude() {
        return maxLong;
    }

}
