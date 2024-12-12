using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DetectionZone : MonoBehaviour
{
    public List<Collider2D> detectedObjs = new List<Collider2D>();
    public List<Collider2D> OrcObjs = new List<Collider2D>();
    public Collider2D col;

    // Called on object entering collider
    // If game object has player or enemy tag, add to associated list
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            detectedObjs.Add(collider);
        }
        else if (collider.gameObject.tag == "Enemy")
        {
            OrcObjs.Add(collider);
        }
    }
    // Called when object exits collider
    // If collider is player or enemy, remove from associated list
    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            detectedObjs.Remove(collider);
        }
        else if (collider.gameObject.tag == "Enemy")
        {
            OrcObjs.Remove(collider);
        }
    }
    // Detects if any player objects are inside of the collider
    public bool isNotEmpty()
    {
        return detectedObjs.Any();
    }
    // Detects if any enemy objects are inside of the collider
    public bool isOrcs()
    {
        return OrcObjs.Any();
    }
}
