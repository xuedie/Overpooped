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
    float orderSpeed = 8f;
    [SerializeField]
    float valueSpeed = 5f;
    [SerializeField]
    SliderController sliderController;

    protected bool isMakeWhite;
    protected bool isMakeBlack;
    bool isCoro = false;
    int count = 0;
    int noDoubleNum = 5;
    [SerializeField]
    float maxFill = 100f;
    public float minFill = 90f;
    public float maxOrderSpeed = 20f;
    float[] tmpValues = { 0, 0 };

    public static OrderController instance;
    protected OrderController() { }

    void Awake()
    {
        if (instance == null) {
            instance = this;
        } 
        else {
            Destroy(this.gameObject);
        }

    }

    virtual protected void Start()
    {
        orders = new List<GameObject>(); // Store current orders
        isMakeWhite = isMakeBlack = false;
        initialPos = GameManager.instance.GetInitialPosition();
        sliderController = GameManager.instance.GetSliderController();
        // Game Starts
        CreateOrder();
    }

    virtual protected void Update()    
    {
        if (!isCoro)
        {
            StartCoroutine(CountDown());
        }
        // Call when player pressing the button
        if (Input.GetKeyDown(KeyCode.A) || InputManager.instance.GetKeyDown(OrderType.White))
        {
            StartMakeWhite();
        }
        if (Input.GetKeyUp(KeyCode.A) || InputManager.instance.GetKeyUp(OrderType.White))
        {
            StopMake();
        }
        if (Input.GetKeyDown(KeyCode.S) || InputManager.instance.GetKeyDown(OrderType.Black))
        {
            StartMakeBlack();
        }
        if (Input.GetKeyUp(KeyCode.S) || InputManager.instance.GetKeyUp(OrderType.Black))
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
                if(orders[0].GetComponent<Order>().type == OrderType.Double)
                {
                    Color tmp = orders[0].GetComponent<Order>().tops[0].GetComponent<SpriteRenderer>().color;
                    tmp.a = orders[0].GetComponent<Order>().values[0] / 100f;
                    orders[0].GetComponent<Order>().tops[2].GetComponent<SpriteRenderer>().color = tmp;
                }
                else
                {
                    Color tmp = orders[0].GetComponent<Order>().tops[0].GetComponent<SpriteRenderer>().color;
                    tmp.a = orders[0].GetComponent<Order>().values[0] / 100f;
                    orders[0].GetComponent<Order>().tops[0].GetComponent<SpriteRenderer>().color = tmp;
                }
            }
        }
        else if(!Input.GetKey(KeyCode.N) && Input.GetKey(KeyCode.M))
        {
            if (isMakeBlack)
            {
                orders[0].GetComponent<Order>().values[1] += valueSpeed * Time.deltaTime;
                if (orders[0].GetComponent<Order>().type == OrderType.Double)
                {
                    Color tmp = orders[0].GetComponent<Order>().tops[0].GetComponent<SpriteRenderer>().color;
                    tmp.a = orders[0].GetComponent<Order>().values[1] / 100f;
                    orders[0].GetComponent<Order>().tops[3].GetComponent<SpriteRenderer>().color = tmp;
                }
                else
                {
                    Color tmp = orders[0].GetComponent<Order>().tops[0].GetComponent<SpriteRenderer>().color;
                    tmp.a = orders[0].GetComponent<Order>().values[1] / 100f;
                    orders[0].GetComponent<Order>().tops[1].GetComponent<SpriteRenderer>().color = tmp;
                }
            }
        }
        else if (Input.GetKey(KeyCode.N) && Input.GetKey(KeyCode.M))
        {
            if (isMakeWhite && isMakeBlack)
            {
                orders[0].GetComponent<Order>().values[0] += valueSpeed * Time.deltaTime;
                orders[0].GetComponent<Order>().values[1] += valueSpeed * Time.deltaTime;
                if (orders[0].GetComponent<Order>().type == OrderType.Double)
                {
                    Color tmp = orders[0].GetComponent<Order>().tops[0].GetComponent<SpriteRenderer>().color;
                    tmp.a = orders[0].GetComponent<Order>().values[0] / 100f;
                    orders[0].GetComponent<Order>().tops[2].GetComponent<SpriteRenderer>().color = tmp;
                    tmp = orders[0].GetComponent<Order>().tops[0].GetComponent<SpriteRenderer>().color;
                    tmp.a = orders[0].GetComponent<Order>().values[1] / 100f;
                    orders[0].GetComponent<Order>().tops[3].GetComponent<SpriteRenderer>().color = tmp;
                }
                else
                {
                    Color tmp = orders[0].GetComponent<Order>().tops[0].GetComponent<SpriteRenderer>().color;
                    tmp.a = orders[0].GetComponent<Order>().values[0] / 100f;
                    orders[0].GetComponent<Order>().tops[0].GetComponent<SpriteRenderer>().color = tmp;
                    tmp = orders[0].GetComponent<Order>().tops[0].GetComponent<SpriteRenderer>().color;
                    tmp.a = orders[0].GetComponent<Order>().values[1] / 100f;
                    orders[0].GetComponent<Order>().tops[1].GetComponent<SpriteRenderer>().color = tmp;
                }
            }
        }

       if (isMakeWhite)
       {
            orders[0].GetComponent<Order>().values[0] += InputManager.instance.GetRotateValue(0) * valueSpeed;
            if (orders[0].GetComponent<Order>().type == OrderType.Double)
            {
                Color tmp = orders[0].GetComponent<Order>().tops[0].GetComponent<SpriteRenderer>().color;
                tmp.a = orders[0].GetComponent<Order>().values[0] / 100f;
                orders[0].GetComponent<Order>().tops[2].GetComponent<SpriteRenderer>().color = tmp;
            }
            else
            {
                Color tmp = orders[0].GetComponent<Order>().tops[0].GetComponent<SpriteRenderer>().color;
                tmp.a = orders[0].GetComponent<Order>().values[0] / 100f;
                orders[0].GetComponent<Order>().tops[0].GetComponent<SpriteRenderer>().color = tmp;
            }
        }
       else if (isMakeBlack)
       {
            orders[0].GetComponent<Order>().values[1] += InputManager.instance.GetRotateValue(1) * valueSpeed;
            if (orders[0].GetComponent<Order>().type == OrderType.Double)
            {
                Color tmp = orders[0].GetComponent<Order>().tops[0].GetComponent<SpriteRenderer>().color;
                tmp.a = orders[0].GetComponent<Order>().values[1] / 100f;
                orders[0].GetComponent<Order>().tops[3].GetComponent<SpriteRenderer>().color = tmp;
            }
            else
            {
                Color tmp = orders[0].GetComponent<Order>().tops[0].GetComponent<SpriteRenderer>().color;
                tmp.a = orders[0].GetComponent<Order>().values[1] / 100f;
                orders[0].GetComponent<Order>().tops[1].GetComponent<SpriteRenderer>().color = tmp;
            }
        }
        else if (isMakeWhite && isMakeBlack)
        {
            orders[0].GetComponent<Order>().values[0] += InputManager.instance.GetRotateValue(0) * valueSpeed;
            orders[0].GetComponent<Order>().values[1] += InputManager.instance.GetRotateValue(1) * valueSpeed;
            if (orders[0].GetComponent<Order>().type == OrderType.Double)
            {
                Color tmp = orders[0].GetComponent<Order>().tops[0].GetComponent<SpriteRenderer>().color;
                tmp.a = orders[0].GetComponent<Order>().values[0] / 100f;
                orders[0].GetComponent<Order>().tops[2].GetComponent<SpriteRenderer>().color = tmp;
                tmp = orders[0].GetComponent<Order>().tops[0].GetComponent<SpriteRenderer>().color;
                tmp.a = orders[0].GetComponent<Order>().values[1] / 100f;
                orders[0].GetComponent<Order>().tops[3].GetComponent<SpriteRenderer>().color = tmp;
            }
            else
            {
                Color tmp = orders[0].GetComponent<Order>().tops[0].GetComponent<SpriteRenderer>().color;
                tmp.a = orders[0].GetComponent<Order>().values[0] / 100f;
                orders[0].GetComponent<Order>().tops[0].GetComponent<SpriteRenderer>().color = tmp;
                tmp = orders[0].GetComponent<Order>().tops[0].GetComponent<SpriteRenderer>().color;
                tmp.a = orders[0].GetComponent<Order>().values[1] / 100f;
                orders[0].GetComponent<Order>().tops[1].GetComponent<SpriteRenderer>().color = tmp;
            }
        }

        // Update the values of two sliders
        sliderController.SyncroValue(orders[0].GetComponent<Order>().values);
    }

    void CreateOrder()
    {
        GameObject order = Instantiate(orderPrefab, initialPos.transform.position, Quaternion.identity);
        if (count > noDoubleNum)
        {
            order.GetComponent<Order>().Initailize((OrderType)Random.Range(0, 3), orderSpeed);
        }
        else
        {
            order.GetComponent<Order>().Initailize((OrderType)Random.Range(0, 2), orderSpeed);
            count++;
        }
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

    virtual public void AssessOrder()
    {
        Debug.Log("OrderController: assess orders " + orders.Count);
        if(orders[0].GetComponent<Order>().state == CreamType.Filled)
        {
            SoundManager.instance.PlayOrderSound(true);
            orders[0].GetComponent<Order>().StartMove();
            GameManager.instance.IncreaseOrder();
        }
        else
        {
            SoundManager.instance.PlayOrderSound(false);
            orders[0].GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
            SoundManager.instance.PlaySFX("icecreamDrop");
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
                else {
                    if (CheckCreamType(values[0]) == CreamType.Filled && 
                        CheckCreamType(values[1]) == CreamType.Filled)
                        order.state = CreamType.Filled;
                    else if (CheckCreamType(values[0]) == CreamType.Unfilled &&
                        CheckCreamType(values[1]) == CreamType.Unfilled)
                        order.state = CreamType.Unfilled;
                    else if (CheckCreamType(values[0]) == CreamType.Overfilled &&
                        CheckCreamType(values[1]) == CreamType.Overfilled)
                        order.state = CreamType.Overfilled;
                    else
                        order.state = CreamType.Wrong;
                }
                Debug.Log("OrderController: Cream type " + order.state);
                // Continue moving
                order.StartMove();
                // Create the next order
                CreateOrder();
                Debug.Log("OrderController: Cream stops making");
            }
        }
    }

    IEnumerator CountDown()
    {
        isCoro = true;
        float seconds = 20f + Random.Range(-10f, 10f);
        yield return new WaitForSeconds(seconds);
        orderSpeed = orderSpeed + 0.5f < maxOrderSpeed ? orderSpeed + 0.5f : orderSpeed;
        isCoro = false;
    }

    protected CreamType CheckCreamType(float v) {
        if (v > minFill && v < maxFill)
            return CreamType.Filled;
        else if (v < minFill)
            return CreamType.Unfilled;
        else
            return CreamType.Overfilled;
    }
}
