using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class StartSimulation : MonoBehaviour
{
    public AudioSource _audioSource;
    public Image _screenToFade;
    public float waitTimeSeconds = 1.0f;
    private bool startFade = false;


    // Use this for initialization
    void Start()
    {
        _audioSource.PlayDelayed(waitTimeSeconds);
    }

    // Update is called once per frame
    void Update()
    {
        if (!startFade && Time.time >= waitTimeSeconds)
        {
            startFade = true;
            _screenToFade.CrossFadeAlpha(0f, waitTimeSeconds, false);
        }

    }
}
