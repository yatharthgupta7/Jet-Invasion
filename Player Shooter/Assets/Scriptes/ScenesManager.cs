using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class ScenesManager : MonoBehaviour
{
    [SerializeField] Text coins;
    [SerializeField] Text highScore;
    [SerializeField] GameObject loadingScreen;
    [SerializeField] Slider slider;
    [SerializeField] Text progressText;
    int coin;
    float score;
    UnityAds ads;

    public void Quit()
    {
        Application.Quit();
    }
    public void ToGame()
    {
        StartCoroutine(LoadAsynchronously("Game"));
        Time.timeScale = 1;
    }
    IEnumerator LoadAsynchronously(string scene)
    {
        loadingScreen.SetActive(true);
        AsyncOperation operation = SceneManager.LoadSceneAsync(scene);
        while (!operation.isDone)
        {
            float progress = (operation.progress / .9f);
            slider.value = progress;
            progressText.text = ((int)progress * 100f) + " %";
            yield return null;
        }
    }


    void Start()
    {
        score = PlayerPrefs.GetFloat("HighScore");
        coin = PlayerPrefs.GetInt("Coins");
        coins.text = ": " + coin;
        highScore.text = "HighScore : " + (int)score;
        ads.GetComponentInParent<UnityAds>();
        ads.ShowBanner();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
