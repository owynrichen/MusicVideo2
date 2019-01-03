using UnityEngine;
using System.Collections;

public class LightFlash : MonoBehaviour
{
    public AudioPeer _audioPeer;
    public float _baseIntensity = 1;
    public int _band;
    public bool _amplitudeBased = false;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Light l = GetComponent<Light>();
        if (_amplitudeBased || (_band < 0 || _band > 7)) {
            l.intensity = _baseIntensity * _audioPeer._AmplitudeBuffer;
        } else {
            l.intensity = _baseIntensity * _audioPeer._audioBandBuffer[_band];
        }

    }
}
