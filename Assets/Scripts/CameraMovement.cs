using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {

    private float speed = 15.0f;
    private float zoomSpeed = 2.0f;

    private Vector3 lastposition = new Vector3(255, 255, 255);

    public bool smooth = true;

    Camera m_camera;
    GameObject currentEnemy;

    void Start()
    {
        m_camera = Camera.main;
    }

    void Update()
    {

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        transform.Translate(0, scroll * zoomSpeed, scroll * zoomSpeed, Space.World);

        if (Input.GetMouseButton(1))
        {
            lastposition = Input.mousePosition - lastposition;
            lastposition = new Vector3(transform.eulerAngles.x + lastposition.y, transform.eulerAngles.y + lastposition.x, 0);
            transform.eulerAngles = lastposition;

            lastposition = Input.mousePosition;
        }

        Vector3 dir = new Vector3();

        if (Input.GetKey(KeyCode.W))
        {
            dir.z += speed;
        }
        if (Input.GetKey(KeyCode.S))
        {
            dir.z -= speed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            dir.x += speed;
        }
        if (Input.GetKey(KeyCode.A))
        {
            dir.x -= speed;
        }
        

        dir.Normalize();

        transform.Translate(dir * speed * Time.deltaTime);

        if(Input.GetKey(KeyCode.UpArrow))
        {
            transform.Rotate(Vector3.right * speed * Time.deltaTime);
        }

        if(Input.GetKey(KeyCode.DownArrow))
        {
            transform.Rotate(-Vector3.right * speed * Time.deltaTime);
        }

        if(Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(-Vector3.up * speed * Time.deltaTime);
        }

        if(Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(Vector3.up * speed * Time.deltaTime);
        }

        Ray ray = m_camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit) && hit.transform.gameObject.tag == "Enemy")
        {
            Debug.Log("Found a target");
            hit.transform.gameObject.GetComponentInChildren<Canvas>(true).gameObject.SetActive(true);
            currentEnemy = hit.transform.gameObject;
        }
        else
        {
            if(currentEnemy != null)
                currentEnemy.transform.gameObject.GetComponentInChildren<Canvas>().gameObject.SetActive(false);
            currentEnemy = null;
        }
        
    }
}
