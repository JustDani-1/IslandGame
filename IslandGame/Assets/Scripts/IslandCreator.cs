using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class IslandCreator : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject prefab;

    int size = 80;
    float seed = 0;
    public float space;
    public float perlinStrength = 50;
    public float scale;
    public Vector3 startpos;
    private GameObject[,] objects;
    void Start()
    {
        objects = new GameObject[size, size];
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                Vector3 pos = new Vector3(startpos.x + i * space, startpos.y, startpos.z + j * space);
                GameObject sphere = Instantiate(prefab, pos, Quaternion.identity);
                objects[i, j] = sphere;
                
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
                objects[i, j].transform.position = new Vector3(objects[i, j].transform.position.x,0, objects[i, j].transform.position.z);
                objects[i, j].transform.position = addPerlin(objects[i, j].transform.position, i, j);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow)) 
        {
            seed += 0.1f;
            GenerateTerrain();
        }
    }


    Vector3 addPerlin(Vector3 vec, float i, float j)
    {
        float s = (float)size;
        vec.y += perlinStrength * Mathf.PerlinNoise(i/s * scale + seed , j/s * scale);
        return vec;
    }
}
