using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIScript : MonoBehaviour
{

    public GameObject main_menu;
    public GameObject options_menu;

    private GameObject post_game_menu;
    private GameObject score_manager;
    private ScoreManager score_manager_script;

    private GameObject graphy_go;

    private void Awake()
    {
        
    }

    private void Start()
    {
        graphy_go = GraphyScript.graphy_script.gameObject;

        if ((SceneManager.GetActiveScene().name == "MainMenu"))
        {
            if(GraphyScript.graphy_script.show_active)
            {
                graphy_go.SetActive(true);
            }
            else
            {
                graphy_go.SetActive(false);
            }
            
        }
        else
        {
            post_game_menu = GameObject.FindWithTag("PostGameMenu");
            post_game_menu.SetActive(false);
        }

        

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

    public void switchScene()
    {

        if (SceneManager.GetActiveScene().name == "MainMenu")
            SceneManager.LoadScene("GameScene");
        else
            SceneManager.LoadScene("MainMenu");

    }

    //List of screens: MAINMENU, OPTIONS
    public void switchScreen(string screen)
    {
        if (screen == "OPTIONS")
        {
            main_menu.SetActive(false);
            options_menu.SetActive(true);
        }

        else if (screen == "MAINMENU")
        {
            main_menu.SetActive(true);
            options_menu.SetActive(false);
        }
    }

    public void toggleFPS()
    {
        if (graphy_go.activeSelf)
        {
            graphy_go.SetActive(false);
            GraphyScript.graphy_script.show_active = false;
        }
        else
        {
            graphy_go.SetActive(true);
            GraphyScript.graphy_script.show_active = true;
        }
    }

}
