using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class InkManager : MonoBehaviour
{
    LineRenderer lineRenderer;

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();

        lineRenderer.positionCount = 0;
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

        lineRenderer.positionCount += 1;
        lineRenderer.SetPosition(lineRenderer.positionCount-1, hit.point);
    }
}
