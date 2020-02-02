using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrearPoop : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject shit;
    public GameObject positionobj;

  public void ShitShow()
    {
        Instantiate(shit, positionobj.transform.position, Quaternion.identity);
    }
}
