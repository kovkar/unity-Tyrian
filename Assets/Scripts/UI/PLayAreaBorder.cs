using UnityEngine;

// edited snippet from https://stackoverflow.com/questions/62224353/how-to-show-an-outlined-gui-box-in-unity
public class PlayAreaBorder : MonoBehaviour
{
    [SerializeField] private Texture2D BoxBorder; // Set this to your border texture in the Unity Editor

    void OnGUI()
    {
        var props = EnvironmentProps.Instance;

        var borderSize = 2; // Border size in pixels
        var style = new GUIStyle();
        //Initialize RectOffset object
        style.border = new RectOffset(borderSize, borderSize, borderSize, borderSize);
        style.normal.background = BoxBorder;
        GUI.Box(new Rect(Vector2.zero, new Vector2(10.0f, 10.0f)), GUIContent.none, style);
        //GUI.Box(new Rect(props.playAreaBounds.center, props.playAreaBounds.size), GUIContent.none, style);
    }
}
