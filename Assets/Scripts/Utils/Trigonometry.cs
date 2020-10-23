using UnityEngine;
using System.Collections;

public class Trigonometry
{
    // Get vector translated by an angle in radians
    public static Vector2 VectorTranslatedByAngle(Vector2 v, float radians)
    {
        float vectorAngle = Mathf.Atan(v.y / v.x) + radians;
        if (v.x < 0)
        {
            vectorAngle += Mathf.PI;
        }
        return new Vector2(
            Mathf.Cos(vectorAngle),
            Mathf.Sin(vectorAngle)
        );
    }
}
