using UnityEngine;

public class ButtonController : MonoBehaviour
{

    [Header("Overlays")]
    [SerializeField] GameObject startOverlay;
    [SerializeField] GameObject settingOverlay;
    [SerializeField] GameObject inGameOverlay;
    [SerializeField] GameObject endScreen;

    private void Start()
    {
        settingOverlay.SetActive(false);
        inGameOverlay.SetActive(false);
        endScreen.SetActive(false);
        startOverlay.SetActive(true);
    }

    public void OnStartClick()
    {
        EnableDisableOverlay(startOverlay, false);
    }

    public void OnSettingClick()
    {
        EnableDisableOverlay(settingOverlay, true);
    }


    public void EnableDisableOverlay(GameObject obj, bool enable)
    {
        if (enable)
        {
            obj.SetActive(true);
        }
        else
        {
            obj.SetActive(false);
        }
    }
}
