using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reticle : MonoBehaviour {
    private LineRenderer renderer;
    public int baseSegments = 20;
    public float baseWidth = 1;
    public float baseSize = 1;
    
    private int segments = 20;
    private float width = 1;
    private float size = 1;

    private float animateTime;
    private float currentTime;
    public bool animating;
    
    private float lastWidth;
    private float lastSize;
    private float lastSegments;
    
    private float nextWidth;
    private float nextSize;
    private float nextSegments;
    // Start is called before the first frame update
    void Start() {
        renderer = GetComponent<LineRenderer>();

        segments = baseSegments;
        width = baseWidth;
        size = baseSize;
        UpdateReticle();
    }

    // Update is called once per frame
    void Update() {
        if (animating && currentTime < animateTime) {
            segments = Mathf.RoundToInt(Mathf.SmoothStep(lastSegments, nextSegments, currentTime/animateTime));
            width = Mathf.SmoothStep(lastWidth, nextWidth, currentTime/animateTime);
            size = Mathf.SmoothStep(lastSize, nextSize, currentTime / animateTime);
            currentTime += Time.deltaTime;
            
            UpdateReticle();
        }
        else if (currentTime >= animateTime) {
            animating = false;
            currentTime = 0;
            animateTime = 0;
        }
    }

    private Vector2 lengthDir(float length, float dir) {
        float x = Mathf.Cos(dir) * length;
        float y = Mathf.Sin(dir) * length;
        return new Vector2(y, x);
    }

    public void UpdateReticle() {
        renderer.startWidth = width;
        renderer.endWidth = width;
        renderer.startColor = Color.white;
        renderer.endColor = Color.white;
        renderer.positionCount = segments;

        Vector3[] positions = new Vector3[segments];
        for (int i = 0; i < segments; i++) {
            Vector2 pos = lengthDir(size, 2*Mathf.PI / segments * i);
            positions[i] = pos;
        }
        
        renderer.SetPositions(positions);
    }

    public void AnimateUpdateReticle(float time, int _segments, float _width, float _size) {
        lastSegments = segments;
        lastWidth = width;
        lastSize = size;
        
        animateTime = time;
        currentTime = 0;
        animating = true;

        nextSegments = _segments;
        nextWidth = _width;
        nextSize = _size;
    }
}
