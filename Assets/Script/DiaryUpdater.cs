using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class DiaryUpdater : MonoBehaviour {

	bool isRequestNow = false;

	public void UpdateImage()
	{
		GoogleMapController mapConroller = GameObject.Find ("Map").GetComponent<GoogleMapController> ();
		string url = mapConroller.photoRequestUrl;
		if (url == "") {
			return;
		}

		Build (url);
	}

	public void Build( string url ){
		StartCoroutine(Download(url, tex => {
			isRequestNow = false;
			addSplatPrototype(tex);
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

		Debug.Log ( string.Format( "url <{0}>", url));

		var www = new WWW(url);
		yield return www; // Wait for download to complete

		Debug.Log ( string.Format( "OK <{0}>", url));


		callback(www.texture);
	}

	/// 
	public void addSplatPrototype(Texture2D tex) {
		GameObject picture = GameObject.Find ("Picture");
		//picture.GetComponent<RawImage>().material.mainTexture = tex;

		picture.GetComponent<RawImage> ().texture = tex;
	}
}