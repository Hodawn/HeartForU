using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public CharacterMovement controller;
    public Animator animator;

    public float runSpeed = 40.0f;                          //�̵� �ӵ� �� ����                 
    float horizontalMove = 0f;
    bool jump = false;
    //bool dash = false;
    bool shield = false;
    bool shieldCoolTime = true;

    public float shieldDuration = 2.0f; // ���� ���� �ð�
    public float shieldCooldown = 2.0f; // ���� ��Ÿ��

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

        // �ִϸ��̼ǿ��� ���� Ȱ��ȭ
        animator.SetBool("IsShielding", true);
        Debug.Log("Shield Activated");

        // ���� ���� �ð� ���� ����
        yield return new WaitForSeconds(shieldDuration);

        shield = false;

        // �ִϸ��̼ǿ��� ���� ��Ȱ��ȭ
        animator.SetBool("IsShielding", false);
        Debug.Log("Shield Deactivated");

        // ��Ÿ�� ���
        yield return new WaitForSeconds(shieldCooldown);

        shieldCoolTime = true;
        Debug.Log("Shield Ready");
    }
    public void OnFall()                    //������ ó�� Event
    {
        animator.SetBool("IsJumping", true);
    }

    public void OnLanding()                 //�ٴڿ� ����ó�� Event
    {
        animator.SetBool("IsJumping", false);
    }

    void FixedUpdate()
    {
        //ĳ���� ������ ������ �Լ� 
        controller.Move(horizontalMove * Time.fixedDeltaTime, jump, dash);
        jump = false;
        //dash = false;
    }
}
