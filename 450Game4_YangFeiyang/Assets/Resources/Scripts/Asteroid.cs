using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    // Outlets
    Rigidbody2D rigidbody;

    // Methods
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Always move left
        rigidbody.velocity = Vector2.left * Random.Range(0.5f,3f);
    }

    void OnBecameInvisible(){
        // Destroy when offscreen
        Destroy(gameObject);
    }
}
