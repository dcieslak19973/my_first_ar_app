using UnityEngine;
using TMPro;

/// <summary>
/// Hello World for XREAL One Pro + Beam Pro.
/// Spawns a floating text panel in world space in front of the user.
///
/// Setup:
///   1. Add this script to any GameObject in your scene (e.g. "HelloWorldManager").
///   2. Assign the NRCameraRig's CenterCamera transform to the 'headCamera' field,
///      or leave it null to auto-find it at runtime.
/// </summary>
public class HelloWorldAR : MonoBehaviour
{
    [Header("Text Settings")]
    public string message = "Hello, AR World!";
    public float spawnDistance = 1.5f;   // metres in front of the user
    public float fontSize = 0.1f;
    public Color textColor = Color.white;

    [Header("Panel Settings")]
    public Vector2 panelSize = new Vector2(0.8f, 0.3f);
    public Color panelColor = new Color(0f, 0f, 0f, 0.6f);

    [Header("References (optional — auto-found if blank)")]
    public Camera headCamera;

    private GameObject _panel;

    private void Start()
    {
        // Auto-find the NRSDK center camera if not assigned
        if (headCamera == null)
        {
            var nrRig = FindObjectOfType<NRKernal.NRHMDPoseTracker>();
            if (nrRig != null)
                headCamera = nrRig.centerCamera;
        }

        // Fallback to main camera (useful in editor Play mode)
        if (headCamera == null)
            headCamera = Camera.main;

        SpawnPanel();
    }

    private void SpawnPanel()
    {
        // --- Background quad ---
        _panel = GameObject.CreatePrimitive(PrimitiveType.Quad);
        _panel.name = "HelloWorldPanel";
        Destroy(_panel.GetComponent<MeshCollider>());

        var mat = new Material(Shader.Find("Unlit/Color"));
        mat.color = panelColor;
        _panel.GetComponent<MeshRenderer>().material = mat;
        _panel.transform.localScale = new Vector3(panelSize.x, panelSize.y, 1f);

        // Position it in front of the camera
        PositionPanel();

        // --- World-space Canvas + TextMeshPro ---
        var canvasGO = new GameObject("HelloWorldCanvas");
        canvasGO.transform.SetParent(_panel.transform, false);

        var canvas = canvasGO.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.WorldSpace;

        var rt = canvasGO.GetComponent<RectTransform>();
        rt.sizeDelta = panelSize * 100f;          // Canvas units (1 unit = 0.01 m here)
        rt.localPosition = new Vector3(0f, 0f, -0.001f); // just in front of quad
        rt.localScale    = Vector3.one * 0.01f;

        // TextMeshPro text
        var textGO = new GameObject("HelloText");
        textGO.transform.SetParent(canvasGO.transform, false);

        var tmp = textGO.AddComponent<TextMeshProUGUI>();
        tmp.text          = message;
        tmp.fontSize      = fontSize * 100f;      // scaled to canvas units
        tmp.color         = textColor;
        tmp.alignment     = TextAlignmentOptions.Center;
        tmp.enableWordWrapping = true;

        var textRT = textGO.GetComponent<RectTransform>();
        textRT.anchorMin   = Vector2.zero;
        textRT.anchorMax   = Vector2.one;
        textRT.offsetMin   = Vector2.zero;
        textRT.offsetMax   = Vector2.zero;

        Debug.Log($"[HelloWorldAR] Panel spawned at {_panel.transform.position}");
    }

    private void PositionPanel()
    {
        if (headCamera == null) return;

        Transform cam = headCamera.transform;
        // Spawn directly ahead, ignore vertical tilt so the panel sits level
        Vector3 forward = Vector3.ProjectOnPlane(cam.forward, Vector3.up).normalized;
        if (forward == Vector3.zero) forward = cam.forward;

        _panel.transform.position = cam.position + forward * spawnDistance;
        _panel.transform.rotation = Quaternion.LookRotation(forward);
    }

    // Optional: tap/pinch to relocate the panel
    public void Relocate()
    {
        if (_panel != null)
            PositionPanel();
    }
}
