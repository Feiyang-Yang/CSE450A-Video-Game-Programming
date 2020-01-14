using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    //Outlets
    public int laneNumber; 
    public GameObject revolver;
    public GameObject shotgun;
    public Text scoreUI;
    public Text highScoreUI;
    
    Animator animator;
    // Tracking States
    public int currentLaneNumber; // Temporarily defaults to 2
    public int score;
    public int highScore;
    private float lastSecond = 0;
    public bool hasPowerUp;
    public bool isPaused;

    public enum Powerup {
        None,
        Revolver,
        Shotgun
    };


    // private property
    const float deactivateDelay = 1.0f;

    void Awake()
    {
        instance = this;
    }

    private void Start() {
        animator = GetComponent<Animator>();
        // SetPowerUp(true); //test

        highScore = PlayerPrefs.GetInt("HighScore");
    }

    // Update is called once per frame
    void Update()
    {
        highScoreUI.text = highScore.ToString();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            MenuController.instance.ToggleMenu();
        }

        if (isPaused)
        {
            return;
        }

        scoreUI.text = score.ToString();
        SwitchLane();
        Move();
        UsePowerUp();

        if ((Time.time - lastSecond) >= 1)
        {
            AddScore(1);
            lastSecond = Mathf.Floor(Time.time);
        }
    }

    private void Move() {
        // Moving horizontally
        if(Input.GetKey(KeyCode.LeftArrow)){
            transform.position += new Vector3(-0.1f,0,0);
        }
        if(Input.GetKey(KeyCode.RightArrow)){
            // transform.position += new Vector3(0.1f,0,0);

            Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
            if(screenPos.x < Screen.width-18f){
                transform.position += new Vector3(0.1f,0,0);
            } else {
                print("Right bounds");
                transform.position += new Vector3(-0.1f,0,0);
            }
        }
    }

    private void SwitchLane() {
        // Switching Lanes
        if(Input.GetKeyDown(KeyCode.UpArrow) && currentLaneNumber > 0){
            currentLaneNumber -= 1;
            transform.position += new Vector3(0,1.6f,0);
        }
        if(Input.GetKeyDown(KeyCode.DownArrow) && currentLaneNumber < laneNumber - 1){
            currentLaneNumber += 1;
            transform.position += new Vector3(0,-1.6f,0);
        }

    }

    public void SetPowerUp(Powerup flag) {
        hasPowerUp = true;

        switch (flag) {
            case Powerup.None:
                hasPowerUp = false;
                break;
            case Powerup.Revolver:
                revolver.SetActive(true);
                break;
            case Powerup.Shotgun:
                shotgun.SetActive(true);
                break;

        }
        
        animator.SetBool("hasPowerup", hasPowerUp);
    }

    private void UsePowerUp() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            if (revolver.activeSelf) {
                revolver.GetComponent<PowerUp_Revolver>().fire();
                StartCoroutine("RevolverDeactivate");
            } else if (shotgun.activeSelf) {
                shotgun.GetComponent<PowerUp_Shotgun>().fire();
                StartCoroutine("ShotgunDeactivate");
            }
        }
    }

    public void AddScore(int increment) {
        score += increment;
        if (score > highScore) {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", score);
        }
    }

    public void ResetHighScore()
    {
        highScore = 0;
        PlayerPrefs.DeleteKey("HighScore");
    }

    private IEnumerator RevolverDeactivate() {
        yield return new WaitForSeconds(deactivateDelay);
        revolver.GetComponent<PowerUp_Revolver>().isFired = false;
        revolver.SetActive(false);
        SetPowerUp(Powerup.None);
    }

    private IEnumerator ShotgunDeactivate() {
        yield return new WaitForSeconds(deactivateDelay);
        shotgun.GetComponent<PowerUp_Shotgun>().isFired = false;
        shotgun.SetActive(false);
        SetPowerUp(Powerup.None);
    }
}
