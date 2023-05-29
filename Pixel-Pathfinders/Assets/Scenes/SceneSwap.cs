using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneSwap : MonoBehaviour {
    private void OnTriggerEnter2D(Collider2D colliderObject)
    {
        SceneManager.LoadScene("New_Main");
        DontDestroyOnLoad(colliderObject);
    }
}