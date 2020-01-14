using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle_Barricade : MonoBehaviour
{
    private void Start() {
        transform.position += new Vector3(0, 0.2f, 0);
    }
    
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.GetComponent<PlayerController>()) {
            GameController.GameOver();
        } else if (other.GetComponent<EnemyController>()) {
            // Replace it with animation.
            Destroy(gameObject);
        }
    }

}
