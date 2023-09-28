using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct ModelData
{
    public string name;
    public Sprite Image;
    public GameObject Mesh;
}

[CreateAssetMenu(menuName = "ModelLibrary")]
public class ModelLibrary : ScriptableObject
{
    public List<ModelData> Models;
}
