using UnityEngine;

public static class MathUtils
{
    public static float point2lineDistance(Vector2 linePoint1, Vector2 linePoint2, Vector2 point)
    {
        float distance = (linePoint2[0]-linePoint1[0])*(linePoint1[1]-point[1]);
        distance -= (linePoint1[0]-point[0])*(linePoint2[1]-linePoint1[1]);
        distance = Mathf.Abs(distance);

        float divider = Mathf.Pow((linePoint2[0]-linePoint1[0]), 2);
        divider += Mathf.Pow((linePoint2[1]-linePoint1[1]), 2);
        divider = Mathf.Sqrt(divider);

        distance /= divider;

        return distance;
    }
}