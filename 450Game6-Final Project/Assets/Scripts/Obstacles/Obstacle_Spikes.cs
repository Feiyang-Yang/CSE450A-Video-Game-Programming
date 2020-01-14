using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle_Spikes : Obstacle_Activatable {
    // Outlets
    Animator animator;

    // Start is called before the first frame update
    void Start() {
        animator = GetComponent<Animator>();
    }

    public override void Activate() {
        activated = true;
        animator.SetBool("activated", true);
        StartCoroutine("Deactivate");
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (activated) {
            if (other.GetComponent<EnemyController>()) {
                // kill enemy
                Destroy(other.gameObject);
            }
        }
    }

    private IEnumerator Deactivate() {
        yield return new WaitForSeconds(activeTime);
        activated = false;
        animator.SetBool("activated", false);
    }
}