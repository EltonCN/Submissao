using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;

public abstract class PolygonModel
{
    float inlierDistance;

    public PolygonModel(float inlierDistance)
    {
        this.inlierDistance = inlierDistance;
    }

    public abstract List<Vector2> model2verticesFunc(List<Vector2> modelPoints);

    public Tuple<int, List<Vector2>> createModel(List<Vector2> points, List<Vector2> modelPoints)
    {
        int nInliers = 0;

        List<Vector2> vertices = model2verticesFunc(modelPoints);
        int nVertices = vertices.Count();

        foreach(Vector2 point in points)
        {
            for(int i = 0; i<nVertices; i++)
            {
                int index2 = i+1;
                if(index2 == nVertices)
                {
                    index2 = 0;
                }

                float distance = MathUtils.point2lineDistance(vertices[i], vertices[index2], point);

                if(distance < inlierDistance)
                {
                    nInliers += 1;
                    break;
                }
            }
        }

        return Tuple.Create(nInliers, vertices);
    }   
}

public class TriangleModel : PolygonModel
{
    public TriangleModel(float inlierDistance = 0.1f) : base(inlierDistance)
    {
    }

    public override List<Vector2> model2verticesFunc(List<Vector2> modelPoints)
    {
        List<Vector2> vertices = new List<Vector2>(modelPoints);
        
        Vector2 vertice3 = -modelPoints[0] - modelPoints[1];

        vertices.Add(vertice3);

        return vertices;
    }
}

/// <summary>
/// Detect sigils using RANSAC
/// </summary>
public class PolygonFit : SigilDetector
{
    Ransac<Vector2, List<Vector2>> ransac;

    public PolygonFit()
    {
        TriangleModel triangleModel = new TriangleModel();
        ransac = new Ransac<Vector2, List<Vector2>>(2, triangleModel.createModel);
    }

    public int checkFit(List<Vector2> points)
    {
        if(points.Count < 100)
        {
            return 0;
        }

        List<Vector2> sampledPoints = DrawUtils.sample(points);

        DrawUtils.center(sampledPoints);
        DrawUtils.scaleUnit(sampledPoints);

        Tuple<List<Vector2>, int, List<Vector2>> ransacReturn = ransac.fit(sampledPoints);

        return ransacReturn.Item2;
    }

    
    public override InkSigil detectSigil(List<Vector2> draw)
    {
        Debug.LogWarning("PolygonFit not full implemented. Returning null.");
        if(draw.Count > 100)
        {
            int inliers = checkFit(draw);

            if(inliers > 80)
            {
                //Detected shape
                return null;
            }
            else
            {
                return null;
            }
        }

        return null;
    }
}