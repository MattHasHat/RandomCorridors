using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public Camera AdventurerCamera;
    public float cameraZoomedInDistance;
    public float cameraZoomedOutDistance;
    private bool cameraZoomedIn = true;

    void Start()
    {
        AdventurerCamera.transform.localPosition = new Vector3(AdventurerCamera.transform.localPosition.x, cameraZoomedInDistance, AdventurerCamera.transform.localPosition.z);
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Z))
        {
            cameraZoomedIn = !cameraZoomedIn;
            float zoomLevel = cameraZoomedInDistance;
            if (!cameraZoomedIn)
            {
                zoomLevel = cameraZoomedOutDistance;
            }
            AdventurerCamera.transform.localPosition = new Vector3(AdventurerCamera.transform.localPosition.x, zoomLevel, AdventurerCamera.transform.localPosition.z);
        }
    }
}
