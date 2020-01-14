using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{   
    void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.GetComponent<Projectile>()){
            PlayerController.instance.score++;
            PlayerPrefs.SetInt("Score",PlayerController.instance.score);
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
