// yu-ki-rohi
// https://hacchi-man.hatenablog.com/entry/2020/10/18/220000

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Rail : MonoBehaviour
{
    [SerializeField]
    private Transform[] _transforms;
    private void OnDrawGizmos()
    {
        if (_transforms == null || _transforms.Length <= 1)
            return;
        var a = _transforms.Select(t => t.position).ToArray();
        var prev = _transforms[0].position;
        for (var i = 0f; i <= 1f; i += 0.01f)
        {
            var pos = BezierCurve.Eval(a, i);
            Gizmos.DrawLine(prev, pos);
            prev = pos;
        }
    }

    public Vector3 GetPos(float time)
    {
        if (_transforms == null || _transforms.Length <= 1)
            return _transforms[0].position;
        time = Mathf.Clamp01(time);
        var a = _transforms.Select(t => t.position).ToArray();
        return BezierCurve.Eval(a, time);
    }
}
