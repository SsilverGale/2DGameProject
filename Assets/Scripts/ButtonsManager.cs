using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsManager : MonoBehaviour
{
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject creditsMenu;

    //play button
    public void Play()
    {
        Debug.Log("Play pressed");
        SceneManager.LoadScene("Test Level");
    }

    //credits button
    public void Credits()
    {
        Debug.Log("Credits pressed");
        mainMenu.SetActive(false);
        creditsMenu.SetActive(true);
    }

    //quit button
    public void Quit()
    {
        Debug.Log("Quit pressed");
        Application.Quit();
    }

    //back button for the credits menu
    public void Back()
    {
        Debug.Log("Back pressed");
        mainMenu.SetActive(true);
        creditsMenu.SetActive(false);
    }
}
