using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    public Color pathColor = Color.green;
    Transform[] objArray;
    public List<Transform> pathOBJList = new List<Transform>();
    [Range(1, 20)] public int lineDensity = 1;
    int overload;
    public List<Vector3> bezierObjList = new List<Vector3>();
    public bool visualizePath;
    private void Start()
    {
        CreatePath();
    }
    private void OnDrawGizmos()
    {
        if (visualizePath)
        {
            //straignt Path
            Gizmos.color = pathColor;

            objArray = GetComponentsInChildren<Transform>();
            //clearOBJ
            pathOBJList.Clear();
            //all children into list
            foreach (Transform obj in objArray)
            {
                if (obj != this.transform)
                {
                    pathOBJList.Add(obj);
                }
            }
            //draw the object
            for (int i = 0; i < pathOBJList.Count; i++)
            {
                Vector3 position = pathOBJList[i].position;
                if (i > 0)
                {
                    Vector3 preivous = pathOBJList[i - 1].position;
                    Gizmos.DrawLine(preivous, position);

                }
                Gizmos.DrawWireSphere(position, 0.3f);
            }
            //curved path
            //check overload
            if (pathOBJList.Count % 2 == 0)
            {
                pathOBJList.Add(pathOBJList[pathOBJList.Count - 1]);
                overload = 2;
            }
            else
            {

                pathOBJList.Add(pathOBJList[pathOBJList.Count - 1]);

                pathOBJList.Add(pathOBJList[pathOBJList.Count - 1]);
                overload = 3;
            }

            bezierObjList.Clear();
            Vector3 lineStart = pathOBJList[0].position;
            for (int i = 0; i < pathOBJList.Count - overload; i += 2)
            {
                for (int j = 0; j <= lineDensity; j++)
                {
                    Vector3 lineEnd = GetPoint(pathOBJList[i].position, pathOBJList[i + 1].position, pathOBJList[i + 2].position, j / (float)lineDensity);
                    bezierObjList.Add(lineStart);
                    Gizmos.color = Color.red;
                    Gizmos.DrawLine(lineStart, lineEnd);
                    Gizmos.color = Color.blue;
                    Gizmos.DrawWireSphere(lineStart, 0.1f);
                    lineStart = lineEnd;

                }
            }
        }
        else
        {
            pathOBJList.Clear();
            bezierObjList.Clear();
        }

    }
    void CreatePath()
    {
        objArray = GetComponentsInChildren<Transform>();
        //clearOBJ
        pathOBJList.Clear();
        //all children into list
        foreach (Transform obj in objArray)
        {
            if (obj != this.transform)
            {
                pathOBJList.Add(obj);
            }
        }
        //draw the object
        for (int i = 0; i < pathOBJList.Count; i++)
        {
            Vector3 position = pathOBJList[i].position;
            if (i > 0)
            {
                Vector3 preivous = pathOBJList[i - 1].position;
            }
          
        }
        //curved path
        //check overload
        if (pathOBJList.Count % 2 == 0)
        {
            pathOBJList.Add(pathOBJList[pathOBJList.Count - 1]);
            overload = 2;
        }
        else
        {
            pathOBJList.Add(pathOBJList[pathOBJList.Count - 1]);
            pathOBJList.Add(pathOBJList[pathOBJList.Count - 1]);
            overload = 3;
        }

        bezierObjList.Clear();
        Vector3 lineStart = pathOBJList[0].position;
        for (int i = 0; i < pathOBJList.Count - overload; i += 2)
        {
            for (int j = 0; j <= lineDensity; j++)
            {
                Vector3 lineEnd = GetPoint(pathOBJList[i].position, pathOBJList[i + 1].position, pathOBJList[i + 2].position, j / (float)lineDensity);
                bezierObjList.Add(lineStart);
                lineStart = lineEnd;

            }
        }
    }
    
    Vector3 GetPoint(Vector3 p0, Vector3 p1, Vector3 p2, float t)
    {
        return Vector3.Lerp(Vector3.Lerp(p0, p1, t), Vector3.Lerp(p1, p2, t), t);
    }
}
