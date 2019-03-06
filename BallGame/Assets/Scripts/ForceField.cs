using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class ForceField : MonoBehaviour 
{

    private GameObject gPlayer;

    //Player2 player;
    PlayerInput2 playerInput;

    public float speed = 20f;
    public float newSpeed;
    public Rigidbody2D rb;

    public float goTime = 0;
    public float maxTime;
    public float speedPercent;
    public float speedMax;

    private bool hitSpeed = false;
    private Rewired.Player rplayerInput;

	// Use this for initialization
	void Start() 
    {
        gPlayer = GameObject.FindGameObjectWithTag("Green");
        //player = gPlayer.GetComponent<Player2>();
        playerInput = gPlayer.GetComponent<PlayerInput2>();
        rb.velocity = transform.right * speed;
	}

    void Update()
    {
        rplayerInput = ReInput.players.GetPlayer(playerInput.gameID);

        ProcessInput();

        if(hitSpeed)
        {
            rb.velocity = transform.right * newSpeed;
        }

        if(rb.velocity.x > speedMax)
        {
            rb.velocity = new Vector2(speedMax,0);
        }
        if (rb.velocity.x < -speedMax)
        {
            rb.velocity = new Vector2(-speedMax, 0);
        }
    }

    void ProcessInput()
    {
        if(rplayerInput.GetAxisRaw("Fire 1") > 0.5f)
        {
            goTime += Time.deltaTime;
            speedPercent = goTime / maxTime;
            Debug.Log(speedPercent);

            if(goTime >= maxTime)
            {
                rb.velocity = Vector2.zero;
            }
        }
        if (rplayerInput.GetButtonUp("Fire 1"))
        {
            hitSpeed = true;
            newSpeed *= speedPercent;
        }
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.gameObject.tag == "Bounds")
        {
            Debug.Log(hitInfo.name);

            Destroy(gameObject);
        }
    }

}
