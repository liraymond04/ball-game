using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charge : MonoBehaviour 
{

    public float attack = 0;
    public float attackMultiplier;
    public float attackMax;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update ()
    {
        attack += Time.deltaTime;

        if(Input.GetButtonUp("Fire2"))
        {
            Destroy(gameObject);
        }

        if(attack <= attackMax)
        {
            transform.localScale = new Vector3(attack * attackMultiplier, attack * attackMultiplier, attack * attackMultiplier);
        }
	}

}
