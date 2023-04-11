using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Line : MonoBehaviour
{
    public float        pointResolution = 0.1f;
    public LineRenderer lineRenderer;

    private List<Vector3> points;

    public void UpdateLine(Vector3 position)
    {
        if (points == null)
        {
            points = new List<Vector3>();
            SetPoint(position);
            return;
        }

        if (points.Count < 1)
        {
            SetPoint(position);
            return;
        }

        if (Vector2.Distance(points.Last(), position) > pointResolution) SetPoint(position);
    }
    
    void SetPoint(Vector3 point)
    {
        points.Add(point);

        lineRenderer.positionCount = points.Count;
        lineRenderer.SetPosition(points.Count - 1, point);
    }

    private void OnDisable()
    {
        points.Clear();
    }

}
