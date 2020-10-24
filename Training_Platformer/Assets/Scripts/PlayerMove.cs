using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // Components
    private Rigidbody2D rb;
    private SpriteRenderer spr;

    // Movement
    [SerializeField] private float speed = 0;
    private float inputXFrames = 0, inputXMaxFrames = 1.25f;
    private float moveX = 0;

    // Jump
    [SerializeField] private float jumpForce = 0;
    private int jumpFrames = 0, jumpMaxFrames = 35;
    private bool canJump = false, isJumping = false;
    
    // Inputs
    public static KeyCode xInputPlus = KeyCode.RightArrow, xInputMinus = KeyCode.LeftArrow;
    public static KeyCode jumpInput = KeyCode.Z;


    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        spr = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // Movement
        if (Input.GetKey(xInputPlus)){
            if (inputXFrames < inputXMaxFrames){
                inputXFrames += Time.deltaTime;
            }else{
                inputXFrames = inputXMaxFrames;
            }
            moveX = speed * inputXFrames;
        }else if (Input.GetKey(xInputMinus)){
            if (inputXFrames < inputXMaxFrames){
                inputXFrames += Time.deltaTime;
            }else{
                inputXFrames = inputXMaxFrames;
            }
            moveX = -speed * inputXFrames;
        }else{
            resetMoveX();
        }

        if (Input.GetKey(xInputPlus) && Input.GetKey(xInputMinus)){
            resetMoveX();
        }

        if (Input.GetKeyUp(xInputPlus) || Input.GetKeyUp(xInputMinus)){
            resetMoveX();
        }


        // Jump
        if (Input.GetKeyDown(jumpInput) && canJump){
            canJump = false;
            isJumping = true;
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        if (Input.GetKey(jumpInput) && isJumping){
            if (jumpFrames < jumpMaxFrames){
                jumpFrames++;
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }else{
                isJumping = false;
                jumpFrames = 0;
            }
        }
        
        if (Input.GetKeyUp(jumpInput)){
            isJumping = false;
            jumpFrames = 0;
        }
        
        flipSpr();

        gameObject.transform.position = new Vector3(Mathf.Clamp(transform.position.x, -6.55f, 6.55f), transform.position.y, transform.position.z);
    }

    void FixedUpdate() {
        rb.velocity = new Vector2(moveX, rb.velocity.y);
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.transform.position.y < this.transform.position.y && !isJumping){
            canJump = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision) {
        canJump = false;
    }


    void resetMoveX(){
        moveX = 0;
        inputXFrames = 0;
    }

    void flipSpr(){
        if (moveX > 0){
            spr.flipX = false;
        }else if (moveX < 0){
            spr.flipX = true;
        }
    }
}
