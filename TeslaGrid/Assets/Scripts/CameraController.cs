using System.Collections;
using UnityEngine;

public class CameraController : MonoBehaviour 
{
    public float speed;
    public float zoomSpeed = 1;
    public float targetOrtho;
    public float smoothSpeed = 2.0f;
    public float minOrtho = 1.0f;
    public float maxOrtho = 20.0f;

    void Start()
    {
        targetOrtho = Camera.main.orthographicSize;
    }

    private void Update()
    {
        float xAxisValue = Input.GetAxis("Horizontal");
        float yAxisValue = Input.GetAxis("Vertical");
        if (Camera.main != null)
        {
            Camera.main.transform.Translate(new Vector3(xAxisValue*speed, yAxisValue*speed)*Time.deltaTime);
        }
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0.0f)
        {
            targetOrtho -= scroll * zoomSpeed;
            targetOrtho = Mathf.Clamp(targetOrtho, minOrtho, maxOrtho);
        }

        Camera.main.orthographicSize = Mathf.MoveTowards(Camera.main.orthographicSize, targetOrtho, smoothSpeed * Time.deltaTime);

    }

}