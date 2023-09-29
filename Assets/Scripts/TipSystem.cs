using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TipSystem : MonoBehaviour
{
    [SerializeField] private GameObject m_Tip1PressTheAddButton;

    [SerializeField] private GameObject m_Tip2TapToPlaceObject;

    private GameObject m_CurrentTip;

    private const float FADE_DURATION = 0.5f;

    private void Start()
    {
        App app = FindObjectOfType<App>();
        app.OnAddButtonPressed += OnAddButtonPressed;
        app.OnObjectPlaced += OnObjectPlaced;

        m_CurrentTip = m_Tip1PressTheAddButton;
    }

    private void OnAddButtonPressed()
    {
        SwitchTip(m_Tip2TapToPlaceObject);
    }

    private void OnObjectPlaced()
    {
        SwitchTip(null);
    }

    private void SwitchTip(GameObject nextTip)
    {
        if (m_CurrentTip == null)
            return;

        var images = m_CurrentTip.GetComponentsInChildren<Image>();
        foreach (var image in images)
        {
            Color color = image.color;
            color.a = 1f;
            image.color = color;
        }
        LeanTween.value(gameObject, 1f, 0f, FADE_DURATION)
            .setOnUpdate((float alpha) =>
            {
                foreach (var image in images)
                {
                    Color color = image.color;
                    color.a = alpha;
                    image.color = color;
                }
            })
            .setOnComplete(() =>
            {
                m_CurrentTip.SetActive(false);
                if (nextTip == null)
                {
                    m_CurrentTip = null;
                    return;
                }

                m_CurrentTip = nextTip;
                m_CurrentTip.SetActive(true);

                var newImages = m_CurrentTip.GetComponentsInChildren<Image>();
                foreach (var image in newImages)
                {
                    Color color = image.color;
                    color.a = 0f;
                    image.color = color;
                }
                LeanTween.value(gameObject, 0f, 1f, FADE_DURATION)
                    .setOnUpdate((float alpha) =>
                    {
                        foreach (var image in newImages)
                        {
                            Color color = image.color;
                            color.a = alpha;
                            image.color = color;
                        }
                    });
            });
    }
}
