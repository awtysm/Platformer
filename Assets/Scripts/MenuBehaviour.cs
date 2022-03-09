using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuBehaviour : MonoBehaviour
{
	public bool IsPaused = false;
	public bool InInventory = false;
	public bool InOptions = false;
	public GameObject PauseUI;
	public GameObject InventoryUI;
	public GameObject OptionsUI;

	void Start()
	{
		Time.timeScale = 1f;
		Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false; 
	}
	public void StartGame(){SceneManager.LoadScene(1);}

    public void NextLevel()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
		Time.timeScale = 1f;
		Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false; 
	}

    public static void Restart(){SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);}

	public void MainMenu(){SceneManager.LoadScene(0);}

	public void QuitGame(){Application.Quit();}

    public void Link(){Application.OpenURL("https://example.com");}

    public void Pause()
        {
            if(IsPaused)
			{
				PauseUI.SetActive(false);
				Time.timeScale = 1f;
				Cursor.lockState = CursorLockMode.Locked;
            	Cursor.visible = false; 
			}
			else
			{
				PauseUI.SetActive(true);
				Time.timeScale = 0f;
				Cursor.lockState = CursorLockMode.None;
            	Cursor.visible = true; 
			}
			IsPaused = !IsPaused;
        }
	
	public void Options()
		{
			if(InOptions)
			{
				PauseUI.SetActive(true);
				OptionsUI.SetActive(false);
			}
			else
			{
				PauseUI.SetActive(false);
				OptionsUI.SetActive(true);
			}
			InOptions = !InOptions;
		}
	
	public void OpenInventory()
		{
			if(!IsPaused)
			{
				if(InInventory)
				{
					InventoryUI.SetActive(false);
					Time.timeScale = 1f;
					Cursor.lockState = CursorLockMode.Locked;
            		Cursor.visible = false;
				}
				else
				{
					InventoryUI.SetActive(true);
					Time.timeScale = 0f;
					Cursor.lockState = CursorLockMode.None;
            		Cursor.visible = true; 
				}
				InInventory = !InInventory;
			}
		}

	private void Update()
		{
			if(Input.GetKeyDown(KeyCode.Escape))
			{
				if(InInventory)
					OpenInventory();
				else
				{
					if(!InOptions){Pause();}
					else{Options();}
				}
			}

			//if(Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.JoystickButton0))
				//OpenInventory();
		}
}