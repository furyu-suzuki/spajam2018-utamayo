using System;

public class Location {
	public float lat { get; set; }
	public float lon { get; set; }

	const float latitudeDiff = 0.004060f;
	const float longitudeDiff = 0.005360f;

	public Location (float latitude, float longitude)	{
		lat = latitude;
		lon = longitude;
	}
}

