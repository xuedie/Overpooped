using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialController : OrderController
{
    [SerializeField] GameObject[] hints, startSigns;
    [SerializeField] bool p1Ready, p2Ready;

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
        if (!p1Ready || !p2Ready)
        {
            if (Input.GetKeyDown(KeyCode.B))
            {
                SceneManager.LoadScene("Gameplay");
            }
            if (!p1Ready && (Input.GetKeyDown(KeyCode.Z) || Input.GetButtonDown("A1")))
            {
                p1Ready = true;
                startSigns[0].SetActive(true);
            }
            if (!p2Ready && (Input.GetKeyDown(KeyCode.X) || Input.GetButtonDown("A2")))
            {
                p2Ready = true;
                startSigns[1].SetActive(true);
            }
            if (p1Ready && p2Ready) {
                hints[0].SetActive(false);
                hints[1].SetActive(true);
            }
        }
        else {
            base.Update();
        }
    }

    override public void StartMakeWhite() {
        if (orders[orders.Count - 1].GetComponent<Order>().IsReadyMake)
        {
            isMakeWhite = true;
            hints[1].SetActive(false);
            hints[2].SetActive(true);
            Debug.Log("OrderController: Cream starts making.");
        }
    }

    override public void StartMakeBlack()
    {
        if (orders[orders.Count - 1].GetComponent<Order>().IsReadyMake)
        {
            isMakeBlack = true;
            hints[1].SetActive(false);
            hints[2].SetActive(true);
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
                Debug.Log("OrderController: Cream type " + order.state);
                // Continue moving
                order.StartMove();
                // Create the next order
                // CreateOrder();
                Debug.Log("OrderController: Cream stops making");
            }
        }
    }

    override public void AssessOrder()
    {
        Debug.Log("OrderController: assess orders " + orders.Count);
        Order order = orders[0].GetComponent<Order>();
        if (order.state == CreamType.Filled)
        {
            order.StartMove();
            if (order.type == OrderType.White)
            {
                CreateOrder(OrderType.Black);
            }
            else
            {
                // wait and load scene
                GameManager.instance.GameStart();
            }
        }
        else
        {
            orders[0].GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
            CreateOrder(order.type);
        }
        GameObject assessedOrder = orders[0];
        orders.RemoveAt(0);
        assessedOrder.GetComponent<Order>().DestroyOrder();
        hints[1].SetActive(true);
        hints[2].SetActive(false);
    }
}
