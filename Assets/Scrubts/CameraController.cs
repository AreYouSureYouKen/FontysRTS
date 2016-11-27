using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	private float scrolly = 0;
	private float scrollx = 0;

    public float scrollspeed_y = 3;
    public float scrollspeed_x = 3;

    public float offsetY;
    public float offsetZ;
	// Use this for initialization
	void Start () {
	
	}

    public void setPlayer(GameObject player)
    {
        this.transform.rotation = player.transform.rotation;
        this.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + offsetY, player.transform.position.z);
        this.transform.position -= transform.forward * offsetZ;
    }
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.W) || Input.GetKey (KeyCode.UpArrow)) {
			if (scrolly != 0.04f)
				if(scrolly < 0)
					scrolly = 0;
				else
				scrolly += 0.01f;
            this.transform.position += transform.forward * (scrolly * scrollspeed_y);
		} else if (Input.GetKey (KeyCode.S) || Input.GetKey (KeyCode.DownArrow)) {
			if (scrolly != -0.04f)
				if(scrolly > 0)
					scrolly = 0;
				else
				scrolly -= 0.01f;
            this.transform.position += transform.forward * (scrolly * scrollspeed_y);
		}  else {
			scrolly = 0;
		}

		if (Input.GetKey (KeyCode.A) || Input.GetKey (KeyCode.LeftArrow)) {
			if (scrollx != -0.04f)
				if(scrollx > 0)
					scrollx = 0;
				else
				scrollx -= 0.01f;
            this.transform.position += transform.right * (scrollx * scrollspeed_x);
		} else if (Input.GetKey (KeyCode.D) || Input.GetKey (KeyCode.RightArrow)) {
			if (scrollx != 0.04f)
				if(scrollx < 0)
					scrollx = 0;
				else
				scrollx += 0.01f;
            this.transform.position += transform.right * (scrollx * scrollspeed_x);
		} else {
			scrollx = 0;
		}

	}
}
