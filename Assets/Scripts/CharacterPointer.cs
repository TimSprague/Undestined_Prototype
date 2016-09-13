using UnityEngine;
using System.Collections;

public class CharacterPointer : MonoBehaviour {

    private const float Y_ANGLE_MAX = 0f;
    private const float Y_ANGLE_MIN = 0f;

    public GameObject target;
    public Transform camTransform;
    public Transform characterTransform;

    private float distance = 2.5f;
    private float currentX = 0.0f;
    private float currentY = 0.0f;
    private float camSensitivity = 10.0f;
    //private int Inverted = 1;

    // Use this for initialization
    void Start () {

        camTransform = transform;
        currentX = characterTransform.rotation.eulerAngles.y;
        currentY = 15;
    }
	
	// Update is called once per frame
	void Update () {
        if (camSensitivity < 0)
        {
            camSensitivity = 0;
        }

        distance = Raycast3.distance3;

        if (distance > 5.0)
        {
            distance = 5.0f;
        }

        currentX += (Input.GetAxis("Mouse X") * camSensitivity * 10);
        currentY += (Input.GetAxis("Mouse Y") * camSensitivity);

        currentY = Mathf.Clamp(currentY, Y_ANGLE_MIN, Y_ANGLE_MAX);
    }

    void LateUpdate()
    {
        Vector3 dir = new Vector3(0.0f, 0.0f, distance);
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        camTransform.position = target.transform.position + rotation * dir;
        camTransform.LookAt(target.transform.position);
    }
}
