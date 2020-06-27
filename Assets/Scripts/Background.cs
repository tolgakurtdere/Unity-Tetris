using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    public static int height, width; //height and width of game area
    public static Transform[,] gameArea; //2d array represents game area
    void Start()
    {
        height = Mathf.RoundToInt(transform.localScale.y);
        width = Mathf.RoundToInt(transform.localScale.x);
        gameArea = new Transform[height+2, width];
    }

}
