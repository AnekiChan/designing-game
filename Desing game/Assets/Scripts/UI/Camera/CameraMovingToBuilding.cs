using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovingToBuilding : MonoBehaviour
{
    private Camera _camera;
    //[SerializeField] GameMachine machine;
    private Vector3 _mainBuildingPosition;
    [SerializeField] private float _speed = 3f;

    private void Awake()
    {
        _camera = GetComponent<Camera>();
        //_mainBuildingPosition = machine.building.transform.position;
        _mainBuildingPosition.z = _camera.transform.position.z;
    }

    private void Update()
    {
        _camera.transform.position = Vector3.MoveTowards(_camera.transform.position, _mainBuildingPosition, _speed * Time.deltaTime);
        if ((Vector2)_camera.transform.position == (Vector2)_mainBuildingPosition)
        {
            this.enabled = false;
        }
    }


}
