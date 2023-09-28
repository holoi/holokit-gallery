using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionPanelUIController : MonoBehaviour
{
    [SerializeField] private ModelSlotUIController m_ModelSlotPrefab;

    [SerializeField] private RectTransform m_Root;

    private void Start()
    {
        App app = FindObjectOfType<App>();

        int idx = 0;
        foreach (var modelData in app.ModelLibrary.Models)
        {
            var slotInstance = Instantiate(m_ModelSlotPrefab, m_Root);
            slotInstance.transform.localScale = Vector3.one;

            int currentIdx = idx;
            slotInstance.GetComponent<Button>().onClick.AddListener(() =>
            {
                app.OnModelSelected(currentIdx);
                slotInstance.OnSelected();
            });
            slotInstance.GetComponent<Image>().sprite = modelData.Image;

            // Select the first model by default
            if (idx == 0)
                slotInstance.Initialize();

            idx++;
        }
    }
}
