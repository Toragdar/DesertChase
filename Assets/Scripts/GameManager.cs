using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;


public class GameManager : MonoBehaviour
{
    private int scores;
    private int scoresOffset = 0;
    private int hpOffset = 10;
    private SpawnManager spawnManagerScript;

    public int hp;
    public bool isGameActive;
    public Button restartButton;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI hpText;
    public TextMeshProUGUI gameoverText;
    public GameObject titleScreen;
    public GameObject cursor;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        spawnManagerScript = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckHP();
    }
    public void StartGame()
    {        
        spawnManagerScript.PlayerRespawn();
        isGameActive = true;
        scores = scoresOffset;
        UpdateScores(0);
        OffsetHP(hpOffset);
        titleScreen.gameObject.SetActive(false);
        scoreText.gameObject.SetActive(true);
        hpText.gameObject.SetActive(true);
        cursor.gameObject.SetActive(true);
        player = GameObject.FindGameObjectWithTag("Player");
    }
    public void RestartGame()
    {        
        spawnManagerScript.PlayerRespawn();
        isGameActive = true;
        scores = scoresOffset;
        UpdateScores(0);
        OffsetHP(hpOffset);
        restartButton.gameObject.SetActive(false);
        gameoverText.gameObject.SetActive(false);
        scoreText.gameObject.SetActive(true);
        hpText.gameObject.SetActive(true);
        cursor.gameObject.SetActive(true);
        player = GameObject.FindGameObjectWithTag("Player");
    }
    public void GameOver()
    {
        player.gameObject.GetComponent<PlayerController>().IsDead();
        restartButton.gameObject.SetActive(true);
        gameoverText.gameObject.SetActive(true);
        cursor.gameObject.SetActive(false);
        isGameActive = false;
    }
    public void UpdateScores(int scoreToAdd)
    {
        scores += scoreToAdd;
        scoreText.text = "Score: " + scores;
    }    
    public void UpdateHP(int hpToSubtract)
    {
        hp -= hpToSubtract;
        hpText.text = "HP: " + hp;
    }
    private void OffsetHP(int hpOffset)
    {
        hp = hpOffset;
        hpText.text = "HP: " + hp;
    }
    void CheckHP()
    {
        if (isGameActive && hp == 0)
        {
            GameOver();
        }
    }
}
