using UnityEngine;
using System.Collections;

public class Shield : MonoBehaviour
{

    public GameObject getPlayer;
    public GameObject shield;

    void Update()
    {
        shield.transform.position = new Vector3(getPlayer.transform.position.x, getPlayer.transform.position.y, getPlayer.transform.position.z - 1);
    }
}