using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class MouseInteract : MonoBehaviour {
    public Camera camera;
    public float range = 3;
    public float moveSpeed;
    public ItemPickup pickedUpItem;
    public float minThrowSpeed = 10;
    public float maxThrowSpeed = 100;
    public float chargeThrow;
    public float chargeSpeed = 1;
    public bool canThrow;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0)) {
            if (pickedUpItem != null && canThrow) {
                Debug.Log("Throw Charge");
                if (chargeThrow < 1) {
                    chargeThrow += chargeSpeed * Time.deltaTime;
                }
            }
            Debug.DrawLine(camera.transform.position, camera.transform.position + camera.transform.forward * range);
        }
        
        if (Input.GetMouseButtonDown(0)) {
            if (pickedUpItem != null) {
                Debug.Log("Throw");
                canThrow = true;
            }
            
            RaycastHit hit;
            if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, range) && pickedUpItem == null) {
                GameObject obj = hit.collider.gameObject;
                if (obj.GetComponent<ItemPickup>()) {
                    pickedUpItem = obj.GetComponent<ItemPickup>();
                    canThrow = false;
                    chargeThrow = 0;
                }
                else if(obj.GetComponent<Button>()) {
                    Button btn = obj.GetComponent<Button>();
                    btn.PushDown();
                }
            }
        }

        if (Input.GetMouseButtonUp(0)) {
            if (pickedUpItem != null && canThrow && chargeThrow > 0) {
                if (pickedUpItem.GetComponent<Rigidbody>()) {
                    pickedUpItem.GetComponent<Rigidbody>().AddForce(camera.transform.forward * (minThrowSpeed + chargeThrow * (maxThrowSpeed - minThrowSpeed)), ForceMode.Impulse);
                }
                pickedUpItem = null;
            }
        }

        if (Input.GetMouseButtonDown(1)) {
            pickedUpItem = null;
        }

        if (pickedUpItem != null) {
            Vector3 targetPos = camera.transform.position + camera.transform.forward * 1.5f;
            if (pickedUpItem.GetComponent<Rigidbody>()) {
                Rigidbody rigidbody = pickedUpItem.GetComponent<Rigidbody>();
                rigidbody.velocity = moveSpeed * Time.deltaTime * Vector3.Distance(pickedUpItem.transform.position, targetPos) * (targetPos - pickedUpItem.transform.position).normalized;
                rigidbody.transform.up = (targetPos - pickedUpItem.transform.position).normalized;
                Debug.DrawRay(pickedUpItem.transform.position, (targetPos - pickedUpItem.transform.position).normalized);
            }
            else {
                pickedUpItem.transform.Translate(moveSpeed * Time.deltaTime * (targetPos - pickedUpItem.transform.position).normalized);
            }
        }
    }

    private void OnDrawGizmos() {
        if (pickedUpItem != null) {
            Gizmos.DrawSphere(camera.transform.position + camera.transform.forward * 1.5f, 0.1f);
        }
    }
}
