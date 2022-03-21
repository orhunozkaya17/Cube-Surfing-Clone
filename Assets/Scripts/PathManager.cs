using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathManager : MonoBehaviour
{
    public static PathManager instance;
    public List<Path> paths = new List<Path>();

    int currentPathIndex = -1;
    private void Awake()
    {
        instance = this;
    }
    public Path GetPath()
    {
        currentPathIndex++;
        if (currentPathIndex>=paths.Count)
        {
            return null;
        }
        return paths[currentPathIndex];
    }

}
