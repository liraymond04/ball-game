using UnityEngine;
using System.Collections;
using Rewired;

public class Shooting : MonoBehaviour
{

    Player getPlayer;
    PlayerInput getPlayerInput;

    public GameObject playerInput;

    public Transform firePointLaser;
    public Transform firePointEnergy;
    public GameObject laserPrefab;
    public GameObject energyPrefab;
    public GameObject player;
    public GameObject energy;

    public float attack;
    public float attackMax;

    public float knockback;

    public bool facingRight = true;

    private Rewired.Player rplayerInput;

    void Start()
    {
        getPlayer = player.GetComponent<Player>();
        getPlayerInput = playerInput.GetComponent<PlayerInput>();
    }

    void Update()
    {
        rplayerInput = ReInput.players.GetPlayer(getPlayerInput.gameID);

        if (getPlayer.timeLeft <= getPlayer.timeTotal && getPlayer.hitTime == false)
        {
            if (rplayerInput.GetButtonDown("Fire 1"))
            {
                getPlayer.timeLeft += getPlayer.timeAddLaser;

                if(getPlayer.timeLeft >= getPlayer.timeTotal)
                {
                    getPlayer.hitTime = true;

                    getPlayer.timeTotal = getPlayer.hitTimeTotal;
                }

                ShootLaser();
            }
            if(rplayerInput.GetButtonDown("Fire 2"))
            {
                Instantiate(energy, firePointEnergy);
            }
            if(rplayerInput.GetAxisRaw("Fire 2") > 0.5f)
            {
                attack += Time.deltaTime;
            }
            if(rplayerInput.GetButtonUp("Fire 2") && (attack >= attackMax))
            {
                //getPlayer.timeLeft += getPlayer.timeAddEnergy;

                if (getPlayer.timeLeft >= getPlayer.timeTotal)
                {
                    getPlayer.hitTime = true;

                    getPlayer.timeTotal = getPlayer.hitTimeTotal;
                }

                ShootEnergy();

                attack = 0;
            }
            if(rplayerInput.GetButtonUp("Fire 2") && (attack < attackMax))
            {
                attack = 0;
            }
        }
        
        if (rplayerInput.GetAxisRaw("Move Horizontal") > 0.5f || rplayerInput.GetAxisRaw("Move Horizontal") < -0.5f)
        {
            if (rplayerInput.GetAxisRaw("Move Horizontal") > 0.5f && !facingRight)
            {
                Flip();
                facingRight = true;
            }
            else if (rplayerInput.GetAxisRaw("Move Horizontal") < -0.5f && facingRight)
            {
                Flip();
                facingRight = false;
            }
        }
    }

    void ShootLaser()
    {
        Instantiate(laserPrefab, firePointLaser.position, firePointLaser.rotation);
    }

    void ShootEnergy()
    {
        Instantiate(energyPrefab, firePointEnergy.position, firePointEnergy.rotation);
    }

    void Flip()
    {
        facingRight = !facingRight;

        transform.Rotate(0f, 180f, 0f);
    }
}