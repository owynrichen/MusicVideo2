using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParamCube : MonoBehaviour {

    public AudioPeer _audioPeer;
    public int _band;
    public float _startScale, _scaleMultiplier;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        transform.localScale = new Vector3(transform.localScale.x, (_audioPeer._audioBandBuffer[_band] * _scaleMultiplier) + _startScale, transform.localScale.z);
	}
}
