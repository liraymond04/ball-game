using UnityEngine;
using System.Collections;
using Rewired;

[RequireComponent (typeof (Player1))]
public class PlayerInput1 : MonoBehaviour
{

	Player1 player;

    public GameObject global;

    private Rewired.Player playerInput;

    public int gameID;

    void Start () 
    {
        global = GameObject.FindGameObjectWithTag("Player Inputs");
        player = GetComponent<Player1>();
    }

	void Update () 
    {
        gameID = global.GetComponent<GlobalControl>().blue;
        playerInput = ReInput.players.GetPlayer(gameID);

        Vector2 directionalInput = new Vector2 (playerInput.GetAxisRaw ("Move Horizontal"), playerInput.GetAxisRaw ("Move Vertical"));
		player.SetDirectionalInput (directionalInput);

        if (playerInput.GetButtonDown ("Jump")) 
        {
			player.OnJumpInputDown ();
		}
        if (playerInput.GetButtonUp ("Jump"))
        {
			player.OnJumpInputUp ();
		}
    }

}
