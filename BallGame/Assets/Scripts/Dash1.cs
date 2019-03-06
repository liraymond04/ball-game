using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class Dash1 : MonoBehaviour 
{
    public PlayerInput3 playerInput;

    private Rigidbody2D rb;
    public float dashSpeed;
    //private float dashTime;
    //public float startDashTime;
    public float trailEndTime;
    public float trailTime;

    public bool trail = false;

    private Rewired.Player rplayerInput;

	// Use this for initialization
	void Start () 
    {
        playerInput = GetComponent<PlayerInput3>();

        rb = GetComponent<Rigidbody2D>();

        //dashTime = startDashTime;
    }
	
	// Update is called once per frame
	void Update () 
    {
        var player = gameObject.GetComponent<Player3>();
        rplayerInput = ReInput.players.GetPlayer(playerInput.gameID);

        if (rplayerInput.GetButtonDown("Fire 2"))
        {
            trail = true;

            if(player.facingRight == true)
            {
                GetComponent<TrailRenderer>().enabled = true;
                //GetComponent<BoxCollider2D>().isTrigger = true;
                trailTime = trailEndTime;

                rb.velocity = Vector2.right * dashSpeed;
            }
            if(player.facingRight == false)
            {
                GetComponent<TrailRenderer>().enabled = true;
                trailTime = trailEndTime;

                rb.velocity = Vector2.left * dashSpeed;
            }
        }

        if(trailTime > 0)
        {
            trailTime -= Time.deltaTime;
        }
        if(trailTime <= 0)
        {
            GetComponent<TrailRenderer>().enabled = false;
            //GetComponent<BoxCollider2D>().isTrigger = false;
            trail =  false;
        }
	}
}
