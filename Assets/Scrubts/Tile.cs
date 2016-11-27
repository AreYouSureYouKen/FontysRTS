using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {
    Renderer rend;
    public bool Moveable=false;
    public bool occupied = false;
    private Color lastColor;
	// Use this for initialization
	void Start () {
        rend = GetComponent<Renderer>();
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnClick() {
		Debug.Log ("Godverdomme"+name);
        
    }

    void OnMouseOver()
    {
        if (Moveable)
        {
            rend.material.color = Color.cyan;
        }
    }

    void OnMouseExit()
    {
        if (Moveable)
        {
            rend.material.color = lastColor;
        }
    }

    public void occupy()
    {
        this.occupied = true;
    }

    public void deoccupy()
    {
        this.occupied = false;
    }

    public void Activate()
    {
        rend.material.color = Color.green;
        lastColor = Color.green;
        Moveable = true;
    }

    public void PrepareYourAnus()
    {
        rend.material.color = Color.red;
        lastColor = Color.red;
        Moveable = true;
    }

    public void Disactivate()
    {
        rend.material.color = Color.white;
        lastColor = Color.white;
        Moveable = false;
    }

    public void Fog()
    {
        rend.material.color = Color.grey;
        lastColor = Color.grey;
    }

    public void InVision()
    {
        rend.material.color = Color.white;
        lastColor = Color.white;
    }
}
