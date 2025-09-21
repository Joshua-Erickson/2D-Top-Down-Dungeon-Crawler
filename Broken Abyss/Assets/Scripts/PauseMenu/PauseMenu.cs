using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseMenu : MonoBehaviour
{
   public GameObject Menu;

    public void Resume()
    {
        Menu.SetActive(false);
        Time.timeScale = 1;

    }

    public void QuitBtn()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
    }
}
