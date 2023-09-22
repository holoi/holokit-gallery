using UnityEngine;
using HoloInteractive.XR.HoloKit;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject m_OperationPanel;

    [SerializeField] private GameObject m_ModelSelectionPanel;

    private bool m_AddButtonFirstPressed = true;

    private HoloKitCameraManager m_HoloKitCameraManager;

    private void Start()
    {
        m_HoloKitCameraManager = FindObjectOfType<HoloKitCameraManager>();
        m_HoloKitCameraManager.OnScreenRenderModeChanged += OnScreenRenderModeChanged;
    }

    private void Update()
    {
        if (m_HoloKitCameraManager.ScreenRenderMode == ScreenRenderMode.Mono)
        {
            if (Screen.orientation != ScreenOrientation.Portrait)
            {
                Screen.orientation = ScreenOrientation.Portrait;
            }
        }
    }

    private void OnScreenRenderModeChanged(ScreenRenderMode renderMode)
    {
        gameObject.SetActive(renderMode == ScreenRenderMode.Mono);
    }

    public void OnCancelButtonPressed()
    {

    }

    public void OnAddButtonPressed()
    {
        m_OperationPanel.SetActive(false);
        m_ModelSelectionPanel.SetActive(true);

        if (m_AddButtonFirstPressed)
        {
            TutorialManager.OnAddButtonPressed?.Invoke();
            m_AddButtonFirstPressed = false;
        }
    }

    public void OnRestoreButtonPressed()
    {

    }

    public void OnObjectPlaced()
    {
        m_OperationPanel.SetActive(true);
        m_ModelSelectionPanel.SetActive(false);
    }
}
