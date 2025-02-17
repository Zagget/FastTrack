using System.Collections.Generic;
using UnityEngine;

public class SpotifyController : MonoBehaviour
{
    private static SpotifyController instance;
    public static SpotifyController Instance { get { return instance; } }

    private static AndroidJavaObject mainActivity;

    public List<string> PlayedSongs = new List<string>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        // Get the Android MainActivity instance
        using (AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        {
            mainActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            Menu();
        }
    }

    public void Menu()
    {
        mainActivity.Call("Menu");
    }

    public void PlaySong(string trackId)
    {
        if (PlayedSongs.Contains(trackId))
        {
            Debug.Log("This song has already been played");
            return;
        }

        mainActivity.Call("PlaySong", trackId);
        PlayedSongs.Add(trackId);
    }

    public void ResetSpotify()
    {
        mainActivity.Call("ResetSpotify");
    }

    public void NextSong()
    {
        mainActivity.Call("SkipToNextSong");
    }
}