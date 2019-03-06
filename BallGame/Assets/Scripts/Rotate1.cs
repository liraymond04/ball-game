using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class Rotate1 : MonoBehaviour 
{
    public GameObject pInput;
    private PlayerInput3 playerInput;

    public bool facingRight = true;

    private Rewired.Player rplayerInput;

	// Use this for initialization
	void Start () 
    {
        playerInput = pInput.GetComponent<PlayerInput3>();
    }
	
	// Update is called once per frame
	void Update () 
    {
        rplayerInput = ReInput.players.GetPlayer(playerInput.gameID);

        if (rplayerInput.GetAxisRaw("Move Horizontal") > 0.5f || rplayerInput.GetAxisRaw("Move Horizontal") < -0.5f)
        {
            if (rplayerInput.GetAxisRaw("Move Horizontal") > 0.5f && !facingRight)
            {
                Flip();
                facingRight = true;
            }
            else if (rplayerInput.GetAxisRaw("Move Horizontal") < -0.5f && facingRight)
            {
                Flip();
                facingRight = false;
            }
        }
    }

    void Flip()
    {
        facingRight = !facingRight;

        transform.Rotate(0f, 180f, 0f);
    }

}
