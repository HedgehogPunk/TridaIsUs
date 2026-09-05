using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMove : MonoBehaviour
{
    public float sensitivity;
    public float slowSpeed;
    public float normalSpeed;
    public float sprintSpeed;
    float currentSpeed;

    public float maxDistance;

    
    InputMaster inputs;


    public bool pause = false;

    private void Awake()
    {
        inputs = new InputMaster();
        inputs.Enable();
       

        isWin.WinEvent.AddListener(Pause);

        UILogic.PauseEvent.AddListener(Pause);
        UILogic.UnpauseEvent.AddListener(UnPause);
        

        currentSpeed = normalSpeed;

        if(pause)
        {
            Pause();
        }
    }


    public void UnPause()
    {
        pause = false;
    }

    public void Pause()
    {
        pause = true;

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }

    void Update()
    {
        
        if(pause)
        {
            return;
        }

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        Vector2 move = inputs.Movement.CamMove.ReadValue<Vector2>();


        Movement(move);

        Rotation();



    }

    public void Rotation()
    {
        Vector3 mouseInput = new Vector3(-inputs.Movement.CamRotateY.ReadValue<float>(), inputs.Movement.CamRotateX.ReadValue<float>(), 0);
       transform.Rotate(mouseInput * sensitivity);
        Vector3 eulerRotation = transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(eulerRotation.x, eulerRotation.y, 0);
    }

    public void Movement( Vector2 vector)
    {
       
        Vector3 input = new Vector3(vector.x, 0f, vector.y);


        /*
        if (Input.GetKey(KeyCode.LeftShift))
        {
            currentSpeed = sprintSpeed;
        }
        else if (Input.GetKey(KeyCode.LeftAlt))
        {
            currentSpeed = slowSpeed;
        }
        else
        {
            currentSpeed = normalSpeed;
        }
        */
       
        transform.Translate(input * currentSpeed * Time.deltaTime);


        transform.position = new Vector3(Mathf.Clamp(transform.position.x, 0.5f, maxDistance), Mathf.Clamp(transform.position.y, 0.5f, maxDistance), Mathf.Clamp(transform.position.z, 0.5f, maxDistance));
    }
}
