using UnityEngine;

public class TouchController : MonoBehaviour
{
    public FixedTouchField FixedTouchField;
    public CameraLook CameraLook;

    private void Update()
    {
        CameraLook.LockAxis = FixedTouchField.TouchDist;
    }
}