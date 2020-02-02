using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialController : OrderController
{
    [SerializeField] GameObject[] hints;

    override protected void Start()
    {
        orders = new List<GameObject>();
        isMake = false;
        CreateOrder(OrderType.White);
        hints[0].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        // Call when player pressing the button
        if (Input.GetKeyDown(KeyCode.A))
        {
            StartMake();
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            StopMake();
        }
        if (isMake && Input.GetKeyDown(KeyCode.S))
        {
            orders[orders.Count - 1].GetComponent<Order>().value += 31;
            int orderValue = orders[orders.Count - 1].GetComponent<Order>().value;
            if (orderValue > minFill && orderValue <= maxFill)
            {
                // hint release the button
                hints[1].SetActive(false);
                hints[3].SetActive(true);
            }
        }
    }

    override public void StartMake() {
        if (orders[orders.Count - 1].GetComponent<Order>().IsReadyMake)
        {
            isMake = true;
            hints[0].SetActive(false);
            hints[1].SetActive(true);
            Debug.Log("OrderController: Cream starts making.");
        }
    }

    override public void StopMake()
    {
        isMake = false;
        int orderValue = orders[orders.Count - 1].GetComponent<Order>().value;
        if (orderValue > minFill && orderValue <= maxFill)
        {
            // only move to access gate when the ice cream is filled
            orders[orders.Count - 1].GetComponent<Order>().state = CreamType.Filled;
            orders[orders.Count - 1].GetComponent<Order>().StartMove();
            if (orders[orders.Count - 1].GetComponent<Order>().type == OrderType.White)
            {
                // create second tutorial order
                // CreateOrder(OrderType.Black);
            }
            else {
                // finish tutorial, load next scene when tested true
            }
        }
        else if (orderValue <= minFill)
        {
            // Not filled
            // show press button hint
            hints[0].SetActive(true);
            hints[1].SetActive(false);
        }
        else {
            // Overfilled
            // reset filling progress
            orders[orders.Count - 1].GetComponent<Order>().value = 0;
        }
    }
}
