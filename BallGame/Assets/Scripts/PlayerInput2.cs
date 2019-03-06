using UnityEngine;
using System.Collections;
using Rewired;

[RequireComponent (typeof (Player2))]
public class PlayerInput2 : MonoBehaviour 
{

	Player2 player;

    public GameObject global;

    public float knockbackLaser;

    //Rigidbody2D rigidbody2D;

    public float attack;
    public float attackMax;

    public int gameID;

    private Rewired.Player playerInput;

	void Start () 
    {
        global = GameObject.FindGameObjectWithTag("Player Inputs");
        player = GetComponent<Player2>();
        //rigidbody2D = GetComponent<Rigidbody2D>();
    }

	void Update () 
    {
        gameID = global.GetComponent<GlobalControl>().green;
        playerInput = ReInput.players.GetPlayer(gameID);

        Vector2 directionalInput = new Vector2 (playerInput.GetAxisRaw ("Move Horizontal"), playerInput.GetAxisRaw ("Move Vertical"));
		player.SetDirectionalInput (directionalInput);

        var script = GetComponent<Player2>();

        if (playerInput.GetButtonDown ("Jump"))
        {
			player.OnJumpInputDown ();
		}
        if (playerInput.GetButtonUp ("Jump"))
        {
			player.OnJumpInputUp ();
		}

        if (playerInput.GetButtonDown("Fire 1"))
        {
            if (player.timeLeft <= player.timeTotal && player.hitTime == false)
            {
                if (script.facingRight == true)
                {
                    //Debug.Log("Right");
                    //rigidbody2D.velocity = new Vector2(-knockbackLaser, 0);
                }
                else
                {
                    //Debug.Log("Left");
                    //rigidbody2D.velocity = new Vector2(knockbackLaser, 0);
                }
            }
        }
    }
}
