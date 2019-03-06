using UnityEngine;
using System.Collections;
using Rewired;

public class Shooting1 : MonoBehaviour
{

    Player2 getPlayer;
    PlayerInput2 getPlayerInput;

    public GameObject playerInput;

    public Transform firePoint;
    public GameObject firePointPrefab;
    public GameObject shieldPrefab;
    public GameObject player;

    public float attack;
    public float attackMax;

    public float knockback;

    public bool facingRight = true;

    public float shieldTime;
    public float shieldTimeMax;
    public bool shieldTimeHit = false;
    public float shieldTimeHitCooldown;
    private float shieldTimeHitCooldownStart;

    private float xPos;
    private float yPos;

    private Rewired.Player rplayerInput;

    void Start()
    {
        getPlayer = player.GetComponent<Player2>();
        getPlayerInput = playerInput.GetComponent<PlayerInput2>();

        shieldTimeHitCooldownStart = shieldTimeHitCooldown;
    }

    void Update()
    {
        rplayerInput = ReInput.players.GetPlayer(getPlayerInput.gameID);

        ProcessInput();
        
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

        if(shieldTimeHit)
        {
            shieldTimeHitCooldown -= Time.deltaTime;

            if(shieldTimeHitCooldown <= 0)
            {
                shieldTimeHit = false;
            }
        }
        else
        {
            shieldTimeHitCooldown = shieldTimeHitCooldownStart;
        }
    }

    void ShootForceField()
    {
        Instantiate(firePointPrefab, firePoint.position, firePoint.rotation);
    }

    void Flip()
    {
        facingRight = !facingRight;

        transform.Rotate(0f, 180f, 0f);
    }

    void ProcessInput()
    {
        if (getPlayer.timeLeft <= getPlayer.timeTotal && getPlayer.hitTime == false)
        {
            if (rplayerInput.GetButtonDown("Fire 1"))
            {
                getPlayer.timeLeft += getPlayer.timeAdd;

                if (getPlayer.timeLeft >= getPlayer.timeTotal)
                {
                    getPlayer.hitTime = true;

                    getPlayer.timeTotal = getPlayer.hitTimeTotal;
                }

                ShootForceField();
            }
        }
        if (rplayerInput.GetAxisRaw("Fire 2") > 0.5f && !shieldTimeHit)
        {
            if (shieldTime < shieldTimeMax)
            {
                shieldTime += Time.deltaTime;
                getPlayer.transform.position = new Vector2(xPos, yPos);

                shieldPrefab.GetComponent<SpriteRenderer>().enabled = true;
                shieldPrefab.GetComponent<CircleCollider2D>().enabled = true;
            }
            else if (shieldTime >= shieldTimeMax)
            {
                shieldTimeHit = true;
                shieldTime = 0;

                shieldPrefab.GetComponent<SpriteRenderer>().enabled = false;
                shieldPrefab.GetComponent<CircleCollider2D>().enabled = false;
            }
        }
        if (rplayerInput.GetButtonUp("Fire 2") && (shieldTime < shieldTimeMax) && !shieldTimeHit)
        {
            shieldPrefab.GetComponent<SpriteRenderer>().enabled = false;
            shieldPrefab.GetComponent<CircleCollider2D>().enabled = false;

            shieldTime = 0;
        }
        if (rplayerInput.GetAxisRaw("Fire 2") == 0)
        {
            xPos = getPlayer.transform.position.x;
            yPos = getPlayer.transform.position.y;
        }
    }
}