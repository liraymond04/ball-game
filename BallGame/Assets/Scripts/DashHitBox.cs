using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using EZCameraShake;

public class DashHitBox : MonoBehaviour 
{
    
    public GameObject playerInput;

    public GameObject dashHitbox;
    public float time;
    public float timeStart;

    //Rigidbody2D rb;

    private Rewired.Player rplayerInput;

	// Use this for initialization
	void Start () 
    {
        time = timeStart;
        //rb = playerInput.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        rplayerInput = ReInput.players.GetPlayer(playerInput.GetComponent<PlayerInput1>().gameID);

        if(time > 0)
        {
            time -= Time.deltaTime;
            dashHitbox.GetComponent<BoxCollider2D>().enabled = true;
        }
        if(time <= 0)
        {
            if (rplayerInput.GetButtonDown("Fire 1"))
            {
                //Debug.Log("Dash");
                time = timeStart;
            }

            dashHitbox.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Red" || collision.tag == "Green" || collision.tag == "Yellow")
        {
            CameraShaker.Instance.ShakeOnce(4f, 4f, 0.1f, 1f);
        }
    }
}
