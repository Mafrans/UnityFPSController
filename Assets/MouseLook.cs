using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour {
    private MouseInteract mouseInteract;
    public Camera camera;
    public Reticle reticle;
    public bool invertY;
    public float sensitivity = 1;
    public float hoverRange = 3;
    // Start is called before the first frame update
    void Start()
    {
        mouseInteract = GetComponent<MouseInteract>();
    }

    // Update is called once per frame
    void Update()
    {
        float xMouse = Input.GetAxis("Mouse X");
        float yMouse = Input.GetAxis("Mouse Y");

        if (Mathf.Abs(xMouse) > 0.1 || Mathf.Abs(yMouse) > 0.1) {
            transform.Rotate(Vector3.up, xMouse * sensitivity, Space.Self);
            camera.transform.Rotate(Vector3.right, yMouse * sensitivity * (invertY ? 1 : -1), Space.Self);
        }
        BloomReticle();
    }

    private void BloomReticle() {
        RaycastHit hit;
        bool isBlooming = false;
        
        if (mouseInteract.pickedUpItem == null && Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, hoverRange)) {
            GameObject obj = hit.collider.gameObject;
            if (obj.GetComponent<Interactable>()) {
                Interactable interactable = obj.GetComponent<Interactable>();

                Debug.Log("bloom");
                if (interactable.doBloom) {
                    isBlooming = true;
                    if (!reticle.animating) {
                        reticle.AnimateUpdateReticle(
                            interactable.bloomTime, 
                            reticle.baseSegments + interactable.bloomSegments, 
                            reticle.baseWidth + interactable.bloomWidth, 
                            reticle.baseSize + interactable.bloomSize);
                    }
                }
            }
        }

        if (!isBlooming && !reticle.animating) {
            reticle.AnimateUpdateReticle(
                0.3f, 
                reticle.baseSegments, 
                reticle.baseWidth, 
                reticle.baseSize);
        }
    }
}
