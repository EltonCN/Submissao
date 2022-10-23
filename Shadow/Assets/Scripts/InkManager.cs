using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using System.IO;
using UnityEngine;
using Unity.Barracuda;

[RequireComponent(typeof(LineRenderer))]
public class InkManager : MonoBehaviour
{
    [Tooltip("Time interval between new draw point creation (in seconds).")]
    [SerializeField] float newPointInterval = 0.1f;

    [Tooltip("Distance from surface to create new draw point.")]
    [SerializeField] float drawSurfaceDistance = 0.01f;

    [SerializeField] SigilDetector sigilDetector;

    LineRenderer lineRenderer;
    float lastTime;
    List<Vector2> mousePositions;


    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();

        lineRenderer.positionCount = 0;
        lastTime = 0;

        mousePositions = new List<Vector2>();

        
    }

    public void SwitchEnable()
    {
        this.enabled = !this.enabled;
    }

    void OnDisable()
    {


        if(mousePositions.Count >50 && sigilDetector != null)
        {
            InkSigil detectedSigil = sigilDetector.detectSigil(mousePositions);
        }

        lineRenderer.positionCount = 0;
        mousePositions.Clear();
    }

    

    public void ReceiveCursorHit(RaycastHit hit, Vector2 position)
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

        mousePositions.Add(position);

        lastTime = Time.time;
    }
}
