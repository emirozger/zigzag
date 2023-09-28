
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public TMP_Text zigzagText, tapToStartText, developerText,highScoreText,scoreText;
    public Image highScoreImg;
    public bool isGameStarted = false,isDead=false;
    [SerializeField] [Range(0, 2f)] float LerpTime;
    [SerializeField] Color[] myColors;
    int colorIndex = 0;
    float t = 0f;
    public int length,score;
    public GameObject playerObj,deadCanvas;
    

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        highScoreText.text = PlayerPrefs.GetFloat("HighScore", 0).ToString();
        length = myColors.Length;
        scoreText.gameObject.SetActive(false);
        zigzagText.gameObject.SetActive(true);
        tapToStartText.gameObject.SetActive(true);
        developerText.gameObject.SetActive(true);
        Time.timeScale = 1;
    }
    public void StartGame()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }
    public void TouchToStartGame()
    {
        isGameStarted = true;
        zigzagText.gameObject.SetActive(false);
        tapToStartText.gameObject.SetActive(false);
        developerText.gameObject.SetActive(false);
        scoreText.gameObject.SetActive(true);
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;

    }
  
    public void SaveGame()
    {
        if (score > PlayerPrefs.GetFloat("HighScore", 0))
        {
            PlayerPrefs.SetFloat("HighScore", score);
            highScoreText.text = score.ToString();
        }
    }
    void Dead()
    {
        if (playerObj.gameObject.transform.position.y < .3f)
        {
            isDead = true;
            isGameStarted = false;
            if (isDead)
            {
                playerObj.GetComponent<BallController>().enabled = false;
                playerObj.GetComponent<Rigidbody>().isKinematic = true;
                deadCanvas.SetActive(true);
                if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    RestartGame();
                }
    
            }
        }
    }
    void Update()
    {
        zigzagText.color = Color.Lerp(zigzagText.color, myColors[colorIndex], LerpTime * Time.deltaTime);
        t = Mathf.Lerp(t, 1f, LerpTime * Time.deltaTime);
        if (t > .9f)
        {
            t = 0f;
            colorIndex++;
            colorIndex = (colorIndex >= myColors.Length) ? 0 : colorIndex;
        }
        if (!isGameStarted && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            TouchToStartGame();
            playerObj.GetComponent<BallController>().enabled = true;
        }
        SaveGame();
        Dead();

    }
}
