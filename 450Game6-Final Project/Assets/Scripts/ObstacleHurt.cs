using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObstacleHurt : MonoBehaviour
{
    public float movingSpeed;
    void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.GetComponent<PlayerController>()){
            // SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if (other.gameObject.GetComponent<EnemyController>())
        {
            //Destroy(other.gameObject);
        }
    }

    private void Update()
    {
        transform.position -= new Vector3(movingSpeed * Time.deltaTime, 0, 0);
    }
}
