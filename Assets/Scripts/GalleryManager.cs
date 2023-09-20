using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct ModelData
{
    public Sprite Image;

    public GameObject Model;
}

public class GalleryManager : MonoBehaviour
{
    [SerializeField] private List<ModelData> m_Models;

    public List<ModelData> Models => m_Models;

    private GameObject m_SelectedModel;

    private GameObject m_SpawnedModel;

    private void Start()
    {
        // Set the default model
        m_SelectedModel = m_Models[0].Model;
    }

    public void OnModelSelected(int idx)
    {
        m_SelectedModel = m_Models[idx].Model;
        if (m_SpawnedModel != null)
        {
            Vector3 position = m_SpawnedModel.transform.position;
            Quaternion rotation = m_SpawnedModel.transform.rotation;

            Destroy(m_SpawnedModel);
            InstantiateSelectedModel(position, rotation);
        }
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
