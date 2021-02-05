using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{

    #region ComponnentVariables

    public Transform Camera;
    public CharacterController charController;
    public TransitionController tranController;
    public BattleSystem battleSystemController;

    #endregion



    #region MovementVariables

    public float turnSmoothTime = 0.1f;
    public float speed = 6f;
    public float gravity = 2f;
    float turnSmoothVelocity;
    public bool canControl = false;

    #endregion


    // Update is called once per frame
    void Update()
    {
        if (canControl)
        {
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
            
                charController.Move(moveDirection.normalized * speed * Time.deltaTime);

            }

        }

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy" && canControl)
        {

            canControl = false;
            tranController.StartBattle();
            

        }
    }

    public void StartBattle() 
    {
        battleSystemController.SetupBattle();
    }

}
