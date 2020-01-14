using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCatchTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D enemy) {
        if (enemy.GetComponent<EnemyController>()) {
            // Add game over animation before
            // showing a prompt with a button to let user manually trigger
            // game reload.
            GameController.GameOver();
        }
    }
}
