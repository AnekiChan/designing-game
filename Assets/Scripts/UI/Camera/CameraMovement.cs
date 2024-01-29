using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Vector3 _touchStart;
    [SerializeField] private float minCameraSize;
    [SerializeField] private float maxCameraSize;
    [SerializeField] private float sensitivity;
    private Camera _camera;

    void Start()
    {
        _camera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(2))
        {
            _touchStart = _camera.ScreenToWorldPoint(Input.mousePosition);
        }
        else if (Input.GetMouseButton(2))
        {
            Vector3 direction = _touchStart - _camera.ScreenToWorldPoint(Input.mousePosition);
            _camera.transform.position += direction;
        }

        CameraZoom(Input.GetAxis("Mouse ScrollWheel"));
    }

    private void CameraZoom(float increment)
    {
        _camera.orthographicSize = Mathf.Clamp(_camera.orthographicSize - increment * sensitivity, minCameraSize, maxCameraSize);
    }
}
