using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenWrapper : MonoBehaviour
{
    //screen wrapping support
    float colliderRadius;
    // Start is called before the first frame update
    void Start()
    {
        colliderRadius = GetComponent<CircleCollider2D>().radius;
    }
    // Called when the game object becomes invisible to the camera
    void OnBecameInvisible()
    {
        /*    we can't change transform.position.x directly, so
              we'll have to save the transform position propery 
              in a local Vector2 variable
        */
        Vector2 position = transform.position;

        //check left, right, top and bottom sides
        if (position.x + colliderRadius < ScreenUtils.ScreenLeft ||
            position.x - colliderRadius > ScreenUtils.ScreenRight)
        {
            position.x *= -1;
        }
        if (position.y - colliderRadius > ScreenUtils.ScreenTop ||
            position.y + colliderRadius > ScreenUtils.ScreenBottom)
        {
            position.y *= -1;
        }

        //move ship
        transform.position = position;
    }

}
