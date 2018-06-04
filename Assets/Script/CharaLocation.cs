using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaLocation : MonoBehaviour {

	public Location mCurrent;
	public Location mStart;
	public Location mEnd;

	public CalcPosition mCalcPosition;

	public GameObject mChara;
	public GameObject mStartPoint;
	public GameObject mEndPoint;
	public GameObject mLine;

	// Use this for initialization
	void Start () {

		//	開始・札幌
		mCurrent = new Location (43.068298f, 141.350022f);
		mStart = new Location (43.068298f, 141.350022f);

		//	終点・東京
		mEnd = new Location (35.681004f, 139.766892f);

		//	キャラの初期位置を設定
		UpdateCharaPosition();
	
		//	ポイントの初期位置を設定
		Vector3 startPos = mCalcPosition.CalcPosFromLocationToWorld( mStart );
		Vector3 endPos = mCalcPosition.CalcPosFromLocationToWorld( mEnd );
		startPos.y = 0.2f;
		endPos.y = 0.2f;

		mStartPoint.transform.position = startPos;
		mEndPoint.transform.position = endPos;

		//	経路ラインの位置設定
		Vector3 dir = endPos - startPos;

		mLine.transform.position = startPos;
		mLine.transform.rotation = Quaternion.Euler ( -90.0f, 0.0f, Mathf.Atan2 (dir.x, dir.z) * Mathf.Rad2Deg + 180.0f );
		mLine.transform.localScale = new Vector3( 1.0f, dir.magnitude, 1.0f );
	}
		
	// Update is called once per frame
	void Update () {

		//仮
		//AddMovement (0.005f);

	}

	public void AddMovement(float movement ){

		Vector2 dir = new Vector2( mEnd.lon - mStart.lon , mEnd.lat - mStart.lat );
		dir.Normalize ();
		dir *= movement;

		mCurrent.lon += dir.x; 
		mCurrent.lat += dir.y;

		//	キャラの座標更新
		UpdateCharaPosition();
	}

	void UpdateCharaPosition(){

		Vector3 startPos = mCalcPosition.CalcPosFromLocationToWorld( mStart );
		Vector3 endPos = mCalcPosition.CalcPosFromLocationToWorld( mEnd );
		Vector3 curPos = mCalcPosition.CalcPosFromLocationToWorld( mCurrent );
		curPos.y = 0.2f;

		Vector3 dir = endPos - startPos;

		mChara.transform.position = curPos;
		mChara.transform.rotation = Quaternion.Euler ( 0.0f, Mathf.Atan2 (dir.x, dir.z) * Mathf.Rad2Deg, 0.0f );
	}
}
