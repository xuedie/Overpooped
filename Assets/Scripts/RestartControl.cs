using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartControl : MonoBehaviour
{
    [SerializeField] bool p1Ready = false;
    [SerializeField] bool p2Ready = false;


    // Update is called once per frame
    void Update()
    {
        // check left and right input
        if (Input.GetKeyDown(KeyCode.A))
        {
            p1Ready = true;
        }
        else if (Input.GetKeyDown(KeyCode.S)) {
            p2Ready = true;
        }

        // load back the gameplay scene
        if (p1Ready && p2Ready) {
        	SceneManager.LoadScene("Gameplay");
        }
    }
}
