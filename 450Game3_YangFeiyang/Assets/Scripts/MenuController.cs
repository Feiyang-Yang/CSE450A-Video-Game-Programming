using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{   
    //Outlets
    public GameObject mainMenu;
    public GameObject optionsMenu;
    public GameObject levelMenu;

    public static MenuController instance;

    // Methods
    void Awake(){
        instance = this;
        Hide();
    }

    void SwitchMenu(GameObject someMenu){
        // Turn off all menus
        mainMenu.SetActive(false);
        optionsMenu.SetActive(false);
        levelMenu.SetActive(false);

        // Turn on requested menu
        someMenu.SetActive(true);
    }

    public void ShowMainMenu(){
        SwitchMenu(mainMenu);
    }

    public void ShowOptionsMenu(){
        SwitchMenu(optionsMenu);
    }

    public void ShowLevelMenu(){
        SwitchMenu(levelMenu);
    }

    public void Show(){
        ShowMainMenu();
        gameObject.SetActive(true);
        Time.timeScale = 0;
        PlayerController.instance.isPaused = true;
    }

    public void Hide(){
        gameObject.SetActive(false);
        Time.timeScale = 1;
        if(PlayerController.instance != null){
            PlayerController.instance.isPaused = false;
        }
    }

    public void ResetScore(){
        PlayerPrefs.DeleteKey("Score");
        PlayerController.instance.score = 0;
    }

    public void LoadLevel(){
        // SceneManager.LoadScene("SampleScene");
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
