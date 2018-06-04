using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalcPosition : MonoBehaviour {

	const float lon_ratio = 1.1f; //経度
	const float lat_ratio = 1.4f; //緯度

	public Vector3 CalcPosFromLocationToWorld( Location pos )
	{
		Location centor = new Location (38.0f, 138.0f);

		Location delta = new Location (0.0f,0.0f);
		delta.lat = pos.lat - centor.lat;//緯度
		delta.lon = pos.lon - centor.lon;//経度

		Vector3 return_value = new Vector3 ();

		return_value.x = delta.lon * lon_ratio;
		return_value.y = 0.0f;
		return_value.z = delta.lat * lat_ratio;
	
		return return_value;
	}
}
