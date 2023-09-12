using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct ModelData
{
    public Sprite m_Image;

    public GameObject m_Model;
}

public class GalleryManager : MonoBehaviour
{
    [SerializeField] private List<ModelData> m_Models;

    private GameObject m_SelectedModel;

    private GameObject m_SpawnedModel;

    private void Start()
    {
        
    }

    public void OnModelSelected(int idx)
    {
        m_SpawnedModel = m_Models[idx].m_Model;
    }

    public void InstantiateSelectedModel(Vector3 position, Quaternion rotation)
    {
        if (m_SpawnedModel != null)
        {
            Destroy(m_SpawnedModel);
        }
        m_SpawnedModel = Instantiate(m_SelectedModel, position, rotation);
    }
}
