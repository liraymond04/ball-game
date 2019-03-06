using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{

    public GameObject player;

    private Dash1 dash;

    // Start is called before the first frame update
    void Start()
    {
        dash = player.GetComponent<Dash1>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Red" || collision.tag == "Blue" || collision.tag == "Green")
        {
            if(dash.trail)
            {
                player.GetComponent<YellowHealth>().GetHealth(5);
            }
        }
    }
}
