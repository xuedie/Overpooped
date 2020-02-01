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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    override public void StartMake() {

    }

    override public void StopMake()
    {

    }
}
