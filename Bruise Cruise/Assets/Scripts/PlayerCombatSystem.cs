using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CombatState { Waiting, Hit1, Hit2, Hit3 }

public class PlayerCombatSystem : MonoBehaviour
{
    private float comboWindow, attackTimer, cooldownTimer;
    private bool h1, h2, h3;
    private Animator anim;
    private CombatState curr;
    private const float DEFAULT_TIME = 0.75f;
    private const float COOLDOWN_TIME = 0.5f;
    
    public Transform L_Hitbox, R_Hitbox;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        attackTimer = DEFAULT_TIME;
        cooldownTimer = 0f;
        curr = CombatState.Waiting;
    }

    // Update is called once per frame
    void Update()
    {
        // CombatState machine! I finally made one and it's so cool! - Love, Ben
        switch(curr) {
            case CombatState.Waiting:
                cooldownTimer -= Time.deltaTime;

                if (cooldownTimer <= 0) {
                    if (Input.GetKeyDown("e") || Input.GetKeyDown("joystick button 2")) {
                        curr = CombatState.Hit1;
                    }
                }

                break;

            case CombatState.Hit1:
                anim.SetBool("Hit1",true);
                attackTimer -= Time.deltaTime;

                if (attackTimer >= 0f) {
                    if (Input.GetKeyDown("e") || Input.GetKeyDown("joystick button 2")) {
                        attackTimer = DEFAULT_TIME;
                        curr = CombatState.Hit2;
                    }
                } else {
                    print("hit2?");
                    attackTimer = DEFAULT_TIME;
                    cooldownTimer = COOLDOWN_TIME;
                    curr = CombatState.Waiting;
                }
                
                break;

            case CombatState.Hit2:
                anim.SetBool("Hit2",true);
                attackTimer -= Time.deltaTime;

                if (attackTimer >= 0f) {
                    if (Input.GetKeyDown("e") || Input.GetKeyDown("joystick button 2")) {
                        attackTimer = DEFAULT_TIME;
                        curr = CombatState.Hit3;
                    }
                } else {
                    attackTimer = DEFAULT_TIME;
                    cooldownTimer = COOLDOWN_TIME;
                    curr = CombatState.Waiting;
                }

                break;
                
            case CombatState.Hit3:
                anim.SetBool("Hit3",true);
                attackTimer = DEFAULT_TIME;
                cooldownTimer = COOLDOWN_TIME;
                curr = CombatState.Waiting;

                break;
        }
    }

    private void ContinueCombo(CombatState newCombatState) {

    }

    private void Attack() {

    }

    public void StopHitAnimation(int index) {
        print("Called");
        if (index == 0) {
            anim.SetBool("Hit1",false);
        }
        else if (index == 1) {
            anim.SetBool("Hit2",false);
        }
        else if (index == 2) {
            anim.SetBool("Hit3",false);
        }
    }
}
