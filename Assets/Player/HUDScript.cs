using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class HUDScript : MonoBehaviour
{
    [SerializeField] public TMP_Text timer;
    float frames = 0;
    int seconds = 0;
    int minutes = 0;
    void Update()
    {
        frames+= Time.deltaTime;
        //Debug.Log(frames);
        if (frames > 1)
        {
            frames = 0;
            seconds++;
            if (seconds == 60)
            {
                seconds = 0;
                minutes++;
            }
            timer.text = string.Format("Time Elapsed: {0:00}:{1:00}", minutes, seconds);
        }
    }
}
