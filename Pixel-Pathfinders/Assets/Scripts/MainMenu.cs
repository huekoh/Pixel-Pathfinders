using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [Header("Menu Buttons")]
    [SerializeField] private Button newGameButton;
    [SerializeField] private Button continueGameButton;

    private void Start()
    {
        if (!DataPersistenceManager.instance.HasGameData())
        {
            continueGameButton.interactable = false;
        }
    }

    public void PlayGame()
    {
        DisableMenuButtons();
        DataPersistenceManager.instance.NewGame();
        SceneManager.LoadSceneAsync("House");
    }

    public void GoToOptionsMenu()
    {
        SceneManager.LoadScene("Options");
    }

    public void ContinueGame()
    {
        DisableMenuButtons();
        DataPersistenceManager.instance.SaveGame();
        SceneManager.LoadSceneAsync("House");
    }

    public void GoToMainMenu() {
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void DisableMenuButtons()
    {
        newGameButton.interactable = false;
        continueGameButton.interactable = false;
    }
}