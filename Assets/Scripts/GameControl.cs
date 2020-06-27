using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameControl : MonoBehaviour
{
    public Text gameOverText,scoreText;
    public static int score;
    public Image nextBlockImage;
    public Sprite[] blockImages;
    void Start()
    {
        score = 0;
        scoreText.text = "Score\n" + score;
        nextBlockImage.color = Color.white;
    }

    void Update()
    {
        if (FindObjectOfType<BlockControl>().isOver)
        {
            gameOverText.text = "Press 'R' to restart";
        }

        scoreText.text = "Score\n" + score;

        nextBlockImage.sprite = blockImages[SpawnControl.nextBlockIndex]; //show what next block is
    }
}
