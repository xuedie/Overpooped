using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum OrderType
{
    White,
    Black,
    Double
};

public enum CreamType
{
    Unfilled,
    Filled,
    Overfilled,
    Wrong
}

public class Order : MonoBehaviour
{
    public OrderType type;
    public CreamType state;
    public float speed;
    public float[] values = {0f, 0f};
    public GameObject[] tops;
    public Text text;

    float delaytime = 5f;
    bool isMove = false;
    bool isReadyMake, isReadyAssess;

    public bool IsMove
    {
        get { return isMove; }
    }

    public bool IsReadyMake
    {
        get { return isReadyMake; }
    }
    public bool IsReadyAssess
    {
        get { return isReadyAssess; }
    }

    void Start()
    {
        isReadyMake = isReadyAssess = false;
    }

    void Update()
    {
        if (isMove)
        {
            Vector3 tmp = transform.position;
            tmp.x += speed * Time.deltaTime;
            transform.position = tmp;
        }
    }

    public void Initailize(OrderType curType, float curSpeed)
    {
        type = curType;
        speed = curSpeed;
        if(curType == OrderType.White)
        {
            text.text = "VANILLA";
        }
        else if(curType == OrderType.Black)
        {
            text.text = "CHOCOLATE";
        }
        else
        {
            text.text = "DOUBLE";
        }
        StartMove();
    }

    public void StartMove()
    {
        Debug.Log("Order: StartMove");
        isMove = true;
        SoundManager.instance.PlaySFX("conveyorBelt");
    }

    public void PauseMove()
    {
        Debug.Log("Order: PauseMove");
        isMove = false;
    }

    public void DestroyOrder()
    {
        Debug.Log("Order: destroy the order");
        Destroy(this.gameObject, delaytime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.tag == "Machine")
        {
            isReadyMake = true;
        }
        else if(collision.gameObject.tag == "Assessor")
        {
            isReadyMake = false;
            isReadyAssess = true;
            OrderController.instance.AssessOrder();
        }
        PauseMove();
    }
}
