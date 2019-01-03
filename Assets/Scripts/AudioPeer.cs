using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (AudioSource))]
public class AudioPeer : MonoBehaviour {
    AudioSource _audioSource;
    private float[] _samplesLeft = new float[512];
    private float[] _samplesRight = new float[512];

    private float[] _freqBand = new float[8];
    private float[] _bandBuffer = new float[8];
    private float[] _bufferDecrease = new float[8];
    private float[] _freqBandHighest = new float[8];

    private float[] _freqBand64 = new float[64];
    private float[] _bandBuffer64 = new float[64];
    private float[] _bufferDecrease64 = new float[64];
    private float[] _freqBandHighest64 = new float[64];

    [HideInInspector]
    public float[] _audioBand, _audioBandBuffer;
    [HideInInspector]
    public float[] _audioBand64, _audioBandBuffer64;

    [HideInInspector]
    public float _Amplitude, _AmplitudeBuffer;
    private float _amplitudeHighest;
    public float _audioProfile;

    public enum _channel { Stereo, Left, Right }
    public _channel channel = new _channel();

	// Use this for initialization
	void Start () {
        _audioBand = new float[8];
        _audioBandBuffer = new float[8];
        _audioBand64 = new float[64];
        _audioBandBuffer64 = new float[64];

        _audioSource = GetComponent<AudioSource>();
        AudioProfile(_audioProfile);
	}
	
	// Update is called once per frame
	void Update () {
        GetSpectrumAudioSource();
        MakeFrequencyBands();
        MakeFrequencyBands64();
        BandBuffer();
        BandBuffer64();
        CreateAudioBands();
        CreateAudioBands64();
        GetAmplitude();
	}

    void AudioProfile(float profile) {
        for (int i = 0; i < 8; i++) {
            _freqBandHighest[i] = profile;
        }

        for (int i = 0; i < 64; i++) {
            _freqBandHighest64[i] = profile;
        }
    }

    void GetSpectrumAudioSource() {
        _audioSource.GetSpectrumData(_samplesLeft, 0, FFTWindow.Blackman);
        _audioSource.GetSpectrumData(_samplesRight, 1, FFTWindow.Blackman);
    }

    void MakeFrequencyBands() {
        int count = 0;

        for (int i = 0; i < 8; i++) {
            float average = 0;
            int sampleCount = (int)Mathf.Pow(2, i + 1);

            if (i == 7) {
                sampleCount += 2;
            }

            for (int j = 0; j < sampleCount; j++) {
                switch(channel) {
                    case _channel.Stereo:
                        average += (_samplesLeft[count] + _samplesRight[count]) * (count + 1);
                        break;
                    case _channel.Left:
                        average += _samplesLeft[count] * (count + 1);
                        break;
                    case _channel.Right:
                        average += _samplesRight[count] * (count + 1);
                        break;
                }

                count++;
            }

            average /= count;
            _freqBand[i] = average * 10;
        }
    }

    void MakeFrequencyBands64()
    {
        int count = 0;
        int sampleCount = 1;
        int power = 0;

        for (int i = 0; i < 64; i++)
        {
            float average = 0;

            if (i == 16 || i == 32 || i == 40 || i == 48 || i == 56)
            {
                power++;
                sampleCount = (int)Mathf.Pow(2, power);
                if (power == 3) {
                    sampleCount -= 2;
                }
            }

            for (int j = 0; j < sampleCount; j++)
            {
                switch (channel)
                {
                    case _channel.Stereo:
                        average += (_samplesLeft[count] + _samplesRight[count]) * (count + 1);
                        break;
                    case _channel.Left:
                        average += _samplesLeft[count] * (count + 1);
                        break;
                    case _channel.Right:
                        average += _samplesRight[count] * (count + 1);
                        break;
                }

                count++;
            }

            average /= count;
            _freqBand64[i] = average * 10;
        }
    }

    void BandBuffer() {
        for (int i = 0; i < 8; i++)
        {
            if (_freqBand[i] > _bandBuffer[i])
            {
                _bandBuffer[i] = _freqBand[i];
                _bufferDecrease[i] = 0.005f;
            }

            if (_freqBand[i] < _bandBuffer[i])
            {
                _bandBuffer[i] -= _bufferDecrease[i];
                _bufferDecrease[i] *= 1.2f;
            }

            if (_bandBuffer[i] < 0) _bandBuffer[i] = 0;
        }
    }

    void BandBuffer64()
    {
        for (int i = 0; i < 64; i++)
        {
            if (_freqBand64[i] > _bandBuffer64[i])
            {
                _bandBuffer64[i] = _freqBand64[i];
                _bufferDecrease64[i] = 0.005f;
            }

            if (_freqBand64[i] < _bandBuffer64[i])
            {
                _bandBuffer64[i] -= _bufferDecrease64[i];
                _bufferDecrease64[i] *= 1.2f;
            }

            if (_bandBuffer64[i] < 0) _bandBuffer64[i] = 0;
        }
    }

    void CreateAudioBands() {
        for (int i = 0; i < 8; i++) {
            if (_freqBand[i] > _freqBandHighest[i]) {
                _freqBandHighest[i] = _freqBand[i];
            }

            _audioBand[i] = (_freqBand[i] / _freqBandHighest[i]);
            _audioBandBuffer[i] = (_bandBuffer[i] / _freqBandHighest[i]); 
        }
    }

    void CreateAudioBands64()
    {
        for (int i = 0; i < 64; i++)
        {
            if (_freqBand64[i] > _freqBandHighest64[i])
            {
                _freqBandHighest64[i] = _freqBand64[i];
            }

            _audioBand64[i] = (_freqBand64[i] / _freqBandHighest64[i]);
            _audioBandBuffer64[i] = (_bandBuffer64[i] / _freqBandHighest64[i]);
        }
    }

    void GetAmplitude() {
        float _currentAmplitude = 0;
        float _currentAmplitudeBuffer = 0;
        for (int i = 0; i < 8; i++) {
            _currentAmplitude += _audioBand[i];
            _currentAmplitudeBuffer += _audioBandBuffer[i];
        }

        if (_currentAmplitude > _amplitudeHighest) {
            _amplitudeHighest = _currentAmplitude;
        }
        _Amplitude = _currentAmplitude / _amplitudeHighest;
        _AmplitudeBuffer = _currentAmplitudeBuffer / _amplitudeHighest;
    }
}
