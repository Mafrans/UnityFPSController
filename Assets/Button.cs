using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    private bool pushDownAnim = false;
    private bool lastPushDownAnim = false;
    public float currentTime = 0;
    public float animationTime = 0.5f;
    public float waitTime = 0;
    public float pushDownAmount = 1;
    private Vector3 oldPos;
    public string callback;
    private bool canTrigger = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(pushDownAnim) {
            if(!lastPushDownAnim) {
                oldPos = transform.localPosition;
            }

            if(currentTime < animationTime*2) {
                if(canTrigger) {
                    gameObject.SendMessage(callback);
                    canTrigger = false;
                }
            }

            if(currentTime < animationTime + waitTime) {
                transform.localPosition = oldPos + new Vector3(0, -transform.localScale.y * Mathf.SmoothStep(0, pushDownAmount, Mathf.Min(1, currentTime/animationTime)), 0);
            }
            else if(currentTime < animationTime*2+waitTime) {
                transform.localPosition = oldPos + new Vector3(0, -transform.localScale.y * Mathf.SmoothStep(pushDownAmount, 0, Mathf.Min(1, (currentTime-animationTime-waitTime)/(animationTime))), 0);
            }
            else {
                pushDownAnim = false;
                canTrigger = true;
                currentTime = 0;
            }
            currentTime += Time.deltaTime;
        }
        lastPushDownAnim = pushDownAnim;
    }

    public void Press() {
        
    }

    public void PushDown() {
        pushDownAnim = true;
    }
}
