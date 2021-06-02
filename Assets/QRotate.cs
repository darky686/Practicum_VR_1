using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QRotate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.rotation = new Quaternion(2, 2, 2, 2);

        
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = new Quaternion(2+Time.time, 2, 2, 2);
    }
}
