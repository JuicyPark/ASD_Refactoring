using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowSpawnsor : MonoBehaviour
{
    [SerializeField]
    int arrowCount;
    [SerializeField]
    Color[] arrowColor;

    bool[] row_colum;
    int[] rowRand;
    int[] columRand;

    int rowSize;
    int columSize;

    Vector3[] positions;

    [SerializeField]
    ArrowContainer arrowContainerPrefab;
    ArrowContainer[] arrowContainers;

    Transform[] startWarp;
    Transform[] endWarp;

    Quaternion arrowDirect = Quaternion.identity;

    public void SetArrowContainerPosition() => StartCoroutine(CSetArrowContainerPosition());

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

    void Awake()
    {
        row_colum = new bool[arrowCount];
        positions = new Vector3[arrowCount];
        startWarp = new Transform[arrowCount];
        endWarp = new Transform[arrowCount];

        arrowContainers = new ArrowContainer[arrowCount];

        for (int i = 0; i < arrowContainers.Length; i++)
        {
            arrowContainers[i] = Instantiate(arrowContainerPrefab, Vector3.right * 90f, Quaternion.identity, transform);
            arrowContainers[i].SetArrowColor(arrowColor[i]);
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

    IEnumerator CSetArrowContainerPosition()
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

            if (row_colum[i])
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

            arrowContainers[i].transform.position = Vector3.zero;
            arrowContainers[i].transform.rotation = Quaternion.identity;
            arrowContainers[i].SpawnArrow(arrowSize);
            startWarp[i] = arrowContainers[i].transform;
            endWarp[i] = arrowContainers[i].LastArrowTileTransform;
            
            arrowContainers[i].transform.position = positions[i];
            arrowContainers[i].transform.rotation = arrowDirect;
            yield return arrowContainers[i].CActivate(arrowSize);
        }
    }
}