using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public enum BotIdleState
{
    Idle_1,
    Idle_2,
    Idle_3,
    Idle_4,
    Idle_5,
    Idle_6

}

public class BotAI : MonoBehaviour
{
    public Transform Player;
    public float distanceUntillLook;


    public Animator botAnim;

    public BotIdleState botState;

    public bool isIdle;
    bool startedWaving;

    public float idleSwitchTime;
    float tempSwitchTime;


    static System.Random R = new System.Random();
    static T RandomEnumValue<T>()
    {
        var v = Enum.GetValues(typeof(T));
        return (T)v.GetValue(R.Next(v.Length));
    }

    private void Start()
    {
        botAnim = GetComponent<Animator>();

        isIdle = true;
        tempSwitchTime = idleSwitchTime;
        botState = RandomEnumValue<BotIdleState>();

        IdleSwitch();
    }

    private void Update()
    {

        if ((Player.transform.position - this.transform.position).sqrMagnitude < distanceUntillLook)
            isIdle = false;
        else
            isIdle = true;


        if (isIdle)  // Normal state of bot when player is not in range
        {
            startedWaving = false;
            tempSwitchTime -= Time.deltaTime;
            if (tempSwitchTime <= 0)
            {
                IdleSwitch();
                tempSwitchTime = idleSwitchTime;
            }
        }
        else   // when player is in range to talk
        {
            if (!startedWaving)
            {
                LookAtPlayer();

                startedWaving = true;
            }
        }
        
    }

    private void LookAtPlayer()
    {
        botAnim.SetTrigger("waveNew"); ;
        

        var lookPos = Player.position - transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 50f);

        
    }

    void IdleSwitch()
    {
        if (!isIdle)
            return;

        botState = RandomEnumValue<BotIdleState>();

        switch (botState)
        {
            case BotIdleState.Idle_1:

                botAnim.SetTrigger("Shrug");
                    break;

            case BotIdleState.Idle_2:
                botAnim.SetTrigger("Yes");
                break;

            case BotIdleState.Idle_3:
                botAnim.SetTrigger("Conversation"); 
                break;

            case BotIdleState.Idle_4:
                botAnim.SetTrigger("No");
                break;


            case BotIdleState.Idle_5:
                botAnim.SetTrigger("Yes");
                break;

            case BotIdleState.Idle_6:
                botAnim.SetTrigger("Conversation");
                break;

            default:
                break;
        }

    }
}
