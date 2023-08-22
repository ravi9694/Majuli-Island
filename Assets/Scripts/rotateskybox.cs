using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateskybox : MonoBehaviour
{
    public float rotationSpeed = 0.1f;
    public float verticalLimitAngle = 90f;

    private Vector3 lastMousePosition;
    private float currentVerticalAngle = 0f;

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 deltaMousePosition = Input.mousePosition - lastMousePosition;
            float rotationX = deltaMousePosition.y * rotationSpeed;
            float rotationY = -deltaMousePosition.x * rotationSpeed;
            currentVerticalAngle += rotationX;
            currentVerticalAngle = Mathf.Clamp(currentVerticalAngle, -verticalLimitAngle, verticalLimitAngle);
            transform.rotation = Quaternion.Euler(currentVerticalAngle, transform.rotation.eulerAngles.y + rotationY, 0f);
        }
        if (Input.GetMouseButton(1)){
            
        }

        lastMousePosition = Input.mousePosition;
    }
}