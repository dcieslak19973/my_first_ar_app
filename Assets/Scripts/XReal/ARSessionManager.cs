using UnityEngine;
using NRKernal; // requires NRSDK to be imported

/// <summary>
/// Entry point for the AR session.
/// Attach this to a GameObject in MainScene.unity.
/// </summary>
public class ARSessionManager : MonoBehaviour
{
    [Header("NRSDK Session Config")]
    [Tooltip("Drag in a NRSessionConfig asset, or leave blank to use defaults.")]
    public NRSessionConfig sessionConfig;

    private void Awake()
    {
        // Optional: override session config at runtime
        if (sessionConfig != null)
        {
            NRSessionManager.Instance.SetConfiguration(sessionConfig);
        }
    }

    private void OnEnable()
    {
        NRSessionManager.Instance.SessionState.AddListener(OnSessionStateChanged);
    }

    private void OnDisable()
    {
        NRSessionManager.Instance.SessionState.RemoveListener(OnSessionStateChanged);
    }

    private void OnSessionStateChanged(SessionState state)
    {
        Debug.Log($"[ARSessionManager] Session state changed: {state}");

        switch (state)
        {
            case SessionState.Running:
                OnSessionRunning();
                break;
            case SessionState.Paused:
                Debug.Log("[ARSessionManager] Session paused.");
                break;
        }
    }

    private void OnSessionRunning()
    {
        Debug.Log("[ARSessionManager] AR session is running. Glasses connected.");
        // TODO: enable your AR content here
    }
}
