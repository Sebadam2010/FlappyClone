using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager manager;

    private int score = 0;

    private void Awake()
    {
        // Can only be one class (Singleton kind of)
        if (manager == null)
        {
            DontDestroyOnLoad(gameObject);
            manager = this;
        }
        else if (manager != this)
        {
            Destroy(gameObject);
        }

        
    }

    private void Start()
    {
        
    }
}
