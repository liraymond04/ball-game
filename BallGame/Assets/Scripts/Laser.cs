using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour 
{

    public float speed = 20f;
    public Rigidbody2D rb;

	// Use this for initialization
	void Start() 
    {
        rb.velocity = transform.right * speed;
	}

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.gameObject.tag == "Blue" || hitInfo.gameObject.tag == "Green" || hitInfo.gameObject.tag == "Yellow" || hitInfo.gameObject.tag == "Bounds")
        {
            Debug.Log(hitInfo.name);

            Destroy(gameObject);
        }
    }

}
