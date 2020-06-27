using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnControl : MonoBehaviour
{
    public GameObject[] objects;
    public static int nextBlockIndex;
    void Start()
    {
        Instantiate(objects[Random.Range(0, objects.Length)], transform.position, Quaternion.identity); //create new block at the beginning
        nextBlockIndex = Random.Range(0, objects.Length); //choose random number for next block
    }

    public void SpawnObject() //firstly create new object, then choose random number for next block
    {
        Instantiate(objects[nextBlockIndex], transform.position, Quaternion.identity);
        nextBlockIndex = Random.Range(0, objects.Length);
    }
}
