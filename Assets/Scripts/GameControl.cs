using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameControl : MonoBehaviour
{
    public Text gameOverText,scoreText,highScoreText,levelText;
    public static int score,level;
    int highScore = 0;
    public Image nextBlockImage;
    public Sprite[] blockImages;
    void Start()
    {
        score = 0;
        level = 1;
        highScore = PlayerPrefs.GetInt("highScore"); //get highscore
        scoreText.text = "Score\n" + score;
        nextBlockImage.color = Color.white;
    }

    void Update()
    {
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("highScore", highScore); //save highscore
        }

        if (FindObjectOfType<BlockControl>().isOver)
        {
            gameOverText.text = "Press 'R' to restart";
        }

        scoreText.text = "Score\n" + score;
        highScoreText.text = "max: " + highScore;
        levelText.text = "level: " + level;

        nextBlockImage.sprite = blockImages[SpawnControl.nextBlockIndex]; //show what next block is
    }
}
