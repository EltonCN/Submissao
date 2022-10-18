using System.Collections.Generic;
using System;
using UnityEngine;
using Unity.Barracuda;

[Serializable]
public class SigilInfo
{
    public int outputIndex;
    public InkSigil sigil;
}

public class NNSigilDetector : SigilDetector
{
    [Tooltip("Model for sygil detection.")]
    [SerializeField] NNModel sygilDetectorModel;

    [Tooltip("Mapping from the NN output to sigils.")]
    [SerializeField] SigilInfo[] outputToSigil;

    Model sygilDetector;
    IWorker worker;
    Tensor input;

    void Start()
    {
        sygilDetector = ModelLoader.Load(sygilDetectorModel);

        worker = WorkerFactory.CreateWorker(WorkerFactory.Type.ComputePrecompiled, sygilDetector);

        input = new Tensor(1, 100, 100, 1);
    }

    public void OnDestroy()
    {
        worker?.Dispose();
        input?.Dispose();
    }

    void resetInput()
    {
        for(int i = 0; i<100; i++)
        {
            for(int j = 0; j<100; j++)
            {
                input[0,i,j,0] = 0;
            }
        }
    }

    List<Vector2> copyDraw(List<Vector2> draw)
    {
        List<Vector2> copy = new List<Vector2>();

        foreach(Vector2 drawPoint in draw)
        {
            Vector2 copyPoint = new Vector2();
            copyPoint.x = drawPoint.x;
            copyPoint.y = drawPoint.y;

            copy.Add(copyPoint);
        }

        return copy;
    }
    
    public override InkSigil detectSigil(List<Vector2> draw)
    {
        List<Vector2> drawCopy = copyDraw(draw);

        DrawUtils.center(drawCopy);
        DrawUtils.scaleUnit(drawCopy);

        resetInput();
        for(int i = 0; i <drawCopy.Count; i++)
        {
            Vector2 index;
            index.x = (drawCopy[i].x+1)/2;
            index.y = (drawCopy[i].y+1)/2;
            
            index *= 100;
            int x = (int) index.x;
            int y = (int) index.y;

            input[0, x, y, 0] = 255;
        }


        worker.Execute(input);

        Tensor output = worker.PeekOutput();
        float[] outputBuffer = output.ToReadOnlyArray();

        float maxValue = float.MinValue;
        int argMax = -1;

        for(int i = 0; i<outputBuffer.Length; i++)
        {
            if(outputBuffer[i] > maxValue)
            {
                maxValue = outputBuffer[i];
                argMax = i;
            }
        }

        foreach(SigilInfo info in outputToSigil)
        {
            if(info.outputIndex == argMax)
            {
                return info.sigil;
            }
        }

        return null;
        
    }

    void detectPoints()
    {
        /*Tensor input = new Tensor(1, 200);

        int index = 0;
        for(int i = 0; i<100; i++)
        {
            input[0, index] = sampledPoints[i].x;
            input[0, index+1] = sampledPoints[i].y;

            index += 2;


        }*/
    }
}