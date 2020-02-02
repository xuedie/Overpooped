using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shitmove : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D rd;
    public float initialSpeedx = 10.0f;
    public float initialSpeedy = 10.0f;
    void Start()
    {
        rd = this.GetComponent<Rigidbody2D>();
        rd.velocity = new Vector2(initialSpeedx, initialSpeedy);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
