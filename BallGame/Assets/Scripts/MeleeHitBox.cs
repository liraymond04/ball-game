using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class MeleeHitBox : MonoBehaviour 
{

    public GameObject player;
    public GameObject playerInput;

    public GameObject meleeHitbox;
    public float time;
    public float timeStart;

    private float initRotate;

    private bool attacking = false;

    private Animator anim;

    private Rewired.Player rplayerInput;

    // Use this for initialization
    void Awake()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    void Start () 
    {
        time = timeStart;
        initRotate = transform.rotation.y;
    }
	
	// Update is called once per frame
	void Update () 
    {
        rplayerInput = ReInput.players.GetPlayer(playerInput.GetComponent<PlayerInput3>().gameID);

        if (player.transform.rotation.y != initRotate)
        {
            Vector3 pos = transform.position;
            pos.z *= -1;
            transform.position = pos;

            initRotate = transform.rotation.y;
        }

        if (rplayerInput.GetButtonDown("Fire 1"))
        {
            time = timeStart;
            attacking = true;
            Debug.Log(attacking);

            meleeHitbox.GetComponent<BoxCollider2D>().enabled = true;
        }

        if(attacking)
        {
            if (time > 0)
            {
                time -= Time.deltaTime;
            }
            if (time <= 0)
            {
                attacking = false;
                meleeHitbox.GetComponent<BoxCollider2D>().enabled = false;
            }
        }

        anim.SetBool("Attacking", attacking);
    }
}
