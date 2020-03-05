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
                        anim.SetBool("Hit1",true);
                    }
                }

                break;

            case CombatState.Hit1:
                attackTimer -= Time.deltaTime;

                if (attackTimer >= 0f) {
                    if (Input.GetKeyDown("e") || Input.GetKeyDown("joystick button 2")) {
                        attackTimer = DEFAULT_TIME;
                        curr = CombatState.Hit2;
                        anim.SetBool("Hit2",true);
                    }
                } else {
                    attackTimer = DEFAULT_TIME;
                    cooldownTimer = COOLDOWN_TIME;
                    curr = CombatState.Waiting;
                }
                
                break;

            case CombatState.Hit2:
                attackTimer -= Time.deltaTime;

                if (attackTimer >= 0f) {
                    if (Input.GetKeyDown("e") || Input.GetKeyDown("joystick button 2")) {
                        attackTimer = DEFAULT_TIME;
                        curr = CombatState.Hit3;
                        anim.SetBool("Hit3",true);
                    }
                } else {
                    attackTimer = DEFAULT_TIME;
                    cooldownTimer = COOLDOWN_TIME;
                    curr = CombatState.Waiting;
                }

                break;
                
            case CombatState.Hit3:
                attackTimer = DEFAULT_TIME;
                cooldownTimer = COOLDOWN_TIME;
                curr = CombatState.Waiting;

                break;
        }
    }

    private void ContinueCombo(CombatState newCombatState) {

    }

    private void Attack(int r) {
        if (r == 0) {
            Collider2D[] enemiesToDamage = Physics2D.OverlapBoxAll(new Vector2(R_Hitbox.position.x, R_Hitbox.position.y),new Vector2(3, 2), 0f);
            foreach(Collider2D enemy in enemiesToDamage)
            {
                enemy.GetComponent<Fish_AI>()?.TakeDamage();
                enemy.GetComponent<Enemy_AI_Duel_StateMachine>()?.TakeDamage();
            }
        } else {
            Collider2D[] enemiesToDamage = Physics2D.OverlapBoxAll(new Vector2(L_Hitbox.position.x, L_Hitbox.position.y),new Vector2(3, 2), 0f);
            foreach(Collider2D enemy in enemiesToDamage)
            {
                enemy.GetComponent<Fish_AI>()?.TakeDamage();
                enemy.GetComponent<Enemy_AI_Duel_StateMachine>()?.TakeDamage();
            }
        }
    }

    public void StopHitAnimation(int index) {
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

        private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(new Vector2(this.gameObject.transform.position.x + 5, this.gameObject.transform.position.y + 5), new Vector2(3f, 2f));
    }
}
