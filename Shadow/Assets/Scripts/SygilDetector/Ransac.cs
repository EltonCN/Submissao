using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;

public class Ransac<T_point, T_model>
{
    int n;
    float success_rate;
    float p_inlier;
    int k;
    Func<List<T_point>, List<T_point>, Tuple<int, T_model>> model_function;


    public Ransac(int n, Func<List<T_point>, List<T_point>, Tuple<int, T_model>> model_function, float success_rate=0.9f, float p_inlier=0.8f)
    {
        this.n = n;
        this.model_function = model_function;
        this.success_rate = success_rate;
        this.p_inlier = p_inlier;

        this.k = (int) (Mathf.Log10(1-success_rate)/Mathf.Log10(1-Mathf.Pow(p_inlier, n)));
        
    }

    public Tuple<List<T_point>, int, T_model> fit(List<T_point> points)
    {
        int n_point = points.Count;

        int best_inliers = -1;
        List<T_point> best_points = null;
        T_model best_model = default(T_model);

        for(int i = 0; i<k; i++)
        {
            List<int> indexes = new List<int>();
            List<T_point> model_points = new List<T_point>();
            while(indexes.Count < n)
            {
                int index = UnityEngine.Random.Range(0, n_point);

                if(!indexes.Any(item => item == index))
                {
                    indexes.Add(index);
                    model_points.Add(points[index]);
                }
            }

            Tuple<int, T_model> model_return = model_function(points, model_points);
            int n_inliers = model_return.Item1;
            T_model model = model_return.Item2;

            if(n_inliers > best_inliers)
            {
                best_inliers = n_inliers;
                best_points = model_points;
                best_model = model;
            }

            
            
        }

        return Tuple.Create(best_points, best_inliers, best_model);
    }
}