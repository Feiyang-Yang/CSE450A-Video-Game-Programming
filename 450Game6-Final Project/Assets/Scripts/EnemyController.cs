using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyController : MonoBehaviour
{   
    //Outlets
    Rigidbody2D _rb;
    public float movingSpeed;
    public int onLane;
    public bool reduced;
    
    public static int [] count = {0,0,0,0,0};

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        count[onLane] += 1;
        // print("Enemy spawns on lane " + onLane + " with current total enemys " + count[onLane]);
        reduced = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(reduced == true){
            transform.position += new Vector3(1f * movingSpeed * Time.deltaTime, 0, 0);
        } else {
            transform.position += new Vector3(0.01f * movingSpeed * Time.deltaTime, 0, 0);
        }

        //Press space to add constraints on the RigidBody (freeze all positions and rotations)
        // if (Input.GetKeyDown(KeyCode.Space))
        // {
        //     //Freeze all positions and rotations
        //     // _rb.constraints = RigidbodyConstraints2D.FreezeAll;
        //     this.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            
        //     // _rb.constraints = RigidbodyConstraints2D.FreezeRotationX | RigidbodyConstraints2D.FreezeRotationZ;
        //     // _rb.constraints = RigidbodyConstraints2D.FreezeRotation;

        //     if (Time.timeScale == 1.0f)
        //         Time.timeScale = 0.2f;
        //     else
        //         Time.timeScale = 1.0f;

        // }

        // Press Q to only reduce the speed of Enemy
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if(reduced == true){
                reduced = false;
            } else {
                reduced = true;
            }
        }

        //Press the A key to rotate the GameObject (if there are no rotation constraints)
        if (Input.GetKey(KeyCode.A))
        {
            // _rb.angularVelocity = Vector3.right * m_Speed * Time.deltaTime;
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnDestroy() {
        if (count[onLane] > 0) {
            count[onLane] -= 1;
            // print("Enemy destroyed on lane " + onLane + " with current total enemys " + count[onLane]);
        }
        PlayerController.instance.AddScore(3);
    }

}
