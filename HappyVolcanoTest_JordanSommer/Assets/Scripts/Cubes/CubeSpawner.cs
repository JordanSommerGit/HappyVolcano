using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject cubeTemplate = null;
    [SerializeField]
    private GameObject spawnSocket = null;

    [SerializeField]
    private int nrCols = 4;
    [SerializeField]
    private float cellSizeX = 1.0f;
    [SerializeField]
    private float cellSizeY = 1.0f;

    [SerializeField]
    private int poolSize = 400;

    private List<ColoredCube> coloredCubes = new List<ColoredCube>();
    private int nrActiveCubes = 0;
    private Color[] possibleColors = null;

    private void Awake()
    {
        CreateCubes();
        HUDInterface hud = FindObjectOfType<HUDInterface>();
        if (hud)
        {
            hud.OnCubeAmountChanged += UpdateCubeAmount;
            hud.OnColorAmountChanged += UpdateCubeColor;
        }
    }

    //Called when the InputField changes
    public void UpdateCubeAmount(int nrCubes)
    {
        if (nrCubes == nrActiveCubes)
            return;
        nrActiveCubes = nrCubes;
        for (int i = 0; i < coloredCubes.Count; i++)
        {
            coloredCubes[i].gameObject.SetActive(false);
        }
        for (int i = 0; i < nrCubes; i++)
        {
            Vector3 offset = new Vector3(i % nrCols * cellSizeX, i / nrCols * cellSizeY, 0);
            coloredCubes[i].gameObject.SetActive(true);
            coloredCubes[i].gameObject.transform.position = spawnSocket.transform.position + offset;
        }
        ApplyCubeColors();
    }

    //Called when the InputField changes
    public void UpdateCubeColor(int nrColors)
    {
        FillPossibleColors(nrColors);
        ApplyCubeColors();
    }

    private void FillPossibleColors(int nrColors)
    {
        if (possibleColors != null)
        {
            if (nrColors == possibleColors.Length)
                return;
        }
        possibleColors = new Color[nrColors];
        for (int i = 0; i < possibleColors.Length; i++)
        {
            possibleColors[i] = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
        }
    }

    private void ApplyCubeColors()
    {
        if (possibleColors == null)
            return;
        if (possibleColors.Length == 0)
            return;
        for (int i = 0; i < nrActiveCubes; i++)
        {
            coloredCubes[i].SetColor(possibleColors[i % possibleColors.Length]);
        }
    }

    //Pooling cubes
    private void CreateCubes()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject spawnedCube = Instantiate(cubeTemplate);
            spawnedCube.transform.parent = transform;
            coloredCubes.Add(spawnedCube.GetComponent<ColoredCube>());
            spawnedCube.SetActive(false);
        }
    }

    //Notify other colors
    public void NotifyColorHit(Color color)
    {
        foreach (ColoredCube cube in coloredCubes)
        {
            if (cube.MeshColor == color)
            {
                cube.TurnBlack();
            }
        }
    }
}
