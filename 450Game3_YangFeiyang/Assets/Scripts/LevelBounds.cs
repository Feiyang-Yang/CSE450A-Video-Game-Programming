using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelBounds : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision){
        SceneManager.LoadScene("Q4");
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
