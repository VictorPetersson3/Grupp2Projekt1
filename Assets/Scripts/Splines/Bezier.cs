using UnityEngine;

public static class Bezier
{
    public static Vector2 EvaluateQuadratic (Vector2 aA, Vector2 aB, Vector2 aC, float aT)
    {
        Vector2 p0 = Vector2.Lerp(aA, aB, aT);
        Vector2 p1 = Vector2.Lerp(aB, aC, aT);
        return Vector2.Lerp(p0, p1, aT);
    }

    public static Vector2 EvaluateCubic(Vector2 aA, Vector2 aB, Vector2 aC, Vector2 aD, float aT)
    {
        Vector2 p0 = EvaluateQuadratic(aA, aB, aC, aT);
        Vector2 p1 = EvaluateQuadratic(aB, aC, aD, aT);
        return Vector2.Lerp(p0, p1, aT);
    }
}