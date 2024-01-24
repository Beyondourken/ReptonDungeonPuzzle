using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
   
     Rigidbody2D rb;
     Animator animator;

     [SerializeField] public Transform groundCheck;
     [SerializeField] public LayerMask groundLayer;
     [SerializeField] public LayerMask climbLayer;

     float horizontal;
     float vertical;
     [SerializeField] public float speed = 8f;
     [SerializeField] public float jumpingPower = 5f;
     float currentY = 0;
    // bool isFacingRight = true;
      bool isJumping = false;
      bool isFalling = false;

      bool isOnLadder = false;
      const float GROUNDRADIUS = 0.2f;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate() {
    //  if(rb.bodyType == RigidbodyType2D.Static){rb.bodyType = RigidbodyType2D.Dynamic;}
    rb.velocity = new Vector2 (horizontal * speed, rb.velocity.y* speed);
     Debug.Log("h " + horizontal + " vert " + rb.velocity.y* speed);
     
         if (isJumping && rb.velocity.y <0f) { isFalling = true; isJumping = false;}
         
        if(IsGrounded() && isFalling ) {
          
    // //      rb.velocity = new Vector2(horizontal* speed,rb.velocity.y * 0.5f); 
    // // //     if(rb.velocity.y <= 0f) {
    // // //     isJumping = false;
    // // //     isFalling = true;}
    // // //   }
    // // //     if(IsGrounded()  && isFalling) {
    // //    }else {
           rb.bodyType = RigidbodyType2D.Static;
           isFalling = false;
           
    //       }
        
      }
   
    //   //  rb.velocity = new Vector2(0f,0f);
            
      
      if(horizontal == 0  || !IsGrounded()) {
          animator.SetBool("isWalking",false);}
      // if(!isFacingRight && horizontal > 0f) {
      //   Flip();
      // } else if(isFacingRight && horizontal < 0f) {
      //   Flip();
      // }
    }
    public void Move(InputAction.CallbackContext context) {

  
      if(!IsGrounded()) return;
      if(rb.bodyType == RigidbodyType2D.Static){rb.bodyType = RigidbodyType2D.Dynamic;}
     if(context.canceled) {
      horizontal = 0;}
      if(context.performed ) {
      horizontal = context.ReadValue<Vector2>().x;}
      
        if(horizontal != 0 || context.ReadValue<Vector2>().y != 0 ) {
          animator.SetFloat("x", horizontal);
          animator.SetFloat("y", context.ReadValue<Vector2>().y);
          animator.SetBool("isWalking",true);
        } else {animator.SetBool("isWalking",false); }
        
    }

    public void Jump(InputAction.CallbackContext context) {
     if(rb.bodyType == RigidbodyType2D.Static){rb.bodyType = RigidbodyType2D.Dynamic;}
      if(context.performed && IsGrounded()) {
        rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
         isJumping = true;
        
        
      }
      if(context.canceled && rb.velocity.y > 0f) {
        
        rb.velocity = new Vector2(rb.velocity.x,rb.velocity.y * 0.5f);
      
        
      }
    }

    public void Climb(InputAction.CallbackContext context) {
         if(!isOnLadder) return;

        if(context.performed ) {
      vertical = context.ReadValue<Vector2>().y;
      horizontal = 0;
      
       
          animator.SetFloat("x", horizontal);
          animator.SetFloat("y", vertical);
          animator.SetBool("isWalking",true);
    }
         if(context.canceled) {
          animator.SetBool("isWalking",false);
         }
    }

    // private bool IsOnLadder() {
    //   Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position,GROUNDRADIUS +10f,climbLayer);
    //   if(colliders.Length > 0) {
        
    //     return true;
     
    //   } else {
    //     return false;
    
    //   }
    
    // }

    private void OnTriggerEnter2D(Collider2D other) {
      if (other.gameObject.tag == "Ladder") {
        isOnLadder = true;
       
      }
    }

    private void OnTriggerExit2D(Collider2D other) {
      if (other.gameObject.tag == "Ladder") {
        isOnLadder = false;
        
      }
    }



    private bool IsGrounded() {
      Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position,GROUNDRADIUS,groundLayer);
      if(colliders.Length > 0) {
        return true;
     
      } else {
        return false;
    
      }
    
    }

    // private void Flip() {
    //   isFacingRight = !isFacingRight;
    //   Vector3 localScale = transform.localScale;
    //   localScale.x *= -1f;
    //   transform.localScale = localScale;
    // }

   





}

