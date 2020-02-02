using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int s = GameManager.instance.GetScore();
        Text t = GetComponent<Text>();
        t.text = s.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
