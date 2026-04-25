using UnityEngine;
using NRKernal;

/// <summary>
/// Detects hand gestures from the XREAL One Pro cameras and
/// dispatches Unity events for other components to consume.
/// </summary>
public class HandGestureController : MonoBehaviour
{
    [Header("References")]
    public Transform leftHandVisual;
    public Transform rightHandVisual;

    private NRHand _leftHand;
    private NRHand _rightHand;

    private void Start()
    {
        _leftHand  = NRInput.Hands.GetHand(HandEnum.LeftHand);
        _rightHand = NRInput.Hands.GetHand(HandEnum.RightHand);
    }

    private void Update()
    {
        UpdateHandVisual(_leftHand,  leftHandVisual);
        UpdateHandVisual(_rightHand, rightHandVisual);

        // Example: detect pinch on either hand
        if (NRInput.Hands.IsGesture(HandEnum.LeftHand,  HandGesture.Pinch)) OnPinch(HandEnum.LeftHand);
        if (NRInput.Hands.IsGesture(HandEnum.RightHand, HandGesture.Pinch)) OnPinch(HandEnum.RightHand);
    }

    private void UpdateHandVisual(NRHand hand, Transform visual)
    {
        if (visual == null) return;
        visual.gameObject.SetActive(hand != null && hand.IsTracked);
        if (hand != null && hand.IsTracked)
            visual.position = hand.GetJointPose(HandJointID.Wrist).position;
    }

    private void OnPinch(HandEnum hand)
    {
        Debug.Log($"[HandGestureController] Pinch detected on {hand}");
        // TODO: dispatch game event or invoke UnityEvent here
    }
}
