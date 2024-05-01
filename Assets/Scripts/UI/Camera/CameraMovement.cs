using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CameraMovement : MonoBehaviour
{
    private Vector3 _touchStart;
    [SerializeField] private float minCameraSize;
    [SerializeField] private float maxCameraSize;
    [SerializeField] private float sensitivity;
	[SerializeField] private float whenHide;
	private Camera _camera;
    private bool _hideTopHouse = true;
    public static Action<bool> onZoomed;

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

        if (_hideTopHouse && _camera.orthographicSize <= whenHide)
        {
            onZoomed.Invoke(true);
            _hideTopHouse = false;
        }
        else if (!_hideTopHouse && _camera.orthographicSize > whenHide)
        {
            onZoomed.Invoke(false);
            _hideTopHouse = true;
        }
    }

    private void CameraZoom(float increment)
    {
        _camera.orthographicSize = Mathf.Clamp(_camera.orthographicSize - increment * sensitivity, minCameraSize, maxCameraSize);
    }
}
