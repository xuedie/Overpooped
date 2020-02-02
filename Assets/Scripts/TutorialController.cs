using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialController : OrderController
{
    [SerializeField] GameObject[] hints;

    override protected void Start()
    {
        orders = new List<GameObject>();
        isMakeWhite = isMakeBlack = false;
        CreateOrder(OrderType.White);
        hints[0].SetActive(true);
    }

    // Update is called once per frame
    override protected void Update()
    {
        base.Update();
        if (Input.GetKeyDown(KeyCode.B)) {
            SceneManager.LoadScene("Gameplay");
        }
    }

    override public void StartMakeWhite() {
        if (orders[orders.Count - 1].GetComponent<Order>().IsReadyMake)
        {
            isMakeWhite = true;
            hints[0].SetActive(false);
            hints[1].SetActive(true);
            Debug.Log("OrderController: Cream starts making.");
        }
    }

    override public void StartMakeBlack()
    {
        if (orders[orders.Count - 1].GetComponent<Order>().IsReadyMake)
        {
            isMakeBlack = true;
            hints[0].SetActive(false);
            hints[1].SetActive(true);
            Debug.Log("OrderController: Cream starts making.");
        }
    }

    override public void StopMake()
    {
        if (isMakeWhite || isMakeBlack)
        {
            isMakeWhite = isMakeBlack = false;
            if (orders.Count < 1)
            {
                Debug.LogError("OrderController: orders is empty.", transform);
            }
            else
            {
                // Determine the cream type for assessment
                Order order = orders[orders.Count - 1].GetComponent<Order>();
                float[] values = order.values;
                if (order.type == OrderType.White)
                {
                    if (values[1] == 0f)
                    {
                        order.state = CheckCreamType(values[0]);
                    }
                    else
                        order.state = CreamType.Wrong;
                }
                else if (order.type == OrderType.Black)
                {
                    if (values[0] == 0f)
                    {
                        order.state = CheckCreamType(values[1]);
                    }
                    else
                        order.state = CreamType.Wrong;
                }
                switch (order.state) {
                    case CreamType.Filled:
                        // create next order
                        if (order.type == OrderType.White)
                        {
                            CreateOrder(OrderType.Black);
                        }
                        else {
                            // 
                        }
                        break;
                    case CreamType.Overfilled:
                    case CreamType.Unfilled:
                        break;
                    default:
                        break;
                }
                Debug.Log("OrderController: Cream type " + order.state);
            }
        }
    }

    override public void AssessOrder()
    {
        Debug.Log("OrderController: assess orders " + orders.Count);
        if (orders[0].GetComponent<Order>().state == CreamType.Filled)
        {
            orders[0].GetComponent<Order>().StartMove();
        }
        else
        {
            orders[0].GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        }
        GameObject assessedOrder = orders[0];
        orders.RemoveAt(0);
        assessedOrder.GetComponent<Order>().DestroyOrder();
    }
}
