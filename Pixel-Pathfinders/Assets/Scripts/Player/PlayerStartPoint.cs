using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStartPoint : MonoBehaviour
{

    private PlayerControl thePlayer;
    private CameraControl theCamera;

    // Start is called before the first frame update
    void Start()
    {
        thePlayer = FindObjectOfType<PlayerControl>();
        thePlayer.transform.position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
