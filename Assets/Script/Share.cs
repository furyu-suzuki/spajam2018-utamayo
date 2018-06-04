using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class Share : MonoBehaviour {

	bool isRequestNow = false;

	public void Tweet(string text, string url = "", string texture_path = ""){
		SocialConnector.SocialConnector.Share(text, url, texture_path );
	}

	public void TestExecute()
	{
		string url = "https://maps.googleapis.com/maps/api/place/photo?maxwidth=400&photoreference=CnRvAAAAwMpdHeWlXl-lH0vp7lez4znKPIWSWvgvZFISdKx45AwJVP1Qp37YOrH7sqHMJ8C-vBDC546decipPHchJhHZL94RcTUfPa1jWzo-rSHaTlbNtjh-N68RkcToUCuY9v2HNpo5mziqkir37WU8FJEqVBIQ4k938TI3e7bf8xq-uwDZcxoUbO_ZJzPxremiQurAYzCTwRhE_V0&key=dummy";

		Tweet ("ここまで来たよ！", url);
	}
}