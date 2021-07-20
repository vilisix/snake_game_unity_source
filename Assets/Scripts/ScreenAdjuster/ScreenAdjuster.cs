using UnityEngine;
using System.Collections;

public class ScreenAdjuster : MonoBehaviour
{
    public NodeScaler GameField;
    public Camera MainCamera;

    private void Awake()
    {
        if(Screen.height > Screen.width)
        {
            float scalingRate = (float)Screen.width / (float)Screen.height;
            GameField.ScaleTo(GameField.GetTargetScale() * scalingRate);
            MainCamera.orthographicSize /= scalingRate;
        }
    }
}
