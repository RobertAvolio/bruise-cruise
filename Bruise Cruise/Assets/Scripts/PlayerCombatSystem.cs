using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatSystem : MonoBehaviour
{
    private Animator anim;
    private KeyCode attack_key = KeyCode.E;
    private PlayerMovement pm;
    
    public Transform L_Hitbox, R_Hitbox;

    // New changes
    public Transform hitbox_location;
    public float hitbox_width;
    public float hitbox_height;
    
    [SerializeField] private int num_of_inputs = 0;
    [SerializeField] private float time_since_last_input = 0;
    [SerializeField] private float max_delay_between_inputs = 1.2f;
    
    // Start is called before the first frame update
    private void Start()
    {
        anim = GetComponent<Animator>();
        pm = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Time.time - time_since_last_input > max_delay_between_inputs)
        {
            num_of_inputs = 0;
        }

        if (Input.GetAxisRaw("Horizontal") == 0 && Input.GetKeyDown(attack_key))
        {
            time_since_last_input = Time.time;
            num_of_inputs++;

            if (num_of_inputs == 1)
            {
                anim.SetBool("Attack1", true);
            }

            num_of_inputs = Mathf.Clamp(num_of_inputs, 0, 3);

            pm.cannot_move = true;
        }
    }

    public void EndFirstHit()
    {
        if (num_of_inputs >= 2)
        {
            anim.SetBool("Attack2", true);
        }
        else
        {
            anim.SetBool("Attack1", false);
            num_of_inputs = 0;
        }
    }

    public void EndSecondHit()
    {
        if (num_of_inputs >= 3)
        {
            anim.SetBool("Attack3", true);
        }
        else
        {
            anim.SetBool("Attack2", false);
            num_of_inputs = 0;
        }
    }
    
    public void EndThirdHit()
    {
        anim.SetBool("Attack1", false);
        anim.SetBool("Attack2", false);
        anim.SetBool("Attack3", false);
        num_of_inputs = 0;
        pm.cannot_move = false;
    }

    private void Attack(int r) {
        Collider2D[] enemiesToDamage = Physics2D.OverlapBoxAll(hitbox_location.position, new Vector2(hitbox_width, hitbox_height), 0);
        foreach(Collider2D enemy in enemiesToDamage)
        {
            enemy.GetComponent<Fish_AI>()?.TakeDamage();
            enemy.GetComponent<Enemy_AI_Duel_StateMachine>()?.TakeDamage();
        }
          
        /*
        if (r == 0) {
            // Collider2D[] enemiesToDamage = Physics2D.OverlapBoxAll(new Vector2(R_Hitbox.position.x, R_Hitbox.position.y),new Vector2(3, 2), 0f);
        } else {
            Collider2D[] enemiesToDamage = Physics2D.OverlapBoxAll(new Vector2(L_Hitbox.position.x, L_Hitbox.position.y),new Vector2(3, 2), 0f);
            foreach(Collider2D enemy in enemiesToDamage)
            {
                enemy.GetComponent<Fish_AI>()?.TakeDamage();
                enemy.GetComponent<Enemy_AI_Duel_StateMachine>()?.TakeDamage();
            }
        }
        */
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        // Gizmos.DrawWireCube(new Vector3(this.gameObject.transform.position.x + 5, this.gameObject.transform.position.y + 5, this.gameObject.transform.position.z), new Vector2(3f, 2f));
        Gizmos.DrawWireCube(hitbox_location.position, new Vector3(hitbox_width, hitbox_height, 1));
    }
}
