using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KeepObject : MonoBehaviour
{
    //public Inventory playerInventory;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if(SceneManager.GetActiveScene().name != "Test-Game")
        {
            //for(int i = 0; i<6; i++)
            //{
            //    playerInventory.InventorySlots[i] = "0";
            //}
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }
}
