using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Rewired;

[RequireComponent (typeof (Player))]
public class PlayerInput : MonoBehaviour 
{

	Player player;

    private GameObject global;

    public float knockbackLaser;
    public float knockbackEnergy;

    Rigidbody2D rigidbody2D;

    public float attack;
    public float attackMax;

    public int gameID;

    private Rewired.Player playerInput;

	void Start () 
    {
        global = GameObject.FindGameObjectWithTag("Player Inputs");
        player = GetComponent<Player>();
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

	void Update () 
    {
        gameID = global.GetComponent<GlobalControl>().red;
        playerInput = ReInput.players.GetPlayer(gameID);

        Vector2 directionalInput = new Vector2 (playerInput.GetAxisRaw ("Move Horizontal"), playerInput.GetAxisRaw ("Move Vertical"));
		player.SetDirectionalInput (directionalInput);

        var script = GetComponent<Player>();

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
                    rigidbody2D.velocity = new Vector2(-knockbackLaser, 0);
                }
                else
                {
                    rigidbody2D.velocity = new Vector2(knockbackLaser, 0);
                }
            }
        }
        if(playerInput.GetButton("Fire 2"))
        {
            attack += Time.deltaTime;
        }
        if (playerInput.GetButtonUp("Fire 2") && (attack >= attackMax))
        {
            Debug.Log(player.timeLeft + " " + player.timeTotal + " " + player.hitTime);

            if (player.timeLeft <= player.timeTotal && player.hitTime == false)
            {
                if (script.facingRight == true)
                {
                    rigidbody2D.velocity = new Vector2(-knockbackEnergy, 0);
                }
                else
                {
                    rigidbody2D.velocity = new Vector2(knockbackEnergy, 0);
                }

                attack = 0;
            }
        }
        if(playerInput.GetButtonUp("Fire 2") && (attack < attackMax))
        {
            attack = 0;
        }
    }
}
