using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //death support
    const float LifeSeconds = 2;
    Timer deathTimer;
    
    // Start is called before the first frame update
    void Start()
    {
        //create and run death timer
        deathTimer = gameObject.AddComponent<Timer>();
        deathTimer.Duration = LifeSeconds;
        deathTimer.Run();
    }

    // Update is called once per frame
    void Update()
    {
        //kill bullet when timer is done
        if (deathTimer.Finished)
        {
            Destroy(gameObject);
        }
    }

    //applies a force to the bullet in the given direction
    public void ApplyForce(Vector2 forceDirection)
    {
        const float forceMagnitude = 10;
        GetComponent<Rigidbody2D>().AddForce(
            forceMagnitude * forceDirection, ForceMode2D.Impulse);

    }
}
