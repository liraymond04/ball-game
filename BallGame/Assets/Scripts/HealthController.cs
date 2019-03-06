using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{

    public bool redDead = false;
    public bool blueDead = false;
    public bool greenDead = false;
    public bool yellowDead = false;

    private bool redHit = false;
    private bool blueHit = false;
    private bool greenHit = false;
    private bool yellowHit = false;

    private int count = 0;

    public int playerWinner;

    void Update()
    {
        CheckHealth();
        CheckWinner();
    }

    void CheckHealth()
    {
        if (redDead && redHit == false)
        {
            count++;

            redHit = true;
        }
        if (blueDead && blueHit == false)
        {
            count++;

            blueHit = true;
        }
        if (greenDead && greenHit == false)
        {
            count++;

            greenHit = true;
        }
        if (yellowDead && yellowHit == false)
        {
            count++;

            yellowHit = true;
        }
    }

    void CheckWinner()
    {
        if (count == 3)
        {
            if (redDead == false)
            {
                playerWinner = 0;
            }
            if (blueDead == false)
            {
                playerWinner = 1;
            }
            if (greenDead == false)
            {
                playerWinner = 2;
            }
            if (yellowDead == false)
            {
                playerWinner = 3;
            }
        }
    }
}
