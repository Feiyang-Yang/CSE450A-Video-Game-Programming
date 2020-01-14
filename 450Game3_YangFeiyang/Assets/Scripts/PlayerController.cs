using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{  
    public static PlayerController instance; 
    // Outlet
    Rigidbody2D rigidbody;
    ForceMode2D m_forceMode;
    public Transform aimPivot;
    public GameObject projectilePrefab;
    SpriteRenderer sprite;
    Animator animator;
    public Text scoreUI;

    // State Tracking
    public int jumpsLeft;
    public int score;
    public bool isPaused;

    // Methods
    void Awake(){
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {   
        //check this line compared to //rigidbody = GetComponent<Rigidbody2D>();
        rigidbody = GetComponent<Rigidbody2D>();
       //rigidbody = GetComponentInParent<Rigidbody2D>();
       sprite = GetComponent<SpriteRenderer>();
       animator = GetComponent<Animator>();

       score = PlayerPrefs.GetInt("Score");
    }

    void FixedUpdate(){
        //This Update Event is synchronized with the Physics Engine
        animator.SetFloat("Speed", rigidbody.velocity.magnitude);
        if(rigidbody.velocity.magnitude > 0){
            animator.speed = rigidbody.velocity.magnitude / 3f;
        } else {
            animator.speed = 1f;
        }
    }

    // Update is called once per frame
    void Update()
    {   
        // We stop the Update loop execution right away if the game is paused.
        if(isPaused){
            return;
        }
        // Update UI
        scoreUI.text = score.ToString();
        // Move player left
        if(Input.GetKey(KeyCode.A)){
            rigidbody.AddForce(Vector2.left*12f);
            sprite.flipX = true;
        }
        // Move player right
        if(Input.GetKey(KeyCode.D)){
            rigidbody.AddForce(Vector2.right*12f);
            sprite.flipX = false;
        }
        // Make player jump
        if(Input.GetKeyDown(KeyCode.Space) && jumpsLeft>0){
            jumpsLeft--;
            // m_forceMode = ForceMode2D.Impulse;
            rigidbody.AddForce(Vector2.up*5f, ForceMode2D.Impulse);       
        }
        animator.SetInteger("JumpsLeft",jumpsLeft);

        // Aim Toward Mouse
        Vector3 mousePosition = Input.mousePosition;
        Vector3 mousePositionInWorld = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector3 directionFromPlayerToMouse = mousePositionInWorld - transform.position;

        float radiansToMouse = Mathf.Atan2(directionFromPlayerToMouse.y, directionFromPlayerToMouse.x);
        float angleToMouse = radiansToMouse * 180f / Mathf.PI;

        aimPivot.transform.rotation = Quaternion.Euler(0,0,angleToMouse);
        
        // Shoot
        if(Input.GetMouseButtonDown(0)){
            GameObject newProjectile = Instantiate(projectilePrefab);
            newProjectile.transform.position = transform.position;
            newProjectile.transform.rotation = aimPivot.rotation;
        }

        // the Escape key will trigger showing the menu.
        if(Input.GetKey(KeyCode.Escape)){
            MenuController.instance.Show();
        }
    }

    void OnCollisionEnter2D(Collision2D collision){
        // Check that we collided with the ground
        if(collision.gameObject.layer == LayerMask.NameToLayer("Ground")){

            // Check what is directly below our character's feet
            RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, -transform.up,1f);
            // Debug.DrawRay(transform.position, -transform.up*0.7f); //Visualizes the Raycast

            // We might have multiple things beneath our character's feet at once
            for(int i=0; i < hits.Length; i++){
                //check that we collided with the ground right below our feet
                if(hits[i].collider.gameObject.layer == LayerMask.NameToLayer("Ground")){
                    // Reset jump count
                    jumpsLeft = 2;
                }
            }
        }
    }
}
