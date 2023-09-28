using System;
using UnityEngine;

public class ModelSlotUIController : MonoBehaviour
{
    private GameObject m_SelectionVisualizer;

    private static event Action<ModelSlotUIController> OnSelectedEvent;

    private void Start()
    {
        m_SelectionVisualizer = transform.GetChild(0).gameObject;

        OnSelectedEvent += OnSelectedFunc;
    }

    private void OnDestroy()
    {
        OnSelectedEvent -= OnSelectedFunc;
    }

    private void OnSelectedFunc(ModelSlotUIController obj)
    {
        if (this != obj)
        {
            OnDeselected();
        }
    }

    public void OnSelected()
    {
        if (m_SelectionVisualizer != null)
            m_SelectionVisualizer.SetActive(true);
        OnSelectedEvent?.Invoke(this);
    }

    public void OnDeselected()
    {
        if (m_SelectionVisualizer != null)
            m_SelectionVisualizer.SetActive(false);
    }

    public void Initialize()
    {
        if (m_SelectionVisualizer == null)
        {
            m_SelectionVisualizer = transform.GetChild(0).gameObject;
        }
        m_SelectionVisualizer.SetActive(true);

        OnSelectedEvent?.Invoke(this);
    }
}
