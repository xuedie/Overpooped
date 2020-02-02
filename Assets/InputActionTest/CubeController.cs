using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CubeController : MonoBehaviour
{
    // Start is called before the first frame update

    Vector2 i_movement;
    [SerializeField]
    float moveSpeed=10.0f;

    PlayerInputManager playInputManager;
    PlayerInput playerinput;


    public bool isvanilla = false;
  
    

    void Start()
    {
        playInputManager = new PlayerInputManager();
        playerinput = new PlayerInput();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        Vector2 movement = new Vector3(i_movement.x,i_movement.y)*moveSpeed*Time.deltaTime;
        transform.Translate(movement);
    }
    private void OnMove(InputValue value)
    {
        if (isvanilla)
        {
            i_movement = value.Get<Vector2>();
            Debug.Log(playerinput.playerIndex);
        }
    }

    void OnVanilla()
    {
        isvanilla = true;
        Debug.Log("vanilla");
    }
}
