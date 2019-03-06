using UnityEngine;
using System.Collections;
using Rewired;

[RequireComponent (typeof (Controller2D))]
public class Player : MonoBehaviour 
{

    public PlayerInput playerInput;

	public float maxJumpHeight = 4;
	public float minJumpHeight = 1;
	public float timeToJumpApex = .4f;
	public float accelerationTimeAirborne = .2f;
	public float accelerationTimeGrounded = .1f;
	public float moveSpeed = 6;

	public Vector2 wallJumpClimb;
	public Vector2 wallJumpOff;
	public Vector2 wallLeap;

    public float knockback;
    public float knockbackLength;
    public float knockbackCount;
    public bool knockFromRight;

    public float knockbackMelee;
    public float knockbackLengthMelee;
    public float knockbackCountMelee;

    public float knockbackWall;
    public float damageWall;

	public float wallSlideSpeedMax = 3;
	public float wallStickTime = .25f;
	float timeToWallUnstick;

	float gravity;
	float maxJumpVelocity;
	float minJumpVelocity;
	Vector3 velocity;
	float velocityXSmoothing;
    float curVelocity;

	Controller2D controller;

	Vector2 directionalInput;
	bool wallSliding;
	int wallDirX;

    private Rigidbody2D rb;

    public bool facingRight = true;
    
    public float timeAddLaser = 1;
    public float timeAddEnergy = 2;
    public float timeTotal = 3;
    public float timeLeft = 0;
    public float hitTimeTotal = 1;
    public bool hitTime = false;
    private float timeTotalStart;

    private Rewired.Player rplayerInput;
   
	void Start() 
    {
        playerInput = GetComponent<PlayerInput>();

		controller = GetComponent<Controller2D> ();

		gravity = -(2 * maxJumpHeight) / Mathf.Pow (timeToJumpApex, 2);
		maxJumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
		minJumpVelocity = Mathf.Sqrt (2 * Mathf.Abs (gravity) * minJumpHeight);

        rb = GetComponent<Rigidbody2D>();

        timeTotalStart = timeTotal;
    }

	void Update() 
    {
        rplayerInput = ReInput.players.GetPlayer(playerInput.gameID);

        CalculateVelocity ();
		HandleWallSliding ();
        DirectionFacing ();
        KnockBackDash();
        KnockBackMelee();
        ShootTime();

		controller.Move (velocity * Time.deltaTime, directionalInput);

		if (controller.collisions.above || controller.collisions.below) 
        {
			if (controller.collisions.slidingDownMaxSlope) 
            {
				velocity.y += controller.collisions.slopeNormal.y * -gravity * Time.deltaTime;
			} else 
            {
				velocity.y = 0;
			}
		}
    }

    public void KnockBackDash()
    {
        if (knockbackCount <= 0)
        {
            rb.velocity = new Vector2(rb.velocity.x / 1.5f, rb.velocity.y / 1.1f);
        }
        else
        {
            if (knockFromRight)
            {
                rb.velocity = new Vector2(-knockback, knockback);
            }
            if (!knockFromRight)
            {
                rb.velocity = new Vector2(knockback, knockback);
            }

            knockbackCount -= Time.deltaTime;
        }
    }

    public void KnockBackMelee()
    {
        if (knockbackCountMelee <= 0)
        {
            rb.velocity = new Vector2(rb.velocity.x / 1.5f, rb.velocity.y / 1.1f);
        }
        else
        {
            if (knockFromRight)
            {
                rb.velocity = new Vector2(-knockbackMelee, knockbackMelee);
            }
            if (!knockFromRight)
            {
                rb.velocity = new Vector2(knockbackMelee, knockbackMelee);
            }

            knockbackCountMelee -= Time.deltaTime;
        }
    }

    public void ShootTime()
    {
        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
        }
        if (timeLeft <= 0)
        {
            hitTime = false;

            timeTotal = timeTotalStart;
        }
    }

	public void SetDirectionalInput (Vector2 input) 
    {
		directionalInput = input;
	}

    public void DirectionFacing()
    {
        if (rplayerInput.GetAxisRaw("Move Horizontal") > 0.5f || rplayerInput.GetAxisRaw("Move Horizontal") < -0.5f)
        {
            if (rplayerInput.GetAxisRaw("Move Horizontal") > 0.5f && !facingRight)
            {
                facingRight = true;
            }
            else if (rplayerInput.GetAxisRaw("Move Horizontal") < -0.5f && facingRight)
            {
                facingRight = false;
            }
        }
    }

	public void OnJumpInputDown() 
    {
		if (wallSliding) 
        {
			if (wallDirX == directionalInput.x) 
            {
				velocity.x = -wallDirX * wallJumpClimb.x;
				velocity.y = wallJumpClimb.y;
			}
			else if (directionalInput.x == 0) 
            {
				velocity.x = -wallDirX * wallJumpOff.x;
				velocity.y = wallJumpOff.y;
			}
			else 
            {
				velocity.x = -wallDirX * wallLeap.x;
				velocity.y = wallLeap.y;
			}
		}
		if (controller.collisions.below) 
        {
			if (controller.collisions.slidingDownMaxSlope)
            {
				if (directionalInput.x != -Mathf.Sign (controller.collisions.slopeNormal.x)) 
                { // not jumping against max slope
					velocity.y = maxJumpVelocity * controller.collisions.slopeNormal.y;
					velocity.x = maxJumpVelocity * controller.collisions.slopeNormal.x;
				}
			} 
            else 
            {
				velocity.y = maxJumpVelocity;
			}
		}
	}

    public void OnJumpInputUp()
    {
        if (velocity.y > minJumpVelocity)
        {
			velocity.y = minJumpVelocity;
		}
	}
		

	void HandleWallSliding()
    {
		wallDirX = (controller.collisions.left) ? -1 : 1;
		wallSliding = false;
		if ((controller.collisions.left || controller.collisions.right) && !controller.collisions.below && velocity.y < 0) 
        {
			wallSliding = true;

			if (velocity.y < -wallSlideSpeedMax) 
            {
				velocity.y = -wallSlideSpeedMax;
			}

			if (timeToWallUnstick > 0)
            {
				velocityXSmoothing = 0;
				velocity.x = 0;

				if (directionalInput.x != wallDirX && directionalInput.x != 0) 
                {
					timeToWallUnstick -= Time.deltaTime;
				}
				else 
                {
					timeToWallUnstick = wallStickTime;
				}
			}
			else 
            {
				timeToWallUnstick = wallStickTime;
			}

		}

	}

	void CalculateVelocity() 
    {
		float targetVelocityX = directionalInput.x * moveSpeed;
		velocity.x = Mathf.SmoothDamp (velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below)?accelerationTimeGrounded:accelerationTimeAirborne);
		velocity.y += gravity * Time.deltaTime;
	}

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.tag == "Dash")
        {
            //Debug.Log(hitInfo.name);

            knockbackCount = knockbackLength;

            if (hitInfo.transform.position.x > transform.position.x)
            {
                knockFromRight = true;
            }
            else
            {
                knockFromRight = false;
            }
        }

        if (hitInfo.tag == "Melee")
        {
            //Debug.Log(hitInfo.name);

            knockbackCountMelee = knockbackLengthMelee;

            if (hitInfo.transform.position.x > transform.position.x)
            {
                knockFromRight = true;
            }
            else
            {
                knockFromRight = false;
            }
        }

        if (hitInfo.tag == "Wall")
        {
            GetComponent<RedHealth>().TakeDamage(damageWall);

            if(hitInfo.transform.position.x > transform.position.x)
            {
                rb.velocity = new Vector2(-knockbackWall, knockbackWall);
            }
            else
            {
                rb.velocity = new Vector2(knockbackWall, knockbackWall);
            }
        }

        if (hitInfo.tag == "Bounds")
        {
            GetComponent<RedHealth>().TakeDamage(100);
        }
    }

}
