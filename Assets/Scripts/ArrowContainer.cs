using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowContainer : MonoBehaviour
{
    bool[] row_colum = new bool[4]; // 가로_세로
    int[] rowRand;
    int[] columRand;

    int rowSize;
    int columSize;

    [SerializeField]
    Vector3[] positions = new Vector3[4];

    [SerializeField]
    Arrow[] arrows;

    Quaternion arrowDirect = Quaternion.identity;

    public void GetTileSize(int row, int colum)
    {
        rowSize = row;
        columSize = colum;
        rowRand = new int[rowSize];
        columRand = new int[columSize];

        for (int i = 0; i < rowRand.Length; i++)
            rowRand[i] = i;
        for (int i = 0; i < columRand.Length; i++)
            columRand[i] = i;
    }

    public void SetArrowPosition()
    {
        Shuffle(rowRand);
        Shuffle(columRand);

        for (int i = 0; i < row_colum.Length; i++)
        {
            int randomBool = Random.Range(0, 2);
            row_colum[i] = (randomBool == 1) ? true : false;
        }

        for (int i = 0; i < row_colum.Length; i++)
        {
            int arrowSize = 0;

            if (row_colum[i]) // 가로일때
            {
                int currentRand = columRand[i];
                int start_end = Random.Range(0, 2);
                arrowSize = rowSize;

                if (start_end == 0)
                {
                    positions[i].x = -1;
                    arrowDirect = Quaternion.Euler(Vector3.right * 90f + Vector3.up * 0f); ;
                }
                else
                {
                    positions[i].x = rowSize;
                    arrowDirect = Quaternion.Euler(Vector3.right * 90f + Vector3.up * 180f);
                }
                positions[i].z = columRand[i];
            }
            else
            {
                int currentRand = rowRand[i];
                int start_end = Random.Range(0, 2);
                arrowSize = columSize;

                if (start_end == 0)
                {
                    positions[i].z = -1;
                    arrowDirect = Quaternion.Euler(Vector3.right * 90f + Vector3.up * -90f);
                }
                else
                {
                    positions[i].z = columSize;
                    arrowDirect = Quaternion.Euler(Vector3.right * 90f + Vector3.up * 90f);
                }
                positions[i].x = rowRand[i];
            }
            arrows[i].transform.position = Vector3.zero;
            arrows[i].transform.rotation = Quaternion.identity;
            arrows[i].Activate(arrowSize);
            arrows[i].transform.position = positions[i];
            arrows[i].transform.rotation = arrowDirect;
            //Instantiate(arrowPrefab, positions[i], arrowDirect);
        }
    }

    void Shuffle(int[] rand)
    {
        for (int i = 0; i < 10; i++)
        {
            int first = Random.Range(0, rand.Length);
            int second = Random.Range(0, rand.Length);

            int temp = rand[first];
            rand[first] = rand[second];
            rand[second] = temp;
        }
    }
}