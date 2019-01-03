using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GameObject))]
public class CameraMovement : MonoBehaviour {

    public AudioPeer _audioPeer;
    public GameObject _target;
    public float _maxDistance = 200;
    public float _minDistance = 1;
    public float _rotateSpeed = 20;
    public float _zoomSpeed = 0.002f;

    private bool moveOut = true; 
	// Use this for initialization
	void Start () {
        // _target = GetComponent<GameObject>();
	}
	
	// Update is called once per frame
	void Update () {

        float move = moveOut ? 1 + _zoomSpeed : 1 - _zoomSpeed;
        transform.position = Vector3.Lerp(transform.position, transform.position * move, 4);
        float distance = Vector3.Distance(transform.position, _target.transform.position);
        // print("Distance " + _audioPeer._AmplitudeBuffer);

        if (distance > _maxDistance) {
            moveOut = false;
        }

        if (distance < _minDistance) {
            moveOut = true;        
        }

        transform.RotateAround(_target.transform.position, Vector3.up, _rotateSpeed * Time.deltaTime * _audioPeer._AmplitudeBuffer);
        transform.LookAt(_target.transform);
	}
}
