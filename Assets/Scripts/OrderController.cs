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
    [SerializeField]
    float valueSpeed = 5f;
    [SerializeField]
    SliderController sliderController;

    protected bool isMakeWhite;
    protected bool isMakeBlack;
    [SerializeField]
    float maxFill = 100f;
    public float minFill = 90f;
    float[] tmpValues = { 0, 0 };

    public static OrderController instance;
    OrderController() { }

    void Awake()
    {
        instance = this;
    }

    virtual protected void Start()
    {
        orders = new List<GameObject>(); // Store current orders
        isMakeWhite = isMakeBlack = false;
        // Game Starts
        CreateOrder();
    }

    void Update()    
    {
        // Call when player pressing the button
        if (Input.GetKeyDown(KeyCode.A))
        {
            StartMakeWhite();
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            StopMake();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            StartMakeBlack();
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            StopMake();
        }

        // Call when player rolling the stick
        // Only for testing
        // N as white, M as black
        if (Input.GetKey(KeyCode.N) && !Input.GetKey(KeyCode.M))
        {
            if (isMakeWhite)
            {
                orders[0].GetComponent<Order>().values[0] += valueSpeed * Time.deltaTime;
            }
        }
        else if(!Input.GetKey(KeyCode.N) && Input.GetKey(KeyCode.M))
        {
            if (isMakeBlack)
            {
                orders[0].GetComponent<Order>().values[1] += valueSpeed * Time.deltaTime;
            }
        }
        else if (Input.GetKey(KeyCode.N) && Input.GetKey(KeyCode.M))
        {
            if (isMakeWhite && isMakeBlack)
            {
                orders[0].GetComponent<Order>().values[1] += valueSpeed * Time.deltaTime;
                orders[0].GetComponent<Order>().values[1] += valueSpeed * Time.deltaTime;
            }
        }
        // Update the values of two sliders
        sliderController.SyncroValue(orders[0].GetComponent<Order>().values);

        //if (orders.Count > 1 && orders[0].GetComponent<Order>().IsReadyAssess)
        //{
        //    AssessOrder();
        //    GameObject assessedOrder = orders[0];
        //    orders.RemoveAt(0);
        //    assessedOrder.GetComponent<Order>().DestroyOrder();
        //}
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

    public void AssessOrder()
    {
        Debug.Log("OrderController: assess orders " + orders.Count);
        if(orders[0].GetComponent<Order>().state == CreamType.Filled)
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

    virtual public void StartMakeWhite()
    {
        if (orders[orders.Count - 1].GetComponent<Order>().IsReadyMake)
        {
            isMakeWhite = true;
            Debug.Log("OrderController: Cream starts making.");
        }
    }

    virtual public void StartMakeBlack()
    {
        if (orders[orders.Count - 1].GetComponent<Order>().IsReadyMake)
        {
            isMakeBlack = true;
            Debug.Log("OrderController: Cream starts making.");
        }
    }

    virtual public void StopMake()
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
                float[] values = orders[orders.Count - 1].GetComponent<Order>().values;
                if (orders[orders.Count - 1].GetComponent<Order>().type == OrderType.White)
                {
                    if (values[1] == 0f)
                    {
                        if (values[0] > minFill && values[0] <= maxFill)
                            orders[orders.Count - 1].GetComponent<Order>().state = CreamType.Filled;
                        else if (values[0] <= minFill)
                            orders[orders.Count - 1].GetComponent<Order>().state = CreamType.Unfilled;
                        else
                            orders[orders.Count - 1].GetComponent<Order>().state = CreamType.Overfilled;
                    }
                    else
                        orders[orders.Count - 1].GetComponent<Order>().state = CreamType.Wrong;
                }
                else if (orders[orders.Count - 1].GetComponent<Order>().type == OrderType.Black)
                {
                    if (values[0] == 0f)
                    {
                        if (values[1] > minFill && values[1] <= maxFill)
                            orders[orders.Count - 1].GetComponent<Order>().state = CreamType.Filled;
                        else if (values[1] <= minFill)
                            orders[orders.Count - 1].GetComponent<Order>().state = CreamType.Unfilled;
                        else
                            orders[orders.Count - 1].GetComponent<Order>().state = CreamType.Overfilled;
                    }
                    else
                        orders[orders.Count - 1].GetComponent<Order>().state = CreamType.Wrong;
                }
                else
                {
                    if (values[0] > minFill && values[0] <= maxFill && values[1] > minFill && values[1] <= maxFill)
                        orders[orders.Count - 1].GetComponent<Order>().state = CreamType.Filled;
                    else if (values[0] <= minFill && values[1] <= minFill)
                        orders[orders.Count - 1].GetComponent<Order>().state = CreamType.Unfilled;
                    else if (values[0] >= minFill && values[1] >= minFill)
                        orders[orders.Count - 1].GetComponent<Order>().state = CreamType.Overfilled;
                    else
                        orders[orders.Count - 1].GetComponent<Order>().state = CreamType.Wrong;
                }
                Debug.Log("OrderController: Cream type " + orders[orders.Count - 1].GetComponent<Order>().state);
                // Continue moving
                orders[orders.Count - 1].GetComponent<Order>().StartMove();
                // Create the next order
                CreateOrder();
                Debug.Log("OrderController: Cream stops making");
            }
        }
    }
}
