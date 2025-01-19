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
        SpotifyController.Instance.ResetSpotify();
    }

    public void OnStartClick()
    {
        EnableDisableOverlay(startOverlay, false);
    }

    public void OnSettingClick()
    {
        EnableDisableOverlay(settingOverlay, true);
    }

    public void OnNextSongClick()
    {
        Debug.Log("next song");
        SpotifyController.Instance.NextSong();
    }

    public void OnPlaySongClick()
    {
        Debug.Log("playing song");
        SpotifyController.Instance.PlaySong("spotify:track:01kfSdF9zfcDLri5sSWEoL");
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
