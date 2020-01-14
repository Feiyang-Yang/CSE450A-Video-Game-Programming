using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
	public static MenuController instance;

	private bool showing = false;

    void Awake()
	{
		instance = this;
		Hide();
	}

    public void Show()
	{
		gameObject.SetActive(true);
		showing = true;
        Time.timeScale = 0;
        PlayerController.instance.isPaused = true;
	}

	public void Hide()
	{
		gameObject.SetActive(false);
		showing = false;
        Time.timeScale = 1;
        if(PlayerController.instance != null)
        {
            PlayerController.instance.isPaused = false;
        }
	}

	public void ToggleMenu()
	{
        if(showing)
		{
			Hide();
		}
        else
		{
			Show();
		}
	}

}
