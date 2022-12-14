using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public Animator anim;
    public CircleCollider2D cc;
    
    private bool dashingCooldown = false;

    private bool dashing = false;
    public string nextLevel = "";
    public GameObject clearScreen;
    
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

        if (hvMove.x != 0) {
            anim.SetBool("Walk", true);
        }
        else {
            anim.SetBool("Walk", false);
        }
        

        if (Input.GetKeyDown(KeyCode.E) && (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 1)  && !dashingCooldown) {
            dashingCooldown = true;

            dashing = true;
            StopCoroutine(DashDuration());
            StartCoroutine(DashDuration());

            anim.SetTrigger("Dash");
            if (dashCount <= 0) {
                Debug.Log(dashCount);
                dashCount = startDashCount;
                rb.velocity = Vector2.zero;
            }
            else {
                dashCount -= Time.deltaTime;

                if (Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == 1) {
                    rb.velocity = Vector2.up * dashSpeed/2;
                }
                else if (Input.GetAxisRaw("Horizontal") == -1 && Input.GetAxisRaw("Vertical") == 1) {
                    rb.velocity = Vector2.left * dashSpeed + Vector2.up * dashSpeed/2;
                }
                else if (Input.GetAxisRaw("Horizontal") == 1 && Input.GetAxisRaw("Vertical") == 1) {
                    rb.velocity = Vector2.right * dashSpeed + Vector2.up * dashSpeed/2;
                }
                else if (Input.GetAxisRaw("Horizontal") == -1) {
                    rb.velocity = Vector2.left * dashSpeed;
                }
                else if (Input.GetAxisRaw("Horizontal") == 1) {
                    rb.velocity = Vector2.right * dashSpeed;
                }
            }
            dashCount -= Time.deltaTime;

            if (Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == 1) {
                rb.velocity = Vector2.up * dashSpeed/2;
            }
            else if (Input.GetAxisRaw("Horizontal") == -1 && Input.GetAxisRaw("Vertical") == 1) {
                rb.velocity = Vector2.left * dashSpeed + Vector2.up * dashSpeed/2;
            }
            else if (Input.GetAxisRaw("Horizontal") == 1 && Input.GetAxisRaw("Vertical") == 1) {
                rb.velocity = Vector2.right * dashSpeed + Vector2.up * dashSpeed/2;
            }
            else if (Input.GetAxisRaw("Horizontal") == -1) {
                rb.velocity = Vector2.left * dashSpeed;
            }
            else if (Input.GetAxisRaw("Horizontal") == 1) {
                rb.velocity = Vector2.right * dashSpeed;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D other){
        if ((other.gameObject.tag == "enemy") && dashing){
            Destroy(other.gameObject);
        } 
        if (other.gameObject.tag == "flag") {
            clearScreen.SetActive(true);
            StartCoroutine(LoadNextScene());
        }
    }

    IEnumerator LoadNextScene() {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(nextLevel);
    }

    void FixedUpdate() {

        controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump); // replace with crouch if adding
        jump = false;


    }

    IEnumerator DashDuration(){
        yield return new WaitForSeconds(0.5f);
        dashing = false;
        dashingCooldown = false;
        yield return null;
    }


}