using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private int Jumptick = 1000;

    private Rigidbody phys;

    private GameObject playerCam;

    private Vector3 lastMousePos = new Vector3(255, 255, 255);
    // Start is called before the first frame update
    void Start()
    {
        this.phys = this.GetComponent<Rigidbody>();
        this.playerCam = this.GetComponentInChildren<Camera>().gameObject;
    }

   
    // Update is called once per frame
    void Update()
    {
        #region Player Movement Code
            Vector3 deltaPos = new Vector3(0.0f, 0.0f,0.0f);
            if (Input.GetKey(KeyCode.W)) deltaPos += this.transform.forward.normalized;
            if (Input.GetKey(KeyCode.A)) deltaPos -= this.transform.right.normalized;
            if (Input.GetKey(KeyCode.S)) deltaPos -= this.transform.forward.normalized;
            if (Input.GetKey(KeyCode.D)) deltaPos += this.transform.right.normalized;
            this.transform.position += 0.01f * deltaPos.normalized;

            if (Jumptick < 1)
            {

                if (Input.GetKey(KeyCode.Space))
                {
                    this.Jumptick = 1000;
                    this.phys.AddForce(0.0f, 200.0f, 0.0f);
                }
            } 
            else this.Jumptick--;
        #endregion Player Movement Code
        
        #region Camera Movement Code
            
            Vector3 mpos = Input.mousePosition;
            this.lastMousePos = mpos - this.lastMousePos;
            this.lastMousePos = new Vector3(-this.lastMousePos.y * Settings.sensitivity,
                this.lastMousePos.x * Settings.sensitivity, 0);
            lastMousePos = new Vector3(transform.eulerAngles.x + lastMousePos.x,
                playerCam.transform.eulerAngles.y + lastMousePos.y, 0);
            //transform.rotation = Quaternion.Euler(0, lastMousePos.x,0);
            playerCam.transform.rotation = Quaternion.Euler(lastMousePos.x,lastMousePos.y,0);
            lastMousePos = mpos;




            #endregion Camera Movement Code
    }
}
