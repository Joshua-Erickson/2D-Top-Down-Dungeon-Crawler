using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GoldCounter : MonoBehaviour
{
    [SerializeField] public int goldCounter = 0;

    public TMP_Text goldText;
    
    // Start is called before the first frame update
    void Start()
    {
        goldText = GameObject.Find("HeroCanvas/GoldCounter/GoldText").GetComponent<TextMeshProUGUI>();

        if (goldText != null)
        {
            goldText.text = goldCounter.ToString();
        }
        else
        {
            Debug.Log("Can not find.");
        }
    }

    // Method to get the current amount of gold
    public int GetGold()
    {
        return goldCounter;
    }

    public void AddGold(int addAmount)
    {
        goldCounter += addAmount;
        UpdateGoldCounter();
    }

    public void RemoveGold(int removeAmount)
    {
        goldCounter -= removeAmount;

        if (goldCounter <= 0)
        {
            goldCounter = 0;
        }

        UpdateGoldCounter();
    }

    private void UpdateGoldCounter()
    {
        goldText.text = goldCounter.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
