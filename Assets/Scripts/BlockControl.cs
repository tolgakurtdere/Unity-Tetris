using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BlockControl : MonoBehaviour
{
    float fallTime = 0;
    float fallTimeControl = 0.2f; //higher means hard, lower means easy
    public Vector3 rotationPoint; //specific to every block
    public bool isOver = false; //true if game is over

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && isOver)
        {
            SceneManager.LoadScene("GameScene");
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) && !isOver) //rotation
        {
            transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), 90);
            if (!inBorders()) transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), -90);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) && !isOver) //move left
        {
            transform.position += new Vector3(-1, 0, 0);
            if(!inBorders()) transform.position -= new Vector3(-1, 0, 0);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) && !isOver) //move right
        {
            transform.position += new Vector3(1, 0, 0);
            if (!inBorders()) transform.position -= new Vector3(1, 0, 0);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) && !isOver) //fall down faster
        {
            transform.position += new Vector3(0, -1, 0);
            if (!inBorders()) transform.position -= new Vector3(0, -1, 0);
        }

        fallTime += Time.deltaTime;
        if(fallTime > fallTimeControl && !isOver) //fall down automatically
        {
            transform.position += new Vector3(0, -1, 0);
            if (!inBorders())
            {
                transform.position -= new Vector3(0, -1, 0);
                updateGameArea();
                this.enabled = false; //to avoid move when block stop
                FindObjectOfType<SpawnControl>().SpawnObject();
            }
            fallTime = 0;
        }
        scoreUpCheck();
        levelCheck();
        gameOver();
    }

    bool inBorders() //check if squares are inside the game area
    {
        foreach (Transform child in transform)
        {
            int tempX = Mathf.RoundToInt(child.transform.position.x);
            int tempY = Mathf.RoundToInt(child.transform.position.y);

            if (tempX < 0 || tempX >= Background.width || tempY < 0 || tempY >= Background.height) return false;

            if (Background.gameArea[tempY, tempX] != null) return false;
        }
        return true;
    }

    void updateGameArea() //update gameArea array
    {
        foreach (Transform child in transform)
        {
            int tempX = Mathf.RoundToInt(child.transform.position.x);
            int tempY = Mathf.RoundToInt(child.transform.position.y);

            Background.gameArea[tempY,tempX] = child;
        }
    }

    void scoreUpCheck() //check for score
    {
        bool isLine = false;
        for(int i = 0; i < Background.height; i++)
        {
            for(int j = 0; j < Background.width; j++)
            {
                if (Background.gameArea[i, j] == null)
                {
                    isLine = false;
                    break;
                }
                isLine = true;
            }
            if (isLine)
            {
                GameControl.score++; //score up
                deleteLine(i);
                moveDown(i);
            }

        }
    }

    void deleteLine(int i) //delete the i. row of gameArea
    {
        for (int j = 0; j < Background.width; j++)
        {
            Destroy(Background.gameArea[i, j].gameObject);
            Background.gameArea[i, j] = null;
        }
    }

    void moveDown(int k) //after deleting, move blocks down
    {
        for (int i = k+1; i < Background.height; i++)
        {
            for (int j = 0; j < Background.width; j++)
            {
                if(Background.gameArea[i,j] != null)
                {
                    Background.gameArea[i - 1, j] = Background.gameArea[i, j]; //fix gameArea
                    Background.gameArea[i, j] = null;
                    Background.gameArea[i - 1, j].transform.position -= new Vector3(0, 1, 0); //move down physically
                }
            }
        }
    }

    void gameOver() //check if game is over
    {
        if (Background.gameArea[19,5] != null) //if the SpawnPoint is not null
        {
            isOver = true;
        }
    }

    void levelCheck()
    {
        if (GameControl.score >= 5 && GameControl.score < 25) GameControl.level = 2;
        if (GameControl.score >= 25) GameControl.level = 3;

        if (GameControl.level == 2)
        {
            fallTimeControl = 0.15f;
        }
        else if(GameControl.level == 3)
        {
            fallTimeControl = 0.1f;
        }
    }
}
