﻿using UnityEngine;
using System.Collections;

public class CameraOrbit : MonoBehaviour {


    private const float Y_ANGLE_MAX = 25.0f;
    private const float Y_ANGLE_MIN = 25.0f;

    public GameObject target;
    public Transform camTransform;
    public Transform characterTransform;

    private float distance = 2.5f;
    private float currentX = 0.0f;
    private float currentY = 0.0f;
    private float camSensitivity = 10.0f;
    //private int Inverted = 1;


    // Use this for initialization
    void Start()
    {
        camTransform = transform;
        currentX = characterTransform.rotation.eulerAngles.y;
        currentY = 15;
    }


    void Update()
    {
        //if (Input.GetButtonDown("Right Stick Push"))
        //{
        //    currentX = characterTransform.rotation.eulerAngles.y;
        //    currentY = 15;
        //    distance = 2.5f;
        //}


        //if (Input.GetButton("SwitchClass") && Input.GetButton("Right Bumper") && Input.GetButtonDown("Y Button"))
        //{
        //    camSensitivity += .25f;
        //    Debug.Log("camSensitivity is " + camSensitivity.ToString());
        //    Debug.Log("test");
        //}
        //if (Input.GetButton("SwitchClass") && Input.GetButton("Right Bumper") && Input.GetButtonDown("A Button"))
        //{
        //    camSensitivity -= .25f;
        //    Debug.Log("camSensitivity is " + camSensitivity.ToString());
        //    Debug.Log("test");
        //}


        if (camSensitivity < 0)
        {
            camSensitivity = 0;
        }

        distance = Raycast3.distance3;

        if (distance > 5.0)
        {
            distance = 5.0f;
        }

        currentX += (Input.GetAxis("Mouse X") * camSensitivity * 10) ;
        currentY += (Input.GetAxis("Mouse Y") * camSensitivity);

        currentY = Mathf.Clamp(currentY, Y_ANGLE_MIN, Y_ANGLE_MAX);
    }

    public void cameraSnap()
    {
        currentX = characterTransform.rotation.eulerAngles.y;
        currentY = 15;
        distance = 2.5f;
    }

    // LateUpdate is called once per frame after Update
    void LateUpdate()
    {
        Vector3 dir = new Vector3(0.0f, 0.0f, -distance);
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        camTransform.position = target.transform.position + rotation * dir;
        camTransform.LookAt(target.transform.position);
    }


    //public void InceaseSensitivity(float _Amount)
    //{
    //    camSensitivity += _Amount;
    //}

    //public void DecreaseSensitivity(float _Amount)
    //{
    //    camSensitivity -= _Amount;
    //}


    //public void SetInvertedControls(int _invert)
    //{
    //    Inverted = _invert;
    //}

    //public int GetInverted()
    //{
    //    return Inverted;
    //}

    //public float GetSensitivity()
    //{

    //    return camSensitivity;
    //}
}
