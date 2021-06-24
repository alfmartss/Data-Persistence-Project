
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public Text BestScoreText;
    public GameObject GameOverText;
    
    private bool m_Started = false;
    private int m_Points;
    
    private bool m_GameOver = false;

    
    // Start is called before the first frame update
    void Start()
    {
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);

        int[] pointCountArray = new[] { 1, 1, 2, 2, 5, 5 };
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }

        AddPoint(0);

        LoadHighScore();
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = UnityEngine.Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        string playername = GetPlayerName();

        ScoreText.text = $"Score : {m_Points} : ({playername})";
    }

    public void GameOver()
    {
        m_GameOver = true;
        UpdateHighScore();
        GameOverText.SetActive(true);
    }

    private void LoadHighScore()
    {
        
        if (GameManager.Instance != null)
        {           
            HighScoreInfo info = GameManager.Instance.highScore;           
            UpdateHighScoreUI(info.points, info.playerName);
        }
    }

    private void UpdateHighScore()
    {        
        if (GameManager.Instance!=null)
        {
            HighScoreInfo info = GameManager.Instance.highScore;
            if (m_Points > info.points)
            {
                info.points = m_Points;
                info.playerName = GetPlayerName();
                UpdateHighScoreUI(info.points, info.playerName);
                GameManager.Instance.SavePersistentInfo();
            }
        }
    }

    private void UpdateHighScoreUI(int points, string player)
    {
        
        BestScoreText.text = $"BestScore : {points} : ({player})";
    }

    private string GetPlayerName()
    {
        return GameManager.Instance != null ? GameManager.Instance.playerName : "";
    }


}
