using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class testscript : MonoBehaviour
{
    // Start is called before the first frame update
    InputGamePlay controls;

    private void Awake()
    {
        controls = new InputGamePlay();
        controls.ControllerInput.Bear.performed += ctx=>TouchBelly();
    }

    void TouchBelly()
    {
        transform.localScale *= 1.1f;
    }

    private void OnEnable()
    {
        controls.ControllerInput.Enable();
    }

    private void OnDisable()
    {
        controls.ControllerInput.Disable();
    }


}
