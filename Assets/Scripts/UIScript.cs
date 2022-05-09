using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIScript : MonoBehaviour
{

    private GameObject post_game_menu;
    private GameObject score_manager;
    private ScoreManager score_manager_script;

    private void Awake()
    {
        post_game_menu = GameObject.FindWithTag("PostGameMenu");
        post_game_menu.SetActive(false);

        score_manager = GameObject.FindWithTag("ScoreManager");
        score_manager_script = score_manager.GetComponent<ScoreManager>();

        // This is here because of RestartScene() function, need to init the gameObjects somehow after scene is loaded again
        score_manager_script.initScoreManager();
    }

    public void PostGameMenu()
    {
        post_game_menu.SetActive(true);
    }

    public void RestartScene()
    {
        
        SceneManager.LoadScene("GameScene");
        
    }
}
