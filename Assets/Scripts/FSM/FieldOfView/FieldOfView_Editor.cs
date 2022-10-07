using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(FieldOfView))]
public class FieldOfView_Editor : Editor
{
    private void OnSceneGUI()
    {
        FieldOfView fov = (FieldOfView)target;

        Handles.color = Color.white;
        Handles.DrawWireArc(fov.transform.position, Vector3.up, Vector3.forward, 360, fov.eyeRadius);

        float degrees = (fov.eyeAngle / 2) + fov.transform.eulerAngles.y;
        float AngleX = Mathf.Sin(degrees * Mathf.Deg2Rad) * fov.eyeRadius;
        float AngleZ = Mathf.Cos(degrees * Mathf.Deg2Rad) * fov.eyeRadius;
        Vector3 vecRight = new Vector3(AngleX, 0, AngleZ);

        degrees = (-fov.eyeAngle / 2) + fov.transform.eulerAngles.y;
        AngleX = Mathf.Sin(degrees * Mathf.Deg2Rad) * fov.eyeRadius;
        AngleZ = Mathf.Cos(degrees * Mathf.Deg2Rad) * fov.eyeRadius;
        Vector3 vecLeft = new Vector3(AngleX, 0, AngleZ);

        Handles.DrawLine(fov.transform.position, fov.transform.position + vecRight);
        Handles.DrawLine(fov.transform.position, fov.transform.position + vecLeft);

        Handles.color = Color.red;
        foreach(Transform visibleTarget in fov.TargetLists)
        {
            Handles.DrawLine(fov.transform.position, visibleTarget.position);
        }

        Handles.color = Color.green;
        if( fov.FirstTarget )
        {
            Handles.DrawLine(fov.transform.position, fov.FirstTarget.position);
        }
    }
}
