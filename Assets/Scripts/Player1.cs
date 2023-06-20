using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player1 : PlayableCharacter
{
    [SerializeField] private Animator m_batAnimation;
    [SerializeField] private PlayableCharacterData m_checkBatUser;
    // For skills animation
    private bool skillPressed;
    private bool canUseSkill;
    private bool isAtDistance;
    void Start()
    {
       
        canUseSkill = true;

    }
    // Skill usage and cooldowns
    // Skills and skills animation cooldowns
    private void UseSkill()
    {
        if (Input.GetKeyDown(KeyCode.E) && canUseSkill)
        {
            skillPressed = true;
        }
        else if (Input.GetKeyDown(KeyCode.E) && !canUseSkill)
        {
            Debug.Log("Cannot use that now!");
        }
        else
        {
            skillPressed = false;
        }
    }
    private IEnumerator WaitToMove()
    {
        canMove = false;
        yield return new WaitForSeconds(1);
        canMove = true;
    }
    private IEnumerator AbilityCooldown()
    {
        canUseSkill = false;
        yield return new WaitForSeconds(3);
        canUseSkill = true;
    }
    //Specific skills
    //P1 Skills
    public void DistanceChecker(bool m_canUseBat)
    {
        isAtDistance = m_canUseBat;
    }
    public void UseBat()
    {
        if (m_checkBatUser.isBatUser && isAtDistance && skillPressed)
        {
            Debug.Log("Bonk");
            m_batAnimation.SetBool("isUsingSkill", true);
            StartCoroutine(AbilityCooldown());
            StartCoroutine(WaitToMove());
        }
        else if (m_checkBatUser.isBatUser && !isAtDistance && skillPressed)
        {
            Debug.Log("I need to be behind a creature!");

        }
        else
        {
            m_batAnimation.SetBool("isUsingSkill", false);
        }
    }
    protected override void OnUpdating()
    {
        UseBat();
        UseSkill();
    }
}
