package com.unity3d.player;

import android.os.Bundle;
import com.unity3d.player.UnityPlayerActivity;
import android.content.Intent;
import android.content.IntentFilter;

import android.util.Log;

import com.spotify.android.appremote.api.ConnectionParams;
import com.spotify.android.appremote.api.Connector;
import com.spotify.android.appremote.api.SpotifyAppRemote;
import com.spotify.android.appremote.api.PlayerApi;

import com.spotify.protocol.client.Subscription;
import com.spotify.protocol.types.PlayerState;
import com.spotify.protocol.types.Repeat;
import com.spotify.protocol.types.Track;

public class MainActivity extends UnityPlayerActivity {
    public static MainActivity instance; // For Unity C# to trigger functions here
    public static String currentPlaying = ""; // For Unity C# to read current playing info

    private static final String CLIENT_ID = "bf89756b59104628888b8d7a2b5b75df"; // Use your own Client Id
    private static final String REDIRECT_URI = "http://localhost:5000/callback"; // Also match this on the app settings
    private SpotifyAppRemote mSpotifyAppRemote;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        instance = this;
    }

    @Override
    protected void onStart() {
        super.onStart();
        ConnectionParams connectionParams = new ConnectionParams.Builder(CLIENT_ID)
                .setRedirectUri(REDIRECT_URI)
                .showAuthView(true)
                .build();

        SpotifyAppRemote.connect(this, connectionParams, new Connector.ConnectionListener() {
            public void onConnected(SpotifyAppRemote spotifyAppRemote) {
                mSpotifyAppRemote = spotifyAppRemote;
                Log.d("MainActivity", "Connected! Yay!");

                // Now you can start interacting with App Remote
                connected();

            }

            public void onFailure(Throwable throwable) {
                Log.e("MyActivity", throwable.getMessage(), throwable);

                // Something went wrong when attempting to connect! Handle errors here
            }
        });
    }

    @Override
    protected void onStop() {
        super.onStop();
        SpotifyAppRemote.disconnect(mSpotifyAppRemote);
        Log.d("MainActivity", "Disconnecting from onStop");
    }

    private void connected() {

        // Subscribe to PlayerState
        mSpotifyAppRemote.getPlayerApi()
                .subscribeToPlayerState()
                .setEventCallback(playerState -> {
                    final Track track = playerState.track;
                    if (track != null) {
                        Log.d("MainActivity", track.name + " by " + track.artist.name);
                        currentPlaying = track.name + " by " + track.artist.name;
                    }
                });
    }

    // Triggered by Unity C#
    public void ResetSpotify() {
        Log.d("MainActivity", "Turning off repeat");

        // ToDO add more if needed.
    }

    public void Menu() {
        // Plays menu song
        mSpotifyAppRemote.getPlayerApi().setRepeat(Repeat.ALL);
        mSpotifyAppRemote.getPlayerApi().play("spotify:track:0ByMNEPAPpOR5H69DVrTNy");
        Log.d("MainActivity", "Playing Menu Song");
    }

    // Triggered by Unity C#
    public void PlaySong(String trackId) {
        mSpotifyAppRemote.getPlayerApi().setRepeat(Repeat.OFF);
        mSpotifyAppRemote.getPlayerApi().play("spotify:track:" + trackId);
        Log.d("MainActivity", "Playing song: " + trackId);
    }

    // Triggered by Unity C#
    public void SkipToNextSong() {
        if (mSpotifyAppRemote != null) {
            mSpotifyAppRemote.getPlayerApi().skipNext();
            Log.d("MainActivity", "Skipped to next song");
        }
    }
}