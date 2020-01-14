using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.gameObject.GetComponent<PlayerController>()) {
            ObstacleEnemy obstacle = GameObject.Find("Obstacle-FailEnemy").GetComponent<ObstacleEnemy>();
            obstacle.on();
        }
        // visualize();
    }

     private void visualize() {
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        Color spriteColor = renderer.color;
        spriteColor.a = 0.5f;
        renderer.color = spriteColor;
    }
}
