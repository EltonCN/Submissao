using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;

public static class DrawUtils
{
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