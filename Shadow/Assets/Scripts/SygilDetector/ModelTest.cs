using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Barracuda;
using System.IO;

public class ModelTest : MonoBehaviour
{
    [SerializeField] NNModel sygilDetectorModel;

    Model sygilDetector;
    IWorker worker;

    // Start is called before the first frame update
    void Start()
    {
        sygilDetector = ModelLoader.Load(sygilDetectorModel);
        worker = WorkerFactory.CreateWorker(WorkerFactory.Type.ComputePrecompiled, sygilDetector);

        List<Vector2> mousePositions = new();

        StreamReader reader = new("Points.csv");

        while(!reader.EndOfStream)
        {
            Vector2 position = new();

            var line = reader.ReadLine();
            var values = line.Split(";");

            position.x = float.Parse(values[0]);
            position.y = float.Parse(values[0]);

            mousePositions.Add(position);
        }
        reader.Close();

        List<Vector2> sampledPoints = PolygonFit.sample(mousePositions);

        PolygonFit.center(sampledPoints);
        PolygonFit.scaleUnit(sampledPoints);

        Tensor input = new Tensor(1, 200);

        int index = 0;
        for(int i = 0; i<100; i++)
        {
            input[0, index] = sampledPoints[i].x;
            input[0, index+1] = sampledPoints[i].y;

            index += 2;
        }

        worker.Execute(input);

        Tensor output = worker.PeekOutput();
 
        float[] outputBuffer = output.ToReadOnlyArray();

        print(outputBuffer[0].ToString()+" "+outputBuffer[1].ToString()+" "+outputBuffer[2].ToString());

        input.Dispose();
    }

    public void OnDestroy()
    {
        worker?.Dispose();
    }
}
