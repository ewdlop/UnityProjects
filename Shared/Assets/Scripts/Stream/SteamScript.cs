using Steamworks;
using UnityEngine;

/// <summary>
/// <seealso cref="https://steamworks.github.io/gettingstarted/"/>
/// </summary>
public class SteamScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected Callback<GameOverlayActivated_t> m_GameOverlayActivated;
    protected CallResult<NumberOfCurrentPlayers_t> m_NumberOfCurrentPlayers;

    protected virtual void OnEnable()
    {
        if (SteamManager.Initialized)
        {
            m_NumberOfCurrentPlayers = CallResult<NumberOfCurrentPlayers_t>.Create(OnNumberOfCurrentPlayers);
            m_GameOverlayActivated = Callback<GameOverlayActivated_t>.Create(OnGameOverlayActivated);
        }
    }

    protected virtual void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SteamAPICall_t handle = SteamUserStats.GetNumberOfCurrentPlayers();
            m_NumberOfCurrentPlayers.Set(handle);
            Debug.Log("Called GetNumberOfCurrentPlayers()");
        }
    }

    protected virtual void OnGameOverlayActivated(GameOverlayActivated_t pCallback)
    {
        if (pCallback.m_bActive != 0)
        {
            Debug.Log("Steam Overlay has been activated");
        }
        else
        {
            Debug.Log("Steam Overlay has been closed");
        }
    }

    protected virtual void OnNumberOfCurrentPlayers(NumberOfCurrentPlayers_t pCallback, bool bIOFailure)
    {
        if (pCallback.m_bSuccess != 1 || bIOFailure)
        {
            Debug.Log("There was an error retrieving the NumberOfCurrentPlayers.");
        }
        else
        {
            Debug.Log($"The number of players playing your game: {pCallback.m_cPlayers}");
        }
    }
}
