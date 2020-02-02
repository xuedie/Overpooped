using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inputtest : MonoBehaviour
{
    // Start is called before the first frame update

    Vector2 i_movement;
    [SerializeField]
    float moveSpeed = 10.0f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        Move();
        Debug.Log(Input.GetAxis("RightJoystickH1"));
    }

    void Move()
    {
        Vector2 movement = new Vector2(Input.GetAxis("RightJoystickH1"), Input.GetAxis("RightJoystickV1")) * moveSpeed * Time.deltaTime;
        transform.Translate(movement);
    }
}
