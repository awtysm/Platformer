using UnityEngine.UI;
using UnityEngine;

public class DebugInfoScript : MonoBehaviour
{
    public Transform player;
    int avgFrameRate;

    public Text FPSTxt;
    public Text XTxt;
    public Text YTxt;
    public Text ZTxt;
    public Text PlayerInputsTxt;

    public void Update ()
        {
            FPSCounter();
            PlayerDebugXYZ();
            PlayerInputsDebug();
        }

    public void FPSCounter()
        {
            float current = 0;
            current = (int)(1f / Time.unscaledDeltaTime);
            avgFrameRate = (int)current;
            FPSTxt.text = avgFrameRate.ToString() + " FPS";
        }

    public void PlayerDebugXYZ()
        {
            float PDCx,PDCy,PDCz = 0;
            PDCx = (float)player.position.x;
            PDCy = (float)player.position.y;
            PDCz = (float)player.position.z;
            XTxt.text = "X: " + PDCx.ToString();
            YTxt.text = "Y: " + PDCy.ToString();
            ZTxt.text = "Z: " + PDCz.ToString();
        }

    private void PlayerInputsDebug()
        {
            string AText = "";
            string BText = "";
            if(Input.GetKey(KeyCode.A)) AText = "A ";
            if(Input.GetKey(KeyCode.B)) BText = "B ";
            PlayerInputsTxt.text = AText + BText;
        }
}

