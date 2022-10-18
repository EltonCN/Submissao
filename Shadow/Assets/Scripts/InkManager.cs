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

    [Tooltip("Model for sygil detection.")]
    [SerializeField] NNModel sygilDetectorModel;

    LineRenderer lineRenderer;
    float lastTime;

    List<Vector2> mousePositions;

    Model sygilDetector;
    IWorker worker;
    


   // PolygonFit polygonFit;

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();

        lineRenderer.positionCount = 0;
        lastTime = 0;

        mousePositions = new List<Vector2>();

        //polygonFit = new PolygonFit();
        sygilDetector = ModelLoader.Load(sygilDetectorModel);

        worker = WorkerFactory.CreateWorker(WorkerFactory.Type.ComputePrecompiled, sygilDetector);
    }

    public void SwitchEnable()
    {
        this.enabled = !this.enabled;
    }

    void OnDisable()
    {

        /*if(mousePositions.Count > 100)
        {
            int inliers = polygonFit.checkFit(mousePositions);

            if(inliers > 80)
            {
                print("Triangle detected! "+inliers.ToString());
            }
            else
            {
                print("Triangle NOT detected. "+inliers.ToString());
            }
        }*/

        if(mousePositions.Count >50)
        {
            /*StreamWriter file = new("Points.csv", append:true);

            foreach(Vector2 point in mousePositions)
            {
                file.WriteLine(point.x.ToString()+";"+point.y.ToString());
            }
            file.Close();*/
            
            int nPointsToSample = 100;
            if(mousePositions.Count < 100)
            {
                nPointsToSample = mousePositions.Count;
            }

            List<Vector2> sampledPoints = PolygonFit.sample(mousePositions, nPointsToSample);

            PolygonFit.center(sampledPoints);
            PolygonFit.scaleUnit(sampledPoints);

            /*Tensor input = new Tensor(1, 200);

            int index = 0;
            for(int i = 0; i<100; i++)
            {
                input[0, index] = sampledPoints[i].x;
                input[0, index+1] = sampledPoints[i].y;

                index += 2;


            }*/

            Tensor input = new Tensor(1, 100, 100, 1);
            for(int i = 0; i<100; i++)
            {
                for(int j = 0; j<100; j++)
                {
                    input[0,i,j,0] = 0;
                }
            }
            for(int i = 0; i <sampledPoints.Count; i++)
            {
                Vector2 index;
                index.x = (sampledPoints[i].x+1)/2;
                index.y = (sampledPoints[i].y+1)/2;
                
                index *= 100;
                int x = (int) index.x;
                int y = (int) index.y;

                input[0, x, y, 0] = 255;
            }


            worker.Execute(input);

            Tensor output = worker.PeekOutput();
 
            float[] outputBuffer = output.ToReadOnlyArray();


            print(outputBuffer[0].ToString()+" "+outputBuffer[1].ToString()+" "+outputBuffer[4].ToString());
            if(outputBuffer[0] > 0.8)
            {
                print("Triângulo");
            }
            else if(outputBuffer[1] > 0.8)
            {
                print("Quadrado");
            }
    
            input.Dispose();
        }

        lineRenderer.positionCount = 0;
        mousePositions.Clear();
    }

    public void OnDestroy()
    {
        worker?.Dispose();
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
