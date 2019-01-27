using System.Security.Permissions;
using Core;
using UnityEngine;

public class TimerController : MonoBehaviour
{
    
    public bool    timeStarted = false;
    public float   gameLimit   = 240f;
    public GUISkin guiSkin;
    
    void Update ()
    {
        if ( timeStarted && gameLimit > -1f) {
            gameLimit -= Time.deltaTime;
            checkIfTimerUp();
        }
    }


    public void startTimer() { timeStarted = true; }

    public void endTimer() { timeStarted = false;}
    
    public void resetTimer() {
        gameLimit   = 240f;
        timeStarted = false;
    }


    public void checkIfTimerUp() {
        if (gameLimit < 0f) GameController.Instance.Loss();
    }
    
    private void OnGUI() {
        if ( !timeStarted ) return;
        GUI.skin = guiSkin;
        int    minutes  = Mathf.FloorToInt(gameLimit / 60F);
        int    seconds  = Mathf.FloorToInt(gameLimit - minutes * 60);
        string niceTime = string.Format("{0:0}:{1:00}", minutes, seconds);
        int    w        = 100;
        int    h        = 40;
        Rect   rect     = new Rect( (Screen.width-w)/2f, h, w, h);
        GUI.Label(rect, niceTime);
    }
    
}