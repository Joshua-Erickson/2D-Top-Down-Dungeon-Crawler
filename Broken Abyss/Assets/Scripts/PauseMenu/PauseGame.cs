using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Menu;
    void Start()
    {
        // Ensure the game starts unpaused
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if(Menu.activeSelf) {
                // Resume the game
                Menu.SetActive(false);
                Time.timeScale = 1;
            } else {
                // Pause the game
                Menu.SetActive(true);
                Time.timeScale = 0;
            }
        }

    }
}
