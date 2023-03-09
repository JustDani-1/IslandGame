using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class IslandCreator : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] prefabs;

    public int size;
    public int islandAmount;
    public float seed = 0;
    public float scattering;
    //public float seed2 = 0;
    float space;
    public float perlinStrength;
    public float scale;
    public Vector3 startpos1;

    private GameObject[,] objects1;

    void Start()
    {
        space = size / islandAmount;
        objects1 = new GameObject[islandAmount, islandAmount];

        for (int i = 0; i < islandAmount; i++)
        {
            for (int j = 0; j < islandAmount; j++)
            {
                Vector3 pos1 = new Vector3(startpos1.x + i * space, startpos1.y, startpos1.z + j * space);

                GameObject sphere1 = Instantiate(getIsland(i, j, seed), pos1, Quaternion.identity);
                objects1[i, j] = sphere1;


            }
        }
        GenerateTerrain();
    }
    void GenerateTerrain()
    {

        for (int i = 0; i < islandAmount; i++)
        {
            for (int j = 0; j < islandAmount; j++)
            {
                objects1[i, j].transform.position = new Vector3(objects1[i, j].transform.position.x, startpos1.y, objects1[i, j].transform.position.z);
                objects1[i, j].transform.position = addPerlin(objects1[i, j].transform.position, i, j, seed);
                objects1[i, j].transform.position = addScattering(objects1[i, j].transform.position, i, j, seed, scattering);
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


    Vector3 addPerlin(Vector3 vec, float i, float j, float seed)
    { 
        float s = (float)islandAmount;
        vec.y += perlinStrength * Mathf.PerlinNoise(i / s * scale + seed, j / s * scale);
        return vec;
    }

    GameObject getIsland(float i, float j, float seed)
    {
        float s = (float)islandAmount;
        float x = doublePerlin(i / s * 3 + seed + 50, j / s * 3);
        int index = (int)(x * prefabs.Length);
        index = Math.Clamp(index, 0, prefabs.Length - 1);

        return prefabs[index];
    }

    Vector3 addScattering(Vector3 pos, float i, float j, float seed, float r)
        {
        float s = (float)islandAmount;
        float deltax =  doublePerlin(i / s * scale + seed + 10, j / s * scale);
        float deltaz = doublePerlin(i / s * scale + seed + 20, j / s * scale);
        deltax -= 0.5f;     //<0 if perlin <0.5, >0 if perlin > 0.5
        deltaz -= 0.5f;
        deltax = 2 * deltax * r;    //range between 0 and +/-r
        deltaz = 2 * deltaz * r;
        pos.x += deltax;
        pos.z += deltaz;
        return pos;

    }

    float doublePerlin(float i, float j)
    {
        //this method return seemingly random numbers by using the PerlinNoise twice
        float strength = 30;
        float s1 = strength * Mathf.PerlinNoise(i, j);
        float s2 = strength * Mathf.PerlinNoise(j, i);
        return Mathf.PerlinNoise(s1, s2);
    }
}
