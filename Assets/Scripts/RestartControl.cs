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
        if (Input.GetKeyDown(KeyCode.A) || Input.GetButtonDown("A1"))
        {
            ReadyPlayerOne();
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetButtonDown("A2")) {
            ReadyPlayerTwo();
        }

        // load back the gameplay scene
        if (p1Ready && p2Ready) {
            GameManager.instance.GameStart();
        }
    }

    public void ReadyPlayerOne() {
        p1Ready = true;
        // change UI
    }

    public void ReadyPlayerTwo()
    {
        p2Ready = true;
        // change UI
    }
}
