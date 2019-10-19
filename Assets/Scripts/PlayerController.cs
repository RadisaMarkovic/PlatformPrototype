using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;




public class PlayerController : MonoBehaviour 
{
    Rigidbody2D rigidBody;
    [SerializeField]
    private float speed = 10f;
    [SerializeField]
    private float jumpForce = 200f;
    [SerializeField]
    private float maxFallingDistance = -10f;
    private Animator anim;
    private bool isRight = true;
    private bool isOnGround = true;
    private bool isDead;
    public bool IsDead {get => isDead;}



    void Start ()
    {
         isDead = false;
         rigidBody = GetComponent<Rigidbody2D>() as Rigidbody2D;
         anim = GetComponent<Animator>() as Animator; 
	}
	

    void Update() 
    {
        if(IsPlayerOutOfWorldBounds())
        {
            GameManager.ReloadLevel();
        }
    }


    bool IsPlayerOutOfWorldBounds() 
    {
        if(transform.position.y < maxFallingDistance)
        {
            isDead = true;
           
            return true;
        }

        return false;
    }
	
	void FixedUpdate ()
    {
        float inputDirection = Input.GetAxis("Horizontal");
        rigidBody.velocity = new Vector2(speed * inputDirection, rigidBody.velocity.y);

        anim.SetFloat("speed",Mathf.Abs(inputDirection));

        if (inputDirection > 0 && !isRight)
            Flip();

       else  if (inputDirection < 0 && isRight)
            Flip();


        

        if (Input.GetKeyDown(KeyCode.Space) && isOnGround == true)
        {
            rigidBody.AddForce(new Vector3(0,jumpForce),ForceMode2D.Force);
            isOnGround = false;
        }

        
	}

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
           
            isOnGround = true;
        }
        
    }

//Flip the characther to opposite direction
    private void Flip()
    {
            isRight = !isRight;
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
    }

  

}
