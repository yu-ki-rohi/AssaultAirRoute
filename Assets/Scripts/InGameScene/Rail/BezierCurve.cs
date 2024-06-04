// yu-ki-rohi
// https://hacchi-man.hatenablog.com/entry/2020/10/18/220000

using System;
using UnityEngine;
[Serializable]
public class BezierCurve
{
    private Vector3[] _positions;
    public BezierCurve(Vector3[] positions)
    {
        _positions = positions;
    }
    public Vector3 Eval(float t)
    {
        return Eval(_positions, t);
    }
    public static Vector3 Eval(Vector3[] pos, float t)
    {
        if (pos == null || pos.Length < 1)
            return Vector3.zero;
        var length = pos.Length;
        if (length == 1)
            return pos[0];
        var r = Vector3.zero;
        for (var i = 0; i < length; i++)
            r += pos[i] * Bernstein(t, length - 1, i);
        return r;
    }
    /// <summary>
    /// バースタイン基底関数
    /// </summary>
    /// <returns></returns>
    private static float Bernstein(float t, int n, int i)
    {
        return nCr(n, i) * Mathf.Pow(t, i) * Mathf.Pow(1 - t, n - i);
    }
    /// <summary>
    /// n 個から r 個を取り出すときの組み合わせの数
    /// </summary>
    private static long nCr(int n, int r)
    {
        return Factorial(n) / (Factorial(r) * Factorial(n - r));
    }
    private static int Factorial(int i)
    {
        if (i <= 1)
            return 1;
        return i * Factorial(i - 1);
    }
}