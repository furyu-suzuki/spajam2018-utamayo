using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlaceInfo {
    public List<string> html_attributions;
    public string next_page_tokenpublic;
    public List<Spot> results;
    public string status;
}

[System.Serializable]
public class Spot {
    public Geometry geometory;
    public string icon;
    public string id;
    public string name;
    public List<Photo> photos;
    public string place_id;
    public string reference;
    public string scope;
    public List<string> types;
    public string vicinity;
}

[System.Serializable]
public class Geometry {
	public Locate location;
	public ViewPort viewport;
}

[System.Serializable]
public class Locate {
	public float lat;
	public float lng;
}

[System.Serializable]
public class ViewPort {
	public Locate northeast;
	public Locate southwest;
}

[System.Serializable]
public class Photo {
   public int height;
   public List<string> html_attributions;
   public string photo_reference;
   public int width;
}

public class JsonParser : MonoBehaviour {

    public string key = null;
	public GameObject obj;
    public string imageUrl;
    public string photoRequestUrl;
    bool updated = false;
    public PlaceInfo placeInfo;

    void Update(){
        // while( updated == false ){
        //     GoogleMapController googleMapController = obj.GetComponent<GoogleMapController>();
    	//     string jsonString = googleMapController.GetJsonString();
        // 	placeInfo = JsonUtility.FromJson<PlaceInfo>(jsonString);

        //     string photo_reference = placeInfo.results[0].photos[0].photo_reference;

        //     //photoAPI
        //     photoRequestUrl = string.Format(
        //     "https://maps.googleapis.com/maps/api/place/photo?maxwidth=400&photoreference={0}&key=dummy", photo_reference);
        //     Debug.Log(photoRequestUrl);

        //     updated = true;
        // }
	}

    public void Set(string jsonString) {
        // GoogleMapController googleMapController = aaa.GetComponent<GoogleMapController>();
        // string jsonString = googleMapController.GetJsonString();
        placeInfo = JsonUtility.FromJson<PlaceInfo>(jsonString);
        if (placeInfo.results.Count == 0 || placeInfo.results[0].photos.Count == 0){
            return;
        }
        string photo_reference = placeInfo.results[0].photos[0].photo_reference;

        //photoAPI
        photoRequestUrl = string.Format(
			"https://maps.googleapis.com/maps/api/place/photo?maxwidth=400&photoreference={0}&key=dummy", photo_reference);
        Debug.Log(photoRequestUrl);

        updated = true;
    }

    public PlaceInfo GetPlaceInfo() {
        return placeInfo;
    }
}

