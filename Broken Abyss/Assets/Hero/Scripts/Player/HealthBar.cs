using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Image healthBarForeground;
    
    public void UpdateHealthBar(PlayerHealth playerHealth)
    {
        healthBarForeground.fillAmount = playerHealth.RemainingHealthPercent;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
