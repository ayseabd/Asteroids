using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    //thrust and rotation support
    Rigidbody2D rb2D;
    Vector2 thrustDirection = new Vector2(1, 0);
    const float ThrustForce = 10;
    const float RotateDegreesPerSecond = 180;

    //shooting support
    [SerializeField]
    GameObject prefabBullet;

    //death support
    [SerializeField]
    GameObject hud;

    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // check for rotation input
        float rotationInput = Input.GetAxis("Rotate");
        if(rotationInput != 0)
        {
            //calculate rotation amount and apply rotation
            float rotationAmount = RotateDegreesPerSecond * Time.deltaTime;
            if(rotationInput < 0)
            {
                rotationAmount *= -1;
            }
            transform.Rotate(Vector3.forward, rotationAmount);

            // change thrust direction to match ship rotation
            float zRotation = transform.eulerAngles.z * Mathf.Deg2Rad;
            thrustDirection.x = Mathf.Cos(zRotation);
            thrustDirection.y = Mathf.Sin(zRotation);
        }

        //shoot as appropriate
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            GameObject bullet = Instantiate(prefabBullet, transform.position, Quaternion.identity);
            Bullet script = bullet.GetComponent<Bullet>();
            script.ApplyForce(thrustDirection);
            AudioManager.Play(AudioClipName.PlayerShot);
        }

    }

    void FixedUpdate()
    {
        if(Input.GetAxis("Thrust") !=0)
        {
            rb2D.AddForce(ThrustForce * thrustDirection, ForceMode2D.Force);

        }
    }

    //destroys the ship on collision with an asteroid
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Asteroid"))
        {
            AudioManager.Play(AudioClipName.PlayerDeath);
            hud.GetComponent<HUD>().StopGameTimer();
            Destroy(gameObject);
        }
    }

}
