using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class Energy : MonoBehaviour 
{

    public float speed = 10f;
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

        if (hitInfo.tag == "Blue" || hitInfo.tag == "Green" || hitInfo.tag == "Yellow")
        {
            CameraShaker.Instance.ShakeOnce(4f, 4f, 0.1f, 1f);
        }
    }

}
