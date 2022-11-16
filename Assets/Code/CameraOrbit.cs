using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOrbit : MonoBehaviour
{
    public float _mouseSensitivity = 3.0f;

    private float _rotationY;
    private float _rotationX;

    public Transform _target;

    public float _distanceFromTarget = 3f;

    private Vector3 _currentRotation;
    private Vector3 _smoothVelocity = Vector3.zero;

    public float _smoothTime = 0.2f;

    public Vector2 _rotationXMinMax = new Vector2(-40, 40);

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * _mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * _mouseSensitivity;

        _rotationY += mouseX;
        _rotationX += mouseY;

        // Apply clamping for x rotation 
        _rotationX = Mathf.Clamp(_rotationX, _rotationXMinMax.x, _rotationXMinMax.y);
        _rotationY = Mathf.Clamp(_rotationY, _rotationXMinMax.x, _rotationXMinMax.y);

        Vector3 nextRotationX = new Vector3(_rotationX, _rotationY);

        // Apply damping between rotation changes
        _currentRotation = Vector3.SmoothDamp(_currentRotation, nextRotationX, ref _smoothVelocity, _smoothTime);
        transform.localEulerAngles = _currentRotation;

        // Substract forward vector of the GameObject to point its forward vector to the target
        transform.position = _target.position + Vector3.up * 2 - transform.forward * _distanceFromTarget;
    }
}