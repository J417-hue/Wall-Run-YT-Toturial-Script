using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class WALLRUN : MonoBehaviour
{
    bool IsWallRunning;

    bool WallOnLeft;
    bool WallOnRight;

    public Rigidbody rb;

    public Transform orientation;
    public Camera cam;

    public ParticleSystem particle;
    

    public float WallRunSpeed;
    public float MaxWallRunDistance;
    public float WallStickStrngth;
    public float MaxVel;
    public float WallRunJump;
    public float wallSwitchStrngth;
    public float RotationCam;
    public float RotationCam2;
    public float RotationCam3;

    public GameObject WallRunUI;

    public LayerMask WallRunLayer;
    
    void Update()
    {
        WallRunInput();

        WallOnRight = Physics.Raycast(transform.position, orientation.right, MaxWallRunDistance, WallRunLayer);
        WallOnLeft = Physics.Raycast(transform.position, -orientation.right, MaxWallRunDistance, WallRunLayer);

        if (WallOnLeft)
        {
            particle.Play();
        }
        else if (WallOnRight)
        {
            particle.Play();
        }


    }

    private void WallRunInput()
    {
        if(Input.GetKey("a") && WallOnLeft)
        {
            SartWR();
          
        }

        if (Input.GetKey("d") && WallOnRight)
        {     
            SartWR();
           
        }
        
        if (Input.GetKeyDown("a") && IsWallRunning && WallOnRight)
        {
            StopWR();
            rb.AddForce(orientation.up * wallSwitchStrngth * Time.deltaTime);
           
            rb.AddForce(-orientation.right * wallSwitchStrngth * Time.deltaTime);
            
            
        } 
        if (Input.GetKeyDown("d") && IsWallRunning && WallOnLeft)
        
        {
            StopWR();
            rb.AddForce(orientation.up * wallSwitchStrngth * Time.deltaTime);
           
            rb.AddForce(orientation.right * wallSwitchStrngth * Time.deltaTime);
            
        }


        else if (!WallOnLeft && !WallOnRight )
        {
            StopWR();
        }
    }

    private void SartWR()
    {
        rb.useGravity = false;
        IsWallRunning = true;

        
       rb.AddForce(orientation.forward * WallRunSpeed * Time.deltaTime);

        WallRunUI.SetActive(true);

        

                if (WallOnRight)
                {
                     rb.AddForce(orientation.right * WallStickStrngth * Time.deltaTime);
            cam.transform.Rotate(Vector3.down, RotationCam * Time.deltaTime);
            cam.transform.Rotate(Vector3.forward, RotationCam2 * Time.deltaTime);
            cam.transform.Rotate(Vector3.left, RotationCam3 * Time.deltaTime);
        }

                if (WallOnLeft)
                {
                    rb.AddForce(-orientation.right * WallStickStrngth * Time.deltaTime);

            cam.transform.Rotate(Vector3.up, RotationCam * Time.deltaTime);
            cam.transform.Rotate(Vector3.forward, RotationCam2 * Time.deltaTime);
            cam.transform.Rotate(Vector3.right, RotationCam3 * Time.deltaTime);
        }
             
    }
    private void StopWR()
    {
        rb.useGravity = true;
        IsWallRunning = false;
        

        WallRunUI.SetActive(false);
    }


}
