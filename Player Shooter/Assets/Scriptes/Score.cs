using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    float m_score;
    [SerializeField] Text scoreText;
    [SerializeField] Text coinsText;
    int coins=0;
    //[SerializeField] DeathMenu deathMenu;

    int difficultyLevel = 1;
    int maxDifficultyLevel = 10;
    int scoreToNextLevel = 10;
    GameObject[] ol;
    ObstacleLogic[] obstacles;
    bool isDead = false;
    void Start()
    {
        ol = GameObject.FindGameObjectsWithTag("Enemy");
        for(int x = 0; x < ol.Length; x++)
        {
            obstacles[x] = ol[x].GetComponent<ObstacleLogic>();
        }
    }
    void Update()
    {
        if (isDead)
        {
            return;
        }
        if (m_score >= scoreToNextLevel)
        {
            LevelUp();
        }
        m_score += Time.deltaTime * difficultyLevel;
        scoreText.text = ((int)m_score).ToString();
    }

    void LevelUp()
    {
        if (difficultyLevel == maxDifficultyLevel)
        {
            return;
        }
        scoreToNextLevel *= 2;
        difficultyLevel++;
        foreach(ObstacleLogic o in obstacles)
        {
            //o.SetSpeed(difficultyLevel);
            Debug.Log(obstacles.Length);
        }
    }

    public void Death()
    {
        isDead = true;
        if (PlayerPrefs.GetFloat("HighScore") < m_score)
        {
            PlayerPrefs.SetFloat("HighScore", m_score);
        }
        //deathMenu.ToggleEndMenu(m_score);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="Coin")
        {
            coins++;
            coinsText.text = "Coins : " + coins;
            Destroy(collision.gameObject);
        }
    }
}
