using UnityEngine;
using System.Collections;

public class FPSCounter : MonoBehaviour
{
    private Rect startRect; // The rect the window is initially displayed at.
    public bool updateColor = true; // Do you want the color to change if the FPS gets low
    public bool allowDrag = true; // Do you want to allow the dragging of the FPS window

    private Color color = Color.white; // The color of the GUI, depending of the FPS ( R < 10, Y < 30, G >= 30 )
    private string sFPS = ""; // The fps formatted into a string.
    private GUIStyle style; // The style the text will be displayed at, based en defaultSkin.label.

    float deltaTime = 0.0f;

    void Start()
    {
        startRect = new Rect(Screen.width * 0.45f, Screen.height * 0.0185f, Screen.width * 0.1f, Screen.height * 0.1f);
    }

    void Update()
    {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
    }
    
    void OnGUI()
    {
        float msec = deltaTime * 1000.0f;
        float fps = 1.0f / deltaTime;

        sFPS = string.Format("{0:F1} FPS ", fps) + string.Format("({0:F2} ms)", msec);

        //Update the color
        color = (fps >= 30) ? Color.green : ((fps > 10) ? Color.red : Color.yellow);

        // Copy the default label skin, change the color and the alignement
        if (style == null)
        {
            style = new GUIStyle(GUI.skin.label);
            style.normal.textColor = Color.white;
            style.alignment = TextAnchor.MiddleCenter;
        }

        GUI.color = updateColor ? color : Color.white;
        startRect = GUI.Window(0, startRect, DoMyWindow, "");
    }

    void DoMyWindow(int windowID)
    {
        GUI.Label(new Rect(0, 0, startRect.width, startRect.height), sFPS, style);
        if (allowDrag) GUI.DragWindow(new Rect(0, 0, Screen.width, Screen.height));
    }
}