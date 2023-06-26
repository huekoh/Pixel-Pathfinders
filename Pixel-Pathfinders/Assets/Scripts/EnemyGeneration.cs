using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGeneration : MonoBehaviour
{

    public GameObject theEnemy;
    public int xPos;
    public int yPos;
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
            xPos = Random.Range(-6,3);
            yPos = Random.Range(-2,4);
            Instantiate(theEnemy, new Vector3(xPos, yPos, 0), Quaternion.identity);
            yield return new WaitForSeconds(buffer);
            counter += 1;
        }
    }
}
