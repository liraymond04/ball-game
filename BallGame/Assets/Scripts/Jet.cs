using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class Jet : MonoBehaviour 
{
    public GameObject pInput;
    private PlayerInput1 playerInput;

    public GameObject blue;

    private Rigidbody2D rb;
    public  float jetSpeed;
    public float jetTime;
    public float startJetTime;
    public float trailEndTime;
    public float trailTime;

    private Rewired.Player rplayerInput;

	// Use this for initialization
	void Start () 
    {
        playerInput = pInput.GetComponent<PlayerInput1>();

        rb = blue.GetComponent<Rigidbody2D>();

        jetTime = startJetTime;
    }
	
	// Update is called once per frame
	void Update () 
    {
        //var player = gameObject.GetComponent<Player1>();
        rplayerInput = ReInput.players.GetPlayer(playerInput.gameID);

        if (rplayerInput.GetButtonDown("Fire 2") && (jetTime <= 0))
        {
            GetComponent<TrailRenderer>().enabled = true;
            trailTime = trailEndTime;

            rb.velocity = Vector2.up * jetSpeed;

            jetTime = startJetTime;
        }

        if(jetTime > 0)
        {
            jetTime -= Time.deltaTime;
        }

        if (trailTime > 0)
        {
            trailTime -= Time.deltaTime;
        }
        if(trailTime <= 0)
        {
            GetComponent<TrailRenderer>().enabled = false;
        }
	}
}
