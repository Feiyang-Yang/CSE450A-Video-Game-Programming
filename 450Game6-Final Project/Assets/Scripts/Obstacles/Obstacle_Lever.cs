using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle_Lever : MonoBehaviour {
    // Outlets
    public bool activated;
    public GameObject trap;
    public Sprite activeSprite;
    SpriteRenderer ourSprite;

    // Start is called before the first frame update
    void Start() {
        ourSprite = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (!activated) {
            if (other.GetComponent<PlayerController>()) {
                activated = true;
                ourSprite.sprite = activeSprite;
                if (trap != null) {
                    trap.GetComponent<Obstacle_Activatable>().Activate();
                }
            }
        }
    }
}