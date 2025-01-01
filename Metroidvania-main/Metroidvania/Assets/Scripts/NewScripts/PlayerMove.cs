using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public CharacterMovement controller;
    public Animator animator;

    public float runSpeed = 40.0f;                          //이동 속도 값 설정                 
    float horizontalMove = 0f;
    bool jump = false;
    //bool dash = false;
    bool shield = false;
    bool shieldCoolTime = true;

    public float shieldDuration = 2.0f; // 쉴드 지속 시간
    public float shieldCooldown = 2.0f; // 쉴드 쿨타임

    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        if (Input.GetKeyDown(KeyCode.Z))
        {
            jump = true;
        }

        //if (Input.GetKeyDown(KeyCode.LeftShift))
        //{
        //    dash = true;
        //}

        if (Input.GetKeyDown(KeyCode.D ) && shieldCoolTime)
        {
            StartCoroutine(ActivateShield());
        }
    }
    IEnumerator ActivateShield()
    {
        shield = true;
        shieldCoolTime = false;

        // 애니메이션에서 쉴드 활성화
        animator.SetBool("IsShielding", true);
        Debug.Log("Shield Activated");

        // 쉴드 지속 시간 동안 유지
        yield return new WaitForSeconds(shieldDuration);

        shield = false;

        // 애니메이션에서 쉴드 비활성화
        animator.SetBool("IsShielding", false);
        Debug.Log("Shield Deactivated");

        // 쿨타임 대기
        yield return new WaitForSeconds(shieldCooldown);

        shieldCoolTime = true;
        Debug.Log("Shield Ready");
    }
    public void OnFall()                    //떨어짐 처리 Event
    {
        animator.SetBool("IsJumping", true);
    }

    public void OnLanding()                 //바닥에 착지처리 Event
    {
        animator.SetBool("IsJumping", false);
    }

    void FixedUpdate()
    {
        //캐릭터 움직임 구현할 함수 
        controller.Move(horizontalMove * Time.fixedDeltaTime, jump, dash);
        jump = false;
        //dash = false;
    }
}
