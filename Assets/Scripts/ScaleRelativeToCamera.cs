using UnityEngine;
using System.Collections;

public class ScaleRelativeToCamera : MonoBehaviour {
    
    public Camera cam;
    public float objectScale = 1.0f;
    private Vector3 initialScale;

    // Use this for initialization
    void Start()
    {
        // record initial scale, use this as a basis
        initialScale = transform.localScale;

        // if no specific camera, grab the default camera
        if (cam == null)
            cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Plane plane = new Plane(cam.transform.forward, cam.transform.position);
        float dist = plane.GetDistanceToPoint(transform.position);
        transform.localScale = initialScale * dist * objectScale;
    }
}
