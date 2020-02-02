using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    // Start is called before the first frame update
    Vector2 i_movement;
    [SerializeField]
    float moveSpeed = 10.0f;

    [SerializeField]
    GameObject[] hands;

    public int playerId;

    public bool iswhite = false;
    public bool isblack = false;
    public bool isdouble = false;

    string pressedText="Pad";

    public enum Buttonchoice
    {
        pad,
        trigger
    };

    Buttonchoice buttonchoice;

     enum ShitType
    {
        Vanilla,
        Choco,
        Both
    };

    ShitType shitType;

    float ph1, ph2, pv1, pv2;
    float d1, d2;
    void Start()
    {
        ph1 = ph2 = pv1 = pv2 = d1 = d2 = 0f;
        buttonchoice = Buttonchoice.pad;
    }

    // Update is called once per frame
    void Update()
    {

        HandMove();
        

        float h1 = Input.GetAxis("RightJoystickH1");
        float v1 = Input.GetAxis("RightJoystickV1");
        float h2 = Input.GetAxis("RightJoystickH2");
        float v2 = Input.GetAxis("RightJoystickV2");

        Vector2 moveDir1 =  new Vector2(h1 - ph1, v1 - pv1);
        d1 = moveDir1.magnitude;
        Vector2 moveDir2 = new Vector2(h2 - ph2, v2 - pv2);
        d2 = moveDir2.magnitude;

        if (Mathf.Abs(Input.GetAxis(pressedText+"2")) > 0 && (Mathf.Abs(h1) > 0))
        {
            iswhite = true;
        }
        else iswhite = false;

        if (Mathf.Abs(Input.GetAxis(pressedText+"1")) > 0 && (Mathf.Abs(h2) > 0))
        {
            isblack = true;
        }
        else isblack = false;

        if (Mathf.Abs(Input.GetAxis(pressedText + "1")) > 0 && (Mathf.Abs(h2) > 0) && Mathf.Abs(Input.GetAxis(pressedText + "2")) > 0 && (Mathf.Abs(h1) > 0))
        {
            isdouble = true;
        }
        else isdouble = false;



        if(buttonchoice==Buttonchoice.pad)
        {
            pressedText = "Pad";
        }
        else
        {
            pressedText = "LT";
        }

    }

    void HandMove()
    {
       
        Vector2 movement0 = new Vector2(Input.GetAxis("RightJoystickH1"), Input.GetAxis("RightJoystickV1")) * moveSpeed * Time.deltaTime;
        hands[0].transform.Translate(movement0);

        Vector2 movement1 = new Vector2(Input.GetAxis("RightJoystickH2"), Input.GetAxis("RightJoystickV2")) * moveSpeed * Time.deltaTime;
        hands[1].transform.Translate(movement1);

    }

    public bool Getkeydown(OrderType type)
    {
        if (type == OrderType.White)
        {
            return iswhite;
        }

        else if (type == OrderType.Black)
        {
            return isblack;
        }

        else if (type == OrderType.Double)
        {
            return isdouble;
        }
        else return (false); 

       // return Input.GetKeyDown(map[type]);
    }

    //public bool GetKeyUp(OrderType type)
    //{
    //  //  return Input.GetKeyUp(map[type]);
    //}

    public float GetRotateValue(int playerIdx)
    {
        if (playerIdx == 0)
        {
            return d1;
        }
        else {
            return d2;
        }
    }
}
