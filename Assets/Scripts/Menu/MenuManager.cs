using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    
	void Start ()
    {
		
	}
	
	void Update ()
    {
		
	}
    public void PlayButton()
    {
        SceneManager.LoadScene("Prototype");
    }
    public void MenuButton()
    {
        SceneManager.LoadScene("Menu");
    }
    public void ExitButton()
    {
        Application.Quit();
    }
}
