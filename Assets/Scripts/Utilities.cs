using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Threading;

public class Utilities : MonoBehaviour
{
    public static bool isCloseEpsilonVec3(Vector3 x, Vector3 y, float epsilon = 0.5f)
    {
        if (Vector3.Distance(x, y) < epsilon)
            return true;
        else
            return false;
    }

    public static bool isCloseEpsilonf(float x, float y, float epsilon = 0.5f)
    {
        if (Mathf.Abs(x-y) < epsilon)
            return true;
        else
            return false;
    }



    public static float crossProductVector2(Vector2 v, Vector2 w)
    {
        return v.x*w.y-v.y*w.x; 
    }

    public static bool haveSameSign(float x, float y)
    {
        return ((x >= 0 && y >= 0) || (x <= 0 && y <= 0));
    }

    public static Vector2 rotate(Vector2 pointA, Vector2 pointB ,float theta, float scale=1)
    {
        
        Vector2 AB = scale *(pointB - pointA);
        //convert degree to radian
        theta *= (Mathf.PI / 180);
        Vector2 afterRotation = new Vector2(Mathf.Cos(theta)*AB.x - Mathf.Sin(theta) * AB.y, Mathf.Sin(theta) * AB.x + Mathf.Cos(theta) * AB.y);

        return afterRotation + pointA;
    }

    public static void instantiateSphereAtPosition(Vector3 position)
    {
        GameObject Sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        Sphere.transform.localScale /= 10;
        Instantiate(Sphere, position, Quaternion.identity);
    }



    public static T Choice<T>(IList<T> sequence, float[] distribution, float randomNumber = -1.0f)
    {
        float sum = 0;

        var cumulative = distribution.Select(c => {
            var result = c + sum;
            sum += c;
            return result;
        }).ToList();


        // now generate random double. It will always be in range from 0 to 1
        float r;
        if (randomNumber == -1.0f)
            r = Random.Range(0.0f, 1.0f);
        else
            r = randomNumber;

        var idx = cumulative.BinarySearch(r);

        if (idx < 0)
            idx = ~idx;
        if (idx > cumulative.Count - 1)
            idx = cumulative.Count - 1; 

        return sequence[idx];
        
    }

}


[System.Serializable]
public class Pair<T, U>
{
    public Pair(T first, U second)
    {
        this.first = first;
        this.second = second;
    }

    public T first { get; set; }
    public U second { get; set; }
};