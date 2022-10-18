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

public class PolygonFit
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

        List<Vector2> sampledPoints = sample(points);

        center(sampledPoints);
        scaleUnit(sampledPoints);

        Tuple<List<Vector2>, int, List<Vector2>> ransacReturn = ransac.fit(sampledPoints);

        return ransacReturn.Item2;
    }

    static public void center(List<Vector2> points)
    {
        Vector2 centroid = Vector2.zero;

        foreach(Vector2 point in points)
        {
            centroid += point;
        }

        centroid /= points.Count;

        for(int i = 0; i<points.Count; i++)
        {
            points[i] -= centroid;
        }
    }

    static public void scaleUnit(List<Vector2> points)
    {
        float xMin = float.MaxValue, 
              xMax = float.MinValue, 
              yMin = float.MaxValue, 
              yMax = float.MinValue;

        foreach(Vector2 point in points)
        {
            if(point.x < xMin)
            {
                xMin = point.x;
            }
            if(point.y < yMin)
            {
                yMin = point.y;
            }
            if(point.x > xMax)
            {
                xMax = point.x;
            }
            if(point.y > yMax)
            {
                yMax = point.y;
            }
        }

        float x_range = xMax - xMin;
        float y_range = yMax - yMin;

        float scale;

        if (x_range > y_range)
        {
            scale = 1/x_range;
        }
        else
        {
            scale = 1/y_range;
        }

        for(int i = 0; i<points.Count; i++)
        {
            points[i] *= scale;
        }
            
    }

    static public List<Vector2> sample(List<Vector2> points, int nPointsToSample=100)
    {
        List<Vector2> sampledPoints = new List<Vector2>();

        int nPoint = points.Count;


        List<int> indexes = new List<int>();
        while(indexes.Count < nPointsToSample)
        {
            int index = UnityEngine.Random.Range(0, nPoint);

            if(!indexes.Any(item => item == index))
            {
                indexes.Add(index);
            }
        }

        indexes.Sort();

        for(int i =0; i<nPointsToSample; i++)
        {
            sampledPoints.Add(points[indexes[i]]);
        }

        return sampledPoints;
    }

}