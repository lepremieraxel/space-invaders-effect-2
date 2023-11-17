using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.TextCore.Text;
using UnityEngine.XR.Interaction.Toolkit;

public class SpaceShipManager : MonoBehaviour
{
    public GameObject SpaceShip;
    public float speed;

    private bool canGoLeft;
    private bool canGoRight;

    public GameObject limitX_Left;
    public GameObject limitX_Right;
    
    private float max_X_Left;
    private float max_X_Right;

    public bool canControlShip;


    //INPUTS
    public ActionBasedController controller = null; // glisser le controler ici
    //public float JoystickValue;
    private bool isTriggered;


    // Start is called before the first frame update
    void Start()
    {
        canControlShip = false;
        canGoLeft = true;
        canGoRight = true;

        max_X_Left = limitX_Left.GetComponent<Transform>().position.x;
        max_X_Right = limitX_Right.GetComponent<Transform>().position.x;

    }

    // Update is called once per frame
    void Update()
    {


    }

    private void FixedUpdate()
    {
        float triggerValue = controller.translateAnchorAction.action.ReadValue<Vector2>().x;

        // SPACESHIP MOVEMENTS WITH VR LEFT JOYSTICK
        if (canControlShip == true)
        {
            Debug.Log(triggerValue);
            //VR Input to move spaceshift to the left
            if (canGoLeft == true && (triggerValue < -0.5) && !isTriggered)
            {
                SpaceShip.transform.Translate(-speed * Time.deltaTime, 0, 0);
                isTriggered = true;
            }
            else if ((triggerValue > 0) && isTriggered)
            {
                isTriggered = false;
            }

            //VR Input to move spaceshift to the right
            if (canGoRight == true && (triggerValue > 0.5) && !isTriggered)
            {
                SpaceShip.transform.Translate(speed * Time.deltaTime, 0, 0);
                isTriggered = true;
            }
            else if ((triggerValue < 0) && isTriggered)
            {
                isTriggered = false;
            }
        }
        
        // SPACESHIP MOVEMENTS WITH KEYBOARD
        if (canControlShip == true)
        {
            // SpaceShip movements to go left and right on the X axis
            if (canGoLeft == true && Input.GetKey(KeyCode.LeftArrow))
            {
                SpaceShip.transform.Translate(-speed * Time.deltaTime, 0, 0);
  
            }
            if (canGoRight == true && Input.GetKey(KeyCode.RightArrow)) 
            {
                SpaceShip.transform.Translate(speed * Time.deltaTime, 0, 0);
            }

            // SpaceShip movement limits on the X axis
            if (SpaceShip.transform.position.x <= max_X_Left)
            {
                canGoLeft = false;
                Debug.Log("Left wall touched");
            }
            else
            {
                canGoLeft = true;
            }

            // SpaceShip movement limits on the X axis
            if (SpaceShip.transform.position.x >= max_X_Right)
            {
                canGoRight = false;
                Debug.Log("Right wall touched");
            }
            else
            {
                canGoRight = true;
            }
        }
    }
}
