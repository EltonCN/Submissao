using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[ExecuteInEditMode]
public class PortalCamera : MonoBehaviour
{
    [SerializeField] Camera portalCamera;
    [SerializeField] Transform pairPortal;
    [SerializeField] Vector3 scale = new Vector3(-1, 1, -1);

    void OnEnable()
    {
        RenderPipelineManager.beginCameraRendering += UpdateCamera;
    }
    void OnDisable()
    {
        RenderPipelineManager.beginCameraRendering -= UpdateCamera;
    }
    
    void UpdateCamera(ScriptableRenderContext context, Camera camera)
    {
        if(portalCamera == null || pairPortal == null)
        {
            return;
        }

        //if((camera.cameraType == CameraType.Game || camera.cameraType == CameraType.SceneView) && camera.tag != "Portal")
        if(camera.cameraType == CameraType.Game && camera.tag != "Portal")
        {
            portalCamera.projectionMatrix = camera.projectionMatrix;

            Vector3 relativePosition = transform.InverseTransformDirection(camera.transform.position);
            relativePosition = Vector3.Scale(relativePosition, scale);
            portalCamera.transform.position = pairPortal.TransformPoint(relativePosition);

            Vector3 relativeRotation = transform.InverseTransformDirection(camera.transform.forward);
            relativeRotation = Vector3.Scale(relativeRotation, new Vector3(-1, -1, -1));
            portalCamera.transform.forward = pairPortal.TransformDirection(relativeRotation);
        }
    }

}
