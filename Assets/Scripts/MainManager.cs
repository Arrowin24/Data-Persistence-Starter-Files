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
    public Text BestText;
    public GameObject GameOverText;
    
    private bool m_Started = false;
    private int m_Points;
    private string m_name=Data.data.nameRead;
    private int m_bestScore = Data.data.bestScore;
    private string m_bestGamer = Data.data.bestGamer;
    private bool m_GameOver = false;

    
    // Start is called before the first frame update
    void Start()
    {
        AddBestText();
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
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
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
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
                SceneManager.LoadScene(0);
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_name} :{m_Points}";
    }

    void AddBestText()
    {
        BestText.text = "Best Score: "+m_bestGamer+" : "+m_bestScore;
    }

    void LoadScore() 
    {
        if (m_bestScore < m_Points)
        {
            Data.data.bestGamer = m_name;
            Data.data.bestScore = m_Points;
        }
    }

    public void GameOver()
    {
        m_GameOver = true;
        LoadScore();
        GameOverText.SetActive(true);
    }
}
