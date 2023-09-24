using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{

    public List<GameObject> objectToDesactivate;
    public GameObject lorePanel;

    public void ShowLorePanel()
    {
        foreach (var obj in objectToDesactivate)
        {
            obj.SetActive(false);
        }

        lorePanel.SetActive(true);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
