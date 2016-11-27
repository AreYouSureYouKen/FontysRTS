using UnityEngine;
using System.Collections;

public class CameraLOD : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetAxis("Mouse ScrollWheel") != 0){

			Vector3 scrolling = Vector3.up * (float)Input.GetAxis("Mouse ScrollWheel") * 3 - Vector3.forward * (float)Input.GetAxis("Mouse ScrollWheel") * 3;
			if(this.transform.localPosition.y < 0 && scrolling.y < 0)
			{
				this.transform.localPosition -= scrolling;
			} 
			if(this.transform.localPosition.y > -3 && scrolling.y > 0)//this.transform.localPosition.y > 0 && scrolling.y > 0)
			{
				this.transform.localPosition -= scrolling;
			}

			print(scrolling);
		}
	}
}
