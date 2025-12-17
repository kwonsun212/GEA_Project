using UnityEngine;

public class MouseSensitivitySettings : MonoBehaviour
{
    public PlayerContorlller player;

    public void SetSensitivity(float value)
    {
        player.mouseSensitivity = value;
    }
}
