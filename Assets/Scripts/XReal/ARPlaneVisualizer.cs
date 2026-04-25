using UnityEngine;
using NRKernal.NRWorld;

/// <summary>
/// Spawns visual indicators on NRSDK-detected planes (floors, tables, walls).
/// </summary>
public class ARPlaneVisualizer : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject planePrefab;

    private NRAnchorManager _anchorManager;

    private void OnEnable()
    {
        NRWorldAnchorStore.OnTrackablePlanesChanged += HandlePlanesChanged;
    }

    private void OnDisable()
    {
        NRWorldAnchorStore.OnTrackablePlanesChanged -= HandlePlanesChanged;
    }

    private void HandlePlanesChanged(ARTrackablesChangedEventArgs<NRTrackablePlane> args)
    {
        // Spawn visuals for newly added planes
        foreach (var plane in args.added)
        {
            if (planePrefab == null) continue;
            var go = Instantiate(planePrefab, plane.GetCenterPose().position, plane.GetCenterPose().rotation);
            go.name = $"Plane_{plane.TrackableId}";
        }

        // Remove visuals for removed planes (simple approach: find by name)
        foreach (var plane in args.removed)
        {
            var go = GameObject.Find($"Plane_{plane.TrackableId}");
            if (go != null) Destroy(go);
        }
    }
}
