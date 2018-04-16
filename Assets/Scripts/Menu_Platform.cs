using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu_Platform : MonoBehaviour {

    public float Jump_Force;

    void OnCollisionEnter2D(Collision2D Other)
    {
        // Add force when player fall from top
        if (-Other.relativeVelocity.y <= 0f)
        {
            Rigidbody2D Rigid = Other.collider.GetComponent<Rigidbody2D>();

            if (Rigid != null)
            {
                Vector2 Force = Rigid.velocity;
                Force.y = Jump_Force;
                Rigid.velocity = Force;

                // Play jump sound
                GetComponent<AudioSource>().Play();
            }
        }
    }
}
