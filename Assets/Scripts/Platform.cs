using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour {

    public float Jump_Force = 10f;
    private float Destroy_Distance;
    private bool Create_NewPlatform = false;

    private GameObject Game_Controller;

    // Use this for initialization
    void Start()
    {
        Game_Controller = GameObject.Find("Game_Controller");

        // Set distance to destroy the platforms out of screen
        Destroy_Distance = Game_Controller.GetComponent<Game_Controller>().Get_DestroyDistance();
    }

    void FixedUpdate()
    {
        // Platform out of screen
        if (transform.position.y - Camera.main.transform.position.y < Destroy_Distance)
        {
            // Create new platform
            if (name != "Platform_Brown(Clone)" && name != "Spring(Clone)" && name != "Trampoline(Clone)" && !Create_NewPlatform)
            {
                Game_Controller.GetComponent<Platform_Generator>().Generate_Platform(1);
                Create_NewPlatform = true;
            }
            
            // Deactive Collider and effector
            GetComponent<EdgeCollider2D>().enabled = false;
            GetComponent<PlatformEffector2D>().enabled = false;
            GetComponent<SpriteRenderer>().enabled = false;

            // Deactive collider and effector if gameobject has child
            if (transform.childCount > 0)
            {
                if(transform.GetChild(0).GetComponent<Platform>()) // if child is platform
                {
                    transform.GetChild(0).GetComponent<EdgeCollider2D>().enabled = false;
                    transform.GetChild(0).GetComponent<PlatformEffector2D>().enabled = false;
                    transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
                }

                // Destroy this platform if sound has finished
                if (!GetComponent<AudioSource>().isPlaying && !transform.GetChild(0).GetComponent<AudioSource>().isPlaying)
                    Destroy(gameObject);
            }
            else
            {
                // Destroy this platform if sound has finished
                if (!GetComponent<AudioSource>().isPlaying)
                    Destroy(gameObject);
            }
        }
    }

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

                // if gameobject has animation; Like spring, trampoline and etc...
                if (GetComponent<Animator>())
                    GetComponent<Animator>().SetBool("Active", true);

                // Check platform type
                Platform_Type();
            }
        }
    }

    void Platform_Type()
    {
        if (GetComponent<Platform_White>())
            GetComponent<Platform_White>().Deactive();
        else if (GetComponent<Platform_Brown>())
            GetComponent<Platform_Brown>().Deactive();
    }
}
