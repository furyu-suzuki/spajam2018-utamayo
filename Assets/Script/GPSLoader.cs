using UnityEngine;
using System.Collections;

public class GPSLoader : MonoBehaviour {
	public GoogleMapController drawer;

	public CharaLocation charaLocation;

	private float intervalTime = 0.0f;

	private Location beforeLocation;

	private float totalMoovment;

	IEnumerator Start() {
		// もし前回の緯度情報があればそれを取得する
		//this.beforeLocation = FindBeforeLocation();
		//drawer.SetText("前のLocationある？");
		//if (this.beforeLocation == null) {
			//drawer.SetText("ないから取得するよ");
			float latitude = 0;
			float longitude = 0;
			while(latitude == 0) {
				latitude = Input.location.lastData.latitude;
				longitude = Input.location.lastData.longitude;
				yield return new WaitForSeconds(1);		
			}
			this.beforeLocation = new Location (latitude, longitude);
		//}
		//drawer.SetText("->"+this.beforeLocation.lat);

		//yield return UpdateLocation ();
		yield return null;
	}

	void Update(){
		//毎フレーム読んでると処理が重くなるので、3秒毎に更新
		intervalTime += Time.deltaTime;
		if (intervalTime >= 3.0f) {
			//StartCoroutine (UpdateLocation());
			StartCoroutine(NonGPSUpdateLocation());
			intervalTime = 0.0f;
		}
	}

	private void AddTotalMovement (Location beforeLocation, Location nowLocation) {
		var distanceLat = Mathf.Pow(beforeLocation.lat - nowLocation.lat, 2);
		var distanceLon = Mathf.Pow(beforeLocation.lon - nowLocation.lon, 2);
		var distance = Mathf.Sqrt(distanceLat+distanceLon);
		this.totalMoovment += distance;
		drawer.SetTotalMoovment(this.totalMoovment/0.009f);

		charaLocation.AddMovement (distance);
		drawer.UpdatePlace(charaLocation.mCurrent);
		//yield return null;
	}

	private void SaveBeforeLocation (Location location) {
		PlayerPrefs.SetFloat("Lat", location.lat);
        PlayerPrefs.SetFloat("Lon", location.lon);
        PlayerPrefs.Save();
	}

	private Location FindBeforeLocation () {
		var lat = PlayerPrefs.GetFloat("Lat");
		var lon = PlayerPrefs.GetFloat("Lon");
		if (lat != 0 && lon != 0) {
			return new Location(lat, lon);
		} else {
			return null;
		}
	}

	private IEnumerator UpdateLocation(){
		if (!Input.location.isEnabledByUser) {
			yield break;
		}

		Input.location.Start();

		int maxWait = 20;
		while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0) {
			yield return new WaitForSeconds(1);
			maxWait--;
		}

		if (maxWait < 1) {
			#if DEBUG
			print("Timed out");
			#endif

			yield break;
		}

		if (Input.location.status == LocationServiceStatus.Failed) {
			#if DEBUG
			print("Unable to determine device location");
			#endif

			yield break;

		} else {

			float latitude = Input.location.lastData.latitude;
			float longitude = Input.location.lastData.longitude;
			var newLocation = new Location (latitude, longitude);

			// #if DEBUG
			// print("Location: " + latitude + " " + longitude + " " + Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy + " " + Input.location.lastData.timestamp);
			// #endif

			if (drawer != null) {
				AddTotalMovement(this.beforeLocation, newLocation);
				this.beforeLocation = newLocation;
				drawer.Geo = newLocation;
				//SaveBeforeLocation(newLocation);
			}
		}

		// Stop service if there is no need to query location updates continuously
		Input.location.Stop();
	}

	IEnumerator NonGPSUpdateLocation(){
		if (beforeLocation == null) {
			this.beforeLocation = new Location (34.98486f, 135.7599f);
		}
		var newLocation = new Location (beforeLocation.lat + 0.2f, beforeLocation.lon + 0.2f);

		AddTotalMovement(this.beforeLocation, newLocation);

		this.beforeLocation = newLocation;
		drawer.Geo = newLocation;
		yield return null;		
	}
}