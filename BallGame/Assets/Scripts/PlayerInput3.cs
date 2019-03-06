using UnityEngine;
using System.Collections;
using Rewired;

[RequireComponent (typeof (Player3))]
public class PlayerInput3 : MonoBehaviour
{

	Player3 player;

    public GameObject global;

    private Rewired.Player playerInput;

    public int gameID;

    void Start () 
    {
        global = GameObject.FindGameObjectWithTag("Player Inputs");
        player = GetComponent<Player3>();
    }

	void Update () 
    {
        gameID = global.GetComponent<GlobalControl>().yellow;
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
