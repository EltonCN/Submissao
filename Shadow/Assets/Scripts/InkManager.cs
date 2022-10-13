using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class InkManager : MonoBehaviour
{
    [Tooltip("Time interval between new draw point creation (in seconds).")]
    [SerializeField] float newPointInterval = 0.1f;

    [Tooltip("Distance from surface to create new draw point.")]
    [SerializeField] float drawSurfaceDistance = 0.01f;

    LineRenderer lineRenderer;
    float lastTime;

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();

        lineRenderer.positionCount = 0;
        lastTime = 0;
    }

    void OnDisable()
    {
        lineRenderer.positionCount = 0;
    }

    public void ReceiveCursorHit(RaycastHit hit)
    {
        if(!this.enabled)
        {
            return;
        }
        if(Time.time - lastTime < newPointInterval)
        {
            return;
        }

        Vector3 cameraPosition = Camera.main.transform.position;
        Vector3 point2Camera = cameraPosition-hit.point;
        point2Camera = point2Camera.normalized;

        Vector3 linePoint = hit.point + (drawSurfaceDistance * point2Camera);

        lineRenderer.positionCount += 1;
        lineRenderer.SetPosition(lineRenderer.positionCount-1, linePoint);

        lastTime = Time.time;
    }
}
