using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager manager;

    [SerializeField]
    private int coins = 0;

    [SerializeField]
    private int score = 0;

    private GameObject score_go;
    private TextMeshProUGUI score_text;

    private void Awake()
    {
        // Can only be one class (Singleton)
        if (manager == null)
        {
            DontDestroyOnLoad(gameObject);
            manager = this;
        }
        else if (manager != this)
        {
            Destroy(gameObject);
        }

        score_go = GameObject.FindWithTag("Score");
        score_text = score_go.GetComponent<TextMeshProUGUI>();

        
    }

    private void Start()
    {
    }

    public void setScore(int score_)
    {
        score = score_;
        updateText();
    }

    public int getScore()
    {
        return score;
    }

    private void updateText()
    {
        score_text.text = score.ToString();
    }

    public void setCoins(int coins_)
    {
        coins = coins_;
    }

    public int getCoins()
    {
        return coins;
    }

    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");

        PlayerData data = new PlayerData();
        data.coins = coins;

        bf.Serialize(file, data);
        file.Close();
    }

    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
            //Casting so that the binary formatter knows what type of object it's pulling
            PlayerData data = (PlayerData)bf.Deserialize(file);
            file.Close();

            coins = data.coins;
        }
    }
}

[Serializable]
class PlayerData
{
    public int coins;
}
