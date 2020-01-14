using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{   
    //Outlets
    Rigidbody2D _rb;

    //Configurations
    public float speed;
    public float rotationSpeed;

    // Start is called before the first frame update
    void Start(){
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update(){
        // Turn Left
        if(Input.GetKey(KeyCode.LeftArrow)){
            _rb.AddTorque(rotationSpeed* Time.deltaTime);
        }

        // Turn Right
        if(Input.GetKey(KeyCode.RightArrow)){
            _rb.AddTorque(-rotationSpeed* Time.deltaTime);
        }

        // Thrust Forward
        if(Input.GetKey(KeyCode.Space)){
            //use right as 'forward' because our art faces to the right
            _rb.AddRelativeForce(Vector2.right * speed * Time.deltaTime);
        }
    }
}
