using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    private App app;

    private void Start()
    {
        app = FindObjectOfType<App>();
    }

    public void Spawn()
    {
        app.OnPlaced(Vector3.zero, Quaternion.identity);
    }
}
