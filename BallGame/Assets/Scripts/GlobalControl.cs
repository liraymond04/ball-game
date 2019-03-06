using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalControl : MonoBehaviour 
{

    public static GlobalControl Instance;

    public int red;
    public int blue;
    public int green;
    public int yellow;

    void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);

            Instance = this;
        }
        else if(Instance != this)
        {
            Destroy(gameObject);
        }
    }

}
