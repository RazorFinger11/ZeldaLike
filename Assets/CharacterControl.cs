using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControl : MonoBehaviour
{
    [SerializeField]
    private Animator anim;
    [SerializeField]
    private float directionDampTime = .25f;
    [SerializeField]
    private ThirdPersonCamera gamecam;
    [SerializeField]
    private float directionSpeed = 3.0f;
    [SerializeField]
    private float rotationDegreePerSecond = 120f;  

    private int m_LocomotionId = 0;
    private float speed;
    private float direction;
    private float h;
    private float v;
    private AnimatorStateInfo stateInfo;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        if (anim.layerCount >= 2)
        {
            anim.SetLayerWeight(1, 1);  
        }

        m_LocomotionId = Animator.StringToHash("Base Layer.Locomotion");
        
    }

    // Update is called once per frame
    void Update()
    {
        if (anim)
        {
            stateInfo = anim.GetCurrentAnimatorStateInfo(0);
            // pegando os valores de WASD OU ANALÓGICO
            h = Input.GetAxis("Horizontal");
            v = Input.GetAxis("Vertical");
            speed = new Vector2(h, v).sqrMagnitude;

            anim.SetFloat("Speed", speed);
            
           
            // Traduz cordenadas do analógico para World/Camera/Character space
            StickToWorldSpace(this.transform, gamecam.transform, ref direction, ref speed);

        }
        
    }

    private void FixedUpdate()
    {
        if (IsInLocomotion() && ((direction >= 0 && h >= 0) || (direction < 0 && h < 0)))
        {
            Vector3 rotationAmount = Vector3.Lerp(Vector3.zero, new Vector3(0f, rotationDegreePerSecond * (h < 0f ? -1f : 1f), 0f), Mathf.Abs(h));
        }
    }

    public void StickToWorldSpace(Transform root, Transform camera, ref float directionOut, ref float speedOut)
    {
        Vector3 rootDirection = root.forward;

        Vector3 stickDirection = new Vector3(h, 0, v);

        speedOut = stickDirection.sqrMagnitude;

        //Pega rotação da Camera
        Vector3 CameraDirection = camera.forward;
        CameraDirection.y = 0.0f;
        Quaternion referentialShift = Quaternion.FromToRotation(Vector3.forward, CameraDirection);

        //Converte input em cordenadas do Worldspace (pensando em analógico)
        Vector3 moveDirection = referentialShift * stickDirection;
        Vector3 axisSign = Vector3.Cross(moveDirection, rootDirection);


        float angleRootToMove = Vector3.Angle(rootDirection, moveDirection) * (axisSign.y >= 0 ? -1f : 1f);

        angleRootToMove /= 180f;

        directionOut = angleRootToMove * directionSpeed;
    }
    public bool IsInLocomotion()
    {
        return stateInfo.nameHash == m_LocomotionId;
    }
}
