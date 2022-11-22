using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController2D controller;
    float horizontalMove = 0f;
    public float runSpeed = 40f;
    bool jump = false;
    // bool crouch = false;

    public float dashSpeed;
    private float dashCount;
    public float startDashCount;
    private bool FaceRight = true;
    public Rigidbody2D rb;
    
    void Start() {
        dashCount = startDashCount;
    }

    // Update is called once per frame
    void Update() {
        
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        if (Input.GetButtonDown("Jump")) {
            jump = true;
        }
        // if (Input.GetButtonDown("Crouch")) {
        //     crouch = true;
        // } else if (Input.GetButtonUp("Crouch")) {
        //     crouch = false;
        // }

         Vector3 hvMove = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.0f);

        

        if ((hvMove.x <0 && !FaceRight) || (hvMove.x >0 && FaceRight)){
                        FaceRight = !FaceRight;
                  }


        if (Input.GetKeyDown(KeyCode.E)) {
            if (dashCount <= 0) {
                dashCount = startDashCount;
                rb.velocity = Vector2.zero;
            }
            else {
                dashCount -= Time.deltaTime;

                if (!FaceRight) {
                    rb.velocity = Vector2.left * dashSpeed;
                }
                else if (FaceRight) {
                    rb.velocity = Vector2.right * dashSpeed;
                }
            }
            dashCount -= Time.deltaTime;

            if (FaceRight) {
                rb.velocity = Vector2.left * dashSpeed;
            }
            else if (!FaceRight) {
                rb.velocity = Vector2.right * dashSpeed;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D other){
        if (other.gameObject.tag == "enemy"){
            Destroy(other.gameObject);
        }
     }



    void FixedUpdate() {

        controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump); // replace with crouch if adding
        jump = false;


    }


}