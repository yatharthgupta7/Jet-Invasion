using System.Collections;
using System.Collections.Generic;
using UnityEngine;using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    #region Singleton
    public static GameManager Instance;
    private void Awake()
    {
        if(Instance==null)
        {
            Instance = this;
        }
    }
    #endregion
    float m_score;
    [SerializeField] Text scoreText;
    public Text coinsText;
    [SerializeField] Text deathScoreText;
    public  Text deathCoinsText;
    [SerializeField] BackgroundLogic bg;
    [SerializeField] ObjectSpawner[] os;
    [SerializeField] GameObject Enemy;
    [SerializeField] Transform[] enemyTransform;
    [SerializeField] GameObject[] Cointile;
    [SerializeField] Transform coinSpawnPosition;
    [SerializeField] GameObject DeathMenu;
    [SerializeField] Slider healthBar;
    [SerializeField] float healthBarChangeTime = 0.5f;
    [SerializeField] PlayerController player;
    [SerializeField] GameObject gun;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] AudioClip coin;
    public bool isMuted;
    public EnemySpawner enemySpawner;
    float timer;
    float gunTimer;
    public int coins = 0;
    //[SerializeField] DeathMenu deathMenu;
    float EnemySpawnTime=20f;
    int difficultyLevel = 1;
    int maxDifficultyLevel = 10;
    int scoreToNextLevel = 10;
    AudioSource background;
    GameObject[] ol;
    ObstacleLogic[] obstacles;
    float obstacleSpeed = 10f;
    bool isDead = false;
    UnityAds ads;
    void Start()
    {
        ol = GameObject.FindGameObjectsWithTag("Enemy");
        background = GetComponent<AudioSource>();
        isMuted = PlayerPrefs.GetInt("MUTED")==1;
        ads = GetComponentInParent<UnityAds>();
        for (int x = 0; x < ol.Length; x++)
        {
            obstacles[x] = ol[x].GetComponent<ObstacleLogic>();
        }
    }

    public float SetSpeedObstacle()
    {
        obstacleSpeed += 1.2f;
        return obstacleSpeed;
    }
    void Update()
    {
        if (isDead)
        {
            return;
        }
        timer += Time.deltaTime;
        gunTimer += Time.deltaTime;
        if (timer >= 5.0)
        {
            GameObject ob = Instantiate(Cointile[Random.Range(0, Cointile.Length)], coinSpawnPosition.position + new Vector3(coins, 0), Quaternion.identity);
            coins += 25;
            timer = 0.0f;
        }
        if(gunTimer>=10.0f)
        {
            if (!player.gunEquiped)
            {
                Instantiate(gun, coinSpawnPosition.transform.position, Quaternion.identity);
            }
            gunTimer = 0.0f;
        }
        if (m_score >= scoreToNextLevel)
        {
            LevelUp();
            if(m_score<=30)
            {
                enemySpawner.SpawnOneEnemy();
            }
            else
            {
                enemySpawner.enemySpawn = true;
                enemySpawner.bossSpawn = false;
            }
            SetSpeedObstacle();
            os[0].LevelUp();
        }

        #region Boss/EnemySpawn
        if (m_score >= 200 && m_score<300&&enemySpawner.countBoss == 0)
        {
            enemySpawner.bossSpawn = true;
            enemySpawner.enemySpawn = false;
            enemySpawner.SpawnBoss();
        }
        else if (m_score > 300 && m_score < 500 && enemySpawner.countBoss <= 1)
        {
            enemySpawner.enemySpawn = true;
        }

        if (m_score >= 500 && m_score < 600 && enemySpawner.countBoss == 0)
        {
            enemySpawner.bossSpawn = true;
            enemySpawner.enemySpawn = false;
            enemySpawner.SpawnBoss();
        }
        else if (m_score > 600 && m_score < 850 && enemySpawner.countBoss <= 1)
        {
            enemySpawner.enemySpawn = true;
        }

        if (m_score >= 850 && m_score < 900 && enemySpawner.countBoss == 0)
        {
            enemySpawner.bossSpawn = true;
            enemySpawner.enemySpawn = false;
            enemySpawner.SpawnBoss();
        }
        else if (m_score > 900 && m_score < 1300 && enemySpawner.countBoss <= 1)
        {
            enemySpawner.enemySpawn = true;
        }

        if (m_score >= 1300 && m_score < 1400 && enemySpawner.countBoss == 0)
        {
            enemySpawner.bossSpawn = true;
            enemySpawner.enemySpawn = false;
            enemySpawner.SpawnBoss();
        }
        else if (m_score > 1400 && m_score < 1500 && enemySpawner.countBoss <= 1)
        {
            enemySpawner.enemySpawn = true;
        }

        if (m_score >= 1500 && m_score < 1600 && enemySpawner.countBoss == 0)
        {
            enemySpawner.bossSpawn = true;
            enemySpawner.enemySpawn = false;
            enemySpawner.SpawnBoss();
        }
        else if (m_score > 1600 && enemySpawner.countBoss <= 1)
        {
            enemySpawner.enemySpawn = true;
        }
        #endregion
        m_score += Time.deltaTime * difficultyLevel;
        scoreText.text = ((int)m_score).ToString();
        deathScoreText.text= ((int)m_score).ToString();
    }

    void LevelUp()
    {
        if (difficultyLevel == maxDifficultyLevel)
        {
            return;
        }
        scoreToNextLevel *= 2;
        difficultyLevel++;
        ObstacleLogic[] e = FindObjectsOfType<ObstacleLogic>();
        bg.SetSpeed(1.2f);
    }

    public void Death()
    {
        isDead = true;
        DeathMenu.SetActive(true);
        if (PlayerPrefs.GetFloat("HighScore") < m_score)
        {
            PlayerPrefs.SetFloat("HighScore", m_score);
        }
        int coin = PlayerPrefs.GetInt("Coins");
        PlayerPrefs.SetInt("Coins", coin + coins);
        background.Stop();
        //deathMenu.ToggleEndMenu(m_score);
    }

    public void OpenDeathMenu()
    {
        Time.timeScale = 0f;
        DeathMenu.SetActive(true);
        //ads.ShowBanner();
        //audioSource.Pause();
    }

    public void Play()
    {
        ads.ShowInterstitial("Game");
        //SceneManager.LoadScene("Game");
        //Time.timeScale = 1;
    }

    public void Menu()
    {
        ads.ShowInterstitial("Menu");
        //SceneManager.LoadScene("Menu");
    }

    public void Quit()
    {
        Application.Quit();
    }

    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Coin")
        {
            coins++;
            background.PlayOneShot(coin);
            coinsText.text = "Coins : " + coins;
            deathCoinsText.text = "Coins : " + coins;
            Destroy(collision.gameObject);
        }
    }*/
    public void ChangeHealthBar(int maxHealth, int currentHealth)
    {
        if (currentHealth < 0)
            return;

        if (currentHealth == 0)
        {
            Invoke("OpenDeathMenu", healthBarChangeTime);
        }
        float healthPct = currentHealth / (float)maxHealth;
        StartCoroutine(SmooothHealthbarChange(healthPct));
    }

    IEnumerator SmooothHealthbarChange(float newFloat)
    {
        float elapsed = 0f;
        float oldFloat = healthBar.value;
        while (elapsed <= healthBarChangeTime)
        {
            elapsed += Time.deltaTime;
            float cureentFillPct = Mathf.Lerp(oldFloat, newFloat, elapsed / healthBarChangeTime);
            healthBar.value = cureentFillPct;
            yield return null;
        }
    }

    public void PauseButton()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void Restart()
    {
        SceneManager.LoadScene("Game");
    }

    public void PauseMenuButton()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Mute()
    {
        isMuted = !isMuted;
        AudioListener.pause = isMuted;
        PlayerPrefs.SetInt("MUTED", isMuted ? 1: 0);
    }
}
