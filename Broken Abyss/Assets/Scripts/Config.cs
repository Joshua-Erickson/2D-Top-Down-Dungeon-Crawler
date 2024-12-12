using System;
using System.IO;
using UnityEngine;

[CreateAssetMenu(fileName = "Config", menuName = "Config")]
public class Config : ScriptableObject
{
    public string apiUrl; // If you have an API URL to configure
    public string apiKey;

    // Method to load the API_KEY from the .env file
    public void LoadApiKeyFromEnv()
    {
        string envFilePath = Path.Combine(Application.dataPath, ".env");

        if (File.Exists(envFilePath))
        {
            string[] lines = File.ReadAllLines(envFilePath);

            foreach (string line in lines)
            {
                if (line.StartsWith("API_KEY="))
                {
                    apiKey = line.Split('=')[1].Trim();
                    Debug.Log("API Key loaded from .env: " + apiKey);
                }
            }
        }
        else
        {
            Debug.LogError(".env file not found at: " + envFilePath);
        }
    }
}
