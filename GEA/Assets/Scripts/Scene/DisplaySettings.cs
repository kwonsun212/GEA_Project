using UnityEngine;

public class DisplaySettings : MonoBehaviour
{
    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
}
