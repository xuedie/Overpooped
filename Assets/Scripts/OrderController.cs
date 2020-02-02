using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderController : MonoBehaviour
{
    [SerializeField]
    protected List<GameObject> orders;
    [SerializeField]
    GameObject orderPrefab;
    [SerializeField]
    GameObject initialPos;
    [SerializeField]
    float orderSpeed = 3.5f;

    protected bool isMake;
    [SerializeField]
    public int maxFill = 100;
    public int minFill = 90;

    virtual protected void Start()
    {
        orders = new List<GameObject>(); // Store current orders
        isMake = false;
        // Game Starts
        CreateOrder();
    }

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

        // Start assessing the order
        if (orders.Count > 1 && orders[0].GetComponent<Order>().IsReadyAssess)
        {
            AssessOrder();
            GameObject assessedOrder = orders[0];
            orders.RemoveAt(0);
            assessedOrder.GetComponent<Order>().DestroyOrder();
        }
    }

    void CreateOrder()
    {
        GameObject order = Instantiate(orderPrefab, initialPos.transform.position, Quaternion.identity);
        order.GetComponent<Order>().Initailize(OrderType.White, orderSpeed);
        orders.Add(order);
        Debug.Log("OrderController: orders " + orders.Count);
    }

    protected void CreateOrder(OrderType type)
    {
        GameObject order = Instantiate(orderPrefab, initialPos.transform.position, Quaternion.identity);
        order.GetComponent<Order>().Initailize(type, orderSpeed);
        orders.Add(order);
        Debug.Log("OrderController: orders " + orders.Count);
    }

    void AssessOrder()
    {
        if(orders[0].GetComponent<Order>().state == CreamType.Filled)
        {
            orders[0].GetComponent<Order>().StartMove();
        }
        else if(orders[0].GetComponent<Order>().state == CreamType.Unfilled 
            || orders[0].GetComponent<Order>().state == CreamType.Overfilled)
        {
            orders[0].GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }

    virtual public void StartMake()
    {
        if (orders[orders.Count - 1].GetComponent<Order>().IsReadyMake)
        {
            isMake = true;
            Debug.Log("OrderController: Cream starts making.");
        }
    }

    virtual public void StopMake()
    {
        isMake = false;
        if(orders.Count < 1)
        {
            Debug.LogError("OrderController: orders is empty.", transform);
        }
        else
        {
            // Determine the cream type for assessment
            int orderValue = orders[orders.Count - 1].GetComponent<Order>().value;
            if (orderValue > minFill && orderValue <= maxFill)
                orders[orders.Count - 1].GetComponent<Order>().state = CreamType.Filled;
            else if (orderValue <= minFill)
                orders[orders.Count - 1].GetComponent<Order>().state = CreamType.Unfilled;
            else
                orders[orders.Count - 1].GetComponent<Order>().state = CreamType.Overfilled;
                orders[orders.Count - 1].GetComponent<Order>().state = CreamType.Overfilled;
            Debug.Log("OrderController: Cream type " + orders[orders.Count - 1].GetComponent<Order>().state);
            // Continue moving
            orders[orders.Count - 1].GetComponent<Order>().StartMove();
            // Create the next order
            CreateOrder();
            Debug.Log("OrderController: Cream stops making");
        }
    }
}
