using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicForPlayer : MonoBehaviour
{
    public AudioSource walk;
    public AudioSource changeLane;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.LeftArrow)){
            walk.Play();
        }
        if(Input.GetKey(KeyCode.RightArrow)){
            walk.Play();
        }
        if(Input.GetKey(KeyCode.UpArrow)){
            changeLane.Play();
        }
        if(Input.GetKey(KeyCode.DownArrow)){
            changeLane.Play();
        }
    }
}
