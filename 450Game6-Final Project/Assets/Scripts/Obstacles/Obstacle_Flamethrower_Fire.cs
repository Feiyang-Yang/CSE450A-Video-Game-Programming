using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle_Flamethrower_Fire : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.GetComponent<EnemyController>()) {
            // kill enemy
            Destroy(other.gameObject);
        }
    }
}
