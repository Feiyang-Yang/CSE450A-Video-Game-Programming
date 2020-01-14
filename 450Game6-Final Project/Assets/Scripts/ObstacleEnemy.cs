using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObstacleEnemy : MonoBehaviour
{
    bool isTriggered = false;
    public float movingSpeed;

    void Start() {
        GetComponent<PolygonCollider2D>().isTrigger = true;
    }
    private void Update()
    {
        transform.position -= new Vector3(movingSpeed * Time.deltaTime, 0, 0);
    }
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
    void OnTriggerEnter2D(Collider2D other){
        
        if(other.gameObject.GetComponent<EnemyController>() && isTriggered){
            Destroy(other.gameObject);
        }
    }

    // Can be turned on by obstacle trigger.
    public void on() {
        isTriggered = true;
        GetComponent<PolygonCollider2D>().isTrigger = false;
        visualize();
    }

    private void visualize() {
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        Color spriteColor = renderer.color;
        spriteColor.a = 1f;
        renderer.color = spriteColor;
    }
}
