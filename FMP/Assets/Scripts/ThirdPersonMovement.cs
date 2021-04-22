using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ThirdPersonMovement : MonoBehaviour
{

    #region ComponnentVariables

    private Vector3 PreviousPos;
    public Transform Camera;
    public CharacterController charController;
    public TransitionController tranController;
    public Animator playerAnimations;
    public CinemachineFreeLook playerCamera;

    #endregion



    #region MovementVariables

    public float turnSmoothTime = 0.1f;
    public float speed = 6f;
    public float gravity = 2f;
    float turnSmoothVelocity;


    [HideInInspector]
    public bool canControl = false;

    #endregion


    // Update is called once per frame
    void FixedUpdate()
    {
        if (canControl)
        {

            playerCamera.enabled = true;
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");
            Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        
            if (direction.magnitude >= 0.1f) 
            {
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + Camera.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                if (!charController.isGrounded)
                {
                    moveDirection += Physics.gravity;
                }

                playerAnimations.SetBool("Walking", true);


                charController.Move(moveDirection.normalized * speed * Time.deltaTime);

            }
            else
            {
                playerAnimations.SetBool("Walking", false);

            }

        }
        else
        {
            playerCamera.enabled = false;
        }

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy" && canControl)
        {
            Destroy(other);
            playerAnimations.SetBool("Walking", false);
            canControl = false;
            PreviousPos = transform.position;
            tranController.StartBattle();
        }
    }

    public void ResumeTravel() 
    {
        transform.position = PreviousPos;
        canControl = true;
    }


}
