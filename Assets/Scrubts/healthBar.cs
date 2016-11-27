using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class healthBar : MonoBehaviour {
    float currentHealth=0;
    float targetHealth=0;
    float maxHealth=0;
    float healthUpgrade = 0;
    public Text healthText;
    PlayerManager player;
	// Use this for initialization
	void Start () {
        this.GetComponent<Renderer>().material.color = Color.green;
	}
	
	// Update is called once per frame
	void Update () {
        if ((targetHealth+healthUpgrade) != 0)
        {
            if ((currentHealth + healthUpgrade) < (targetHealth + healthUpgrade)) { currentHealth += 0.01f; this.transform.localScale = new Vector3(((currentHealth + healthUpgrade) / (maxHealth + healthUpgrade) * 0.1f), 0.1f, 0.1f); healthText.text = Mathf.Round(currentHealth + healthUpgrade) + "/" + (maxHealth + healthUpgrade); }
            else if ((currentHealth + healthUpgrade) > (targetHealth + healthUpgrade)) { currentHealth -= 0.01f; this.transform.localScale = new Vector3(((currentHealth + healthUpgrade) / (maxHealth + healthUpgrade) * 0.1f), 0.1f, 0.1f); }
           
        }
	}

    public void SetNewPlayer(Vector3 position)
    {
        transform.parent.LookAt(position);
        healthUpgrade = player.GetHealthUpgrade();
    }

    public void SetTargetHealth(float newTarget)
    {
        this.targetHealth = newTarget;
    }

    public void setMaxHealth(float newMax)
    {
        this.maxHealth = newMax;
        this.targetHealth = newMax;
        Debug.Log("Setting max health to: " + newMax);
    }

    public void setPlayer(PlayerManager pl)
    {
        this.player = pl;
    }

    public void InVision()
    {
        this.transform.parent.gameObject.SetActive(true);
    }

    public void OutofVision()
    {
        this.transform.parent.gameObject.SetActive(false);
    }
}
