using System;
using UnityEngine;

public class ModelSlotController : MonoBehaviour
{
    [SerializeField] private GameObject m_Circle;

    private static event Action<ModelSlotController> OnModelSelected;

    private void Start()
    {
        OnModelSelected += OnModelSelectedFunc; 
    }

    private void OnDestroy()
    {
        OnModelSelected -= OnModelSelectedFunc;
    }

    private void OnModelSelectedFunc(ModelSlotController modelSlot)
    {
        if (modelSlot != this)
        {
            m_Circle.SetActive(false);
        }
    }

    public void OnSelected()
    {
        m_Circle.SetActive(true);
        OnModelSelected?.Invoke(this);
    }
}
