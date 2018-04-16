using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform_Blue : MonoBehaviour
{

    // For moving platforms
    private bool To_Right = true;
    private float Offset = 1.2f;

    void FixedUpdate()
    {
        // Move the platform
        Vector3 Top_Left = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));

        if (To_Right) // Move to right
        {
            if (transform.position.x < -Top_Left.x - Offset)
                transform.position += new Vector3(0.1f, 0, 0);
            else
                To_Right = false;
        }
        else // Move to left
        {
            if (transform.position.x > Top_Left.x + Offset)
                transform.position -= new Vector3(0.1f, 0, 0);
            else
                To_Right = true;
        }
    }
}
