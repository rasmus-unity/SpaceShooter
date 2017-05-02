﻿using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float tilt;
    public Boundary boundary;

    public GameObject shot;
    public Transform shotSpawn;
    public float fireRate;

    private float nextFire;

    void Update ()
    {
        if (CrossPlatformInputManager.GetButton ("Fire") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate (shot, shotSpawn.position, shotSpawn.rotation);
            GetComponent<AudioSource> ().Play ();
        }
    }

    void FixedUpdate ()
    {
        float moveHorizontal = CrossPlatformInputManager.GetAxis ("Horizontal");
        float moveVertical = CrossPlatformInputManager.GetAxis ("Vertical");

        Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
        GetComponent<Rigidbody> ().velocity = movement * speed;

        GetComponent<Rigidbody> ().position = new Vector3 (
            Mathf.Clamp (GetComponent<Rigidbody> ().position.x, boundary.xMin, boundary.xMax), 
            0.0f, 
            Mathf.Clamp (GetComponent<Rigidbody> ().position.z, boundary.zMin, boundary.zMax)
        );

        GetComponent<Rigidbody> ().rotation = Quaternion.Euler (0.0f, 0.0f, GetComponent<Rigidbody> ().velocity.x * -tilt);
    }
}
