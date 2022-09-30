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
        float angleX = Mathf.Sin(degrees * Mathf.Deg2Rad) * fov.eyeRadius;
        float angleZ = Mathf.Cos(degrees * Mathf.Deg2Rad) * fov.eyeRadius;
        Vector3 vecRight = new Vector3(angleX, 0, angleZ);

        degrees = (-fov.eyeAngle / 2) + fov.transform.eulerAngles.y;
        angleX = Mathf.Sin(degrees * Mathf.Deg2Rad) * fov.eyeRadius;
        angleZ = Mathf.Cos(degrees * Mathf.Deg2Rad) * fov.eyeRadius;
        Vector3 vecLeft = new Vector3(angleX, 0, angleZ);

        Handles.DrawLine(fov.transform.position, fov.transform.position + vecRight);
        Handles.DrawLine(fov.transform.position, fov.transform.position + vecLeft);

        Handles.color = Color.red;
        foreach(Transform visibleTarget in fov.TargetLists)
        {
            Handles.DrawLine(fov.transform.position, visibleTarget.position);
        }

        Handles.color = Color.green;
        if(fov.FirstTarget)
        {
            Handles.DrawLine(fov.transform.position, fov.FirstTarget.position);
        }
    }


}
