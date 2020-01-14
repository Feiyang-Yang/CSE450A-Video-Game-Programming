using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{   
    // Outlets
    Rigidbody2D rigidbody;

    // State Tracking
    Transform target;

    // Methods
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Some Dynamic projectile attributes
        float accerlation = GameController.instance.missileSpeed / 2f;
        float maxSpeed = GameController.instance.missileSpeed;

        // Home in on target
        ChooseNearestTarget();
        if(target != null){
            // Rotate towards target
            Vector2 directionToTarget = target.position - transform.position;
            float angle = Mathf.Atan2(directionToTarget.y,directionToTarget.x) * Mathf.Rad2Deg;

            rigidbody.MoveRotation(angle);
        }

        // Acceleration forward
        rigidbody.AddForce(transform.right * accerlation);

        // Cap max speed
        rigidbody.velocity = Vector2.ClampMagnitude(rigidbody.velocity, maxSpeed);
    }

    // Missile to find the nearest target
    void ChooseNearestTarget(){
        float closestDistance = 9999f; // Pick a really high number as default
        Asteroid[] asteroids = FindObjectsOfType<Asteroid>();
        for(int i=0;i<asteroids.Length;i++){
            Asteroid asteroid = asteroids[i];

            // Asteroid must be to our right
            if(asteroid.transform.position.x > transform.position.x){
                Vector2 directionToTarget = asteroid.transform.position - transform.position;

                // Filter for the closest target we have seen so far
                if(directionToTarget.sqrMagnitude < closestDistance){
                    // Update closest distance for future comparisons
                    closestDistance = directionToTarget.sqrMagnitude;

                    //Store reference to closest target we have seen so far
                    target = asteroid.transform;
                }
            }
        }

    }

    // on collision, we check if we have impacted an asteroid. Our projectile will destroy 
    // asteroid, destroy itself, and create an explosion then destroy
    void OnCollisionExit2D(Collision2D collision){
        // Only explode on and destroy Asteroids
        if(collision.gameObject.GetComponent<Asteroid>()){
            Destroy(collision.gameObject);
            Destroy(gameObject);

            // Create an explosion and destroy it soon after
            GameObject explosion = Instantiate(GameController.instance.explosionPrefab, collision.transform.position, Quaternion.identity);
            Destroy(explosion, 0.25f);
        }
    }

    void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.GetComponent<Asteroid>()){
            Destroy(collision.gameObject);
            Destroy(gameObject);
            GameObject anExplosion = Instantiate(GameController.instance.explosionPrefab, collision.transform.position, Quaternion.identity);
            Destroy(anExplosion, 0.25f);

            GameController.instance.EarnPoints(10);
        }
    }
}
