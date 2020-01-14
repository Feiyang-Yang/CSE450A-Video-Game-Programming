using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObstacleAll : MonoBehaviour
{
    public float movingSpeed;
    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.GetComponent<PlayerController>()){
            GameController.GameOver();
        }
        if (other.gameObject.GetComponent<EnemyController>())
        {
            Destroy(other.gameObject);
        }
    }

    private void Update()
    {
        transform.position -= new Vector3(movingSpeed * Time.deltaTime, 0, 0);
    }
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
