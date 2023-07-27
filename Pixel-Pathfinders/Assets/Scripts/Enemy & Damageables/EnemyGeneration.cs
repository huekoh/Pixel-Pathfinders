using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGeneration : MonoBehaviour
{
    [SerializeField]
    private GameObject theEnemy;
    public int xPos;
    public int yPos;
    public int xPosMin;
    public int xPosMax;
    public int yPosMin;
    public int yPosMax;
    public int counter = 0;
    public int enemyCount;
    public float buffer;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(EnemyDrop());
    }

    IEnumerator EnemyDrop()
    {
        while (counter < enemyCount)
        {
            xPos = Random.Range(xPosMin, xPosMax);
            yPos = Random.Range(yPosMin, yPosMax);
            Instantiate(theEnemy, new Vector3(xPos, yPos, 0), Quaternion.identity);
            yield return new WaitForSeconds(buffer);
            counter += 1;
        }
    }
}
