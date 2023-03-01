using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class IslandCreator : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject prefab;

    int size = 50;
    public float seed1 = 0;
    public float seed2 = 0;
    public float space;
    public float perlinStrength = 50;
    public float scale;
    public Vector3 startpos1;
    public Vector3 startpos2;
    private GameObject[,] objects1;
    private GameObject[,] objects2;
    void Start()
    {
        objects1 = new GameObject[size, size];
        objects2 = new GameObject[size, size];
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                Vector3 pos1 = new Vector3(startpos1.x + i * space, startpos1.y, startpos1.z + j * space);
                Vector3 pos2 = new Vector3(startpos2.x + i * space, startpos2.y, startpos2.z + j * space);
                GameObject sphere1 = Instantiate(prefab, pos1, Quaternion.identity);
                objects1[i, j] = sphere1;
                GameObject sphere2 = Instantiate(prefab, pos2, Quaternion.identity);
                objects2[i, j] = sphere2;

            }
        }
        GenerateTerrain();
    }
    void GenerateTerrain() 
    {
        
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                objects1[i, j].transform.position = new Vector3(objects1[i, j].transform.position.x,startpos1.y, objects1[i, j].transform.position.z);
                objects1[i, j].transform.position = addPerlin(objects1[i, j].transform.position, i, j, seed1);
                objects2[i, j].transform.position = new Vector3(objects2[i, j].transform.position.x, startpos2.y, objects2[i, j].transform.position.z);
                objects2[i, j].transform.position = addPerlin(objects2[i, j].transform.position, i, j, seed2);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow)) 
        {
            seed1 += 0.1f;
            seed2 += 0.1f;
            GenerateTerrain();
        }
    }


    Vector3 addPerlin(Vector3 vec, float i, float j, float seed)
    {
        float s = (float)size;
        vec.y += perlinStrength * Mathf.PerlinNoise(i/s * scale + seed , j/s * scale);
        return vec;
    }
}
