using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraSettings : MonoBehaviour
{
    private CinemachineVirtualCamera cam;

    private void Start()
    {
        cam = GetComponent<CinemachineVirtualCamera>();
    }

    private void Update()
    {

        if (Input.GetMouseButton(1))
        {
            cam.m_Lens.FieldOfView = 20;
        }
        else
        {
            cam.m_Lens.FieldOfView = 35;

        }
    }
}
