using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController2D : MonoBehaviour
{
    [SerializeField]private Rigidbody2D Player;
    [SerializeField]private CapsuleCollider2D coll;
    [SerializeField]private SpriteRenderer sprite;
    [SerializeField]private Animator anim;
    [SerializeField]private LayerMask Ground;

    private enum MovementState { idle, run, jump, fall }

    [HideInInspector]public float DirX, DirY;
    float MovementSpeed = 10F;
    float JumpForce = 15F;
    float DashForce = 32F;
    float DashTime = 0.1F;
    

    bool HasDashed = true;
    bool IsGrounded(){return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, Ground);}

    void Update()
        {
            DirX = Mathf.Clamp(1000*Input.GetAxisRaw("Horizontal"),-1f,1f);
            DirY = Input.GetAxisRaw("Vertical");

            if(Time.timeScale != 0f)
                Player.velocity = new Vector2(DirX * MovementSpeed, Player.velocity.y - 0.4f);

            if(Input.GetButtonDown("Jump") && IsGrounded())
            {
                Player.velocity = new Vector2(DirX * MovementSpeed, JumpForce);
                HasDashed = false;
            }

            if(Input.GetKeyDown("left shift") || Input.GetKeyDown(KeyCode.JoystickButton2) && !HasDashed )
            {
                if(IsGrounded() == false)
                    StartCoroutine(Dash());
            } 
        }

    IEnumerator Dash()
        {
            float StartTime = Time.time;
            float localDir = DirX;
            if (localDir == 0)
                yield return null;
            else
            {
                if(!HasDashed)
                {
                    while(Time.time < StartTime + DashTime)
                    {
                        Player.velocity = new Vector2(localDir * DashForce, 0.4f);
                        yield return null;
                    }
                }      
            }
            HasDashed = true;
        }

    private void UpdateAnimStat()
        {
            MovementState state;
            if(DirX > 0)
            {
                state = MovementState.run;
                sprite.flipX = false;
            }
            else if(DirX < 0)
            {
                state = MovementState.run;
                sprite.flipX = true;
            }
            else
                state = MovementState.idle;
                
            if (Player.velocity.y > .3f &&IsGrounded() == false)
            {
                state = MovementState.jump;
            }
            else if (Player.velocity.y < -1f)
            {
                state = MovementState.fall;
            }
            
            anim.SetInteger("state", (int)state);
        }
    
    private void FixedUpdate(){UpdateAnimStat();}
    
    //checking special collisions
    private void OnTriggerEnter2D(Collider2D coll) 
        {
            if(coll.gameObject.CompareTag("Collectible"))
            {
                Destroy(coll.gameObject);
                GameplayManager.AddToCollectibleCounter();
            }

            if(coll.gameObject.CompareTag("Obstacle"))
                GameplayManager.GameHasEnded = true;
        }
}
