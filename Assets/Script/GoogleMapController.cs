using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;

public class GoogleMapController : MonoBehaviour {

	public string key = null;
	private float lat = 38;
	private float lon = 138;
	private string jsonString = null;
	public Text totalMoovment;
	public Text area;
	Location location;
	public string photoRequestUrl;
	bool isRequestNow = false;
	//public Text debug;
	
	void Start(){
		// GPS情報がなければ、初期値で初期化する
		location = new Location (lat, lon);
		Build ();

		// place 
		var url = string.Format("https://maps.googleapis.com/maps/api/place/nearbysearch/json?location=43.068298,141.350022&radius=1000&sensor=false&language=ja&key={0}&locat", key);
		StartCoroutine(Place(url, (text)=>{
			isRequestNow = false;
			setJsonString(text);
			SetCurrentPoint();
		}));

		// var url = string.Format("https://maps.googleapis.com/maps/api/place/nearbysearch/json?location=34.984857,135.759938&radius=1000&sensor=false&language=ja&key={0}&locat",key);
		// StartCoroutine(Place(url, (text)=>Debug.Log("->"+text)));

	}

	public Location Geo {
		get { return location; }
		set {
			location = value;
			//Build ();
		}
	}

	public void SetTotalMoovment(float tootalMoovment) {
		this.totalMoovment.text = string.Format("{0:0.000}km",tootalMoovment);
	}
	
	public string GetJsonString() {
		return jsonString;
	}

	public void SetText (string text) {
		//this.totalMoovment.text = string.Format("{0}\n{1}",this.totalMoovment.text,text);
	}

	public void SetGeo(Location geo){
		Geo = geo;
	}

	public void Draw (Location gps) {
		location = gps;
		//Build ();
	}

	private void SetCurrentPoint () {
		var jsonParser = new JsonParser();
		jsonParser.Set(jsonString);
		var area = jsonParser.GetPlaceInfo();
		if (area.results.Count != 0){
			this.area.text = area.results[0].name;
			this.photoRequestUrl = jsonParser.photoRequestUrl;
		} else {
			this.area.text = "海";
			this.photoRequestUrl = "";
		}
	}

	public void Build(){
		var url = string.Format (@"https://maps.googleapis.com/maps/api/staticmap?size=500x500&maptype=roadmap&center={0},{1}&zoom=4&scale=4&language=jp&style=element:labels|visibility:on&sensor=true", location.lat, location.lon);

		if (key != null && key.Length != 0) {
			url += "&key=" + key;
		}
		StartCoroutine(Download(url, tex => {
			isRequestNow = false;
			addSplatPrototype(tex);
		}));
	}

	public void UpdatePlace(Location location){
		// place 
		//debug.text = string.Format("{0}\n{1}",debug.text, location.lat);
		var url = string.Format("https://maps.googleapis.com/maps/api/place/nearbysearch/json?location={0},{1}&radius=1000&sensor=false&language=ja&key={2}&locat",location.lat, location.lon, key);
		StartCoroutine(Place(url, (text)=>{
			isRequestNow = false;
			setJsonString(text);
			SetCurrentPoint();
		}));
	}

	/// 
	/// GoogleMapsAPIから地図画像をダウンロードする
	/// 
	/// ダウンロード元
	/// ダウンロード後に実行されるコールバック関数
	IEnumerator Download(string url, Action<Texture2D> callback) {

		while(isRequestNow) {
			yield return null;
		}
		isRequestNow = true;

		//debug.text = string.Format("{0}\n{1}",debug.text,"呼ばれた！");
		var www = new WWW(url);
		yield return www; // Wait for download to complete

		callback(www.texture);
	}

	/// ダウンロード元
	/// ダウンロード後に実行されるコールバック関数
	IEnumerator Place(string url, Action<String> onFinish) {
		while(isRequestNow) {
			yield return null;
		}
		isRequestNow = true;

		//debug.text = string.Format("{0}\n{1}",debug.text,"呼ばれた！");
		var www = new WWW(url);
		yield return www; // Wait for download to complete

		onFinish(www.text);
	}

	private void setJsonString(String text){
		jsonString = text;
	}

	/// 
	/// Planeにテクスチャを貼り付ける
	/// 

	/// 
	public void addSplatPrototype(Texture2D tex) {
		GetComponent<Renderer>().material.mainTexture = tex;
	}
}