using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGeneration : MonoBehaviour
{
    [SerializeField]
    private int numberOfRows, numberOfColumns;
    [SerializeField]
    private GameObject blocPrefab;
    [SerializeField]
    private Transform trsfParent;
    [SerializeField]
    private int numberOfBlocks;

    // Start is called before the first frame update
    void Start()
    {
        CreateBorder();
        SpawnRandomBlocks(numberOfBlocks);
    }

    private void CreateBorder()
    {
        for (int i = 0; i < numberOfRows; i++)
        {
            for (int j = 0; j < numberOfRows; j++)
            {
                if (i == 0 || i == numberOfRows - 1 || j == 0 || j == numberOfColumns - 1)
                {
                    Vector3 pos = new Vector3((float)i, (float) j, 0f);
                    var bloc = Instantiate(blocPrefab, pos, Quaternion.identity, trsfParent);
                    bloc.name = "bloc : (" + i + ", " + j + ")";
                }
            }
        }
    }

    private void SpawnRandomBlocks(int number)
    {
        for (int k = 0; k < number; k++)
        {
            int i = Random.Range(1, numberOfRows - 1);
            int j = Random.Range(1, numberOfColumns - 1);
            Vector3 pos = new Vector3((float)i, (float)j, 0f);
            var bloc = Instantiate(blocPrefab, pos, Quaternion.identity, trsfParent);
            bloc.name = "bloc : (" + i + ", " + j + ")";
        }
    }
}
