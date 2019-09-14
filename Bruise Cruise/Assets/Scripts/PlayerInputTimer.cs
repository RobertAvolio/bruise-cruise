using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputTimer : MonoBehaviour
{
    private float timeToAttack;
    public float startTimeAttack;
    public float timerStart;
    private float timer;
    private bool hit2;
    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
    }

    private void Update()
    {
        if (timeToAttack <= 0)
        {
            if (Input.GetKeyUp("e"))
            {
                if (timer <= 0)
                {
                    Collider2D[] enemiesToDamage = Physics2D.OverlapBoxAll(new Vector2(this.gameObject.transform.position.x + 14, this.gameObject.transform.position.y), new Vector3(11, 6, 1), 0);
                    foreach(Collider2D enemy in enemiesToDamage)
                    {
                        enemy.GetComponent<Enemy>()?.TakeDamage();
                    }
                    timer = timerStart;
                }
                else if (!hit2)
                {
                    Collider2D[] enemiesToDamage = Physics2D.OverlapBoxAll(new Vector2(this.gameObject.transform.position.x + 12, this.gameObject.transform.position.y - 2), new Vector3(9, 4, 1), 0);
                    foreach (Collider2D enemy in enemiesToDamage)
                    {
                        enemy.GetComponent<Enemy>()?.TakeDamage();
                    }

                    hit2 = true;
                }
                else
                {
                    Collider2D[] enemiesToDamage = Physics2D.OverlapBoxAll(new Vector2(this.gameObject.transform.position.x + 10, this.gameObject.transform.position.y + 4), new Vector3(7, 5, 1), 0);
                    foreach (Collider2D enemy in enemiesToDamage)
                    {
                        enemy.GetComponent<Enemy>()?.TakeDamage();
                    }

                    timer = 0;
                }
                timeToAttack = startTimeAttack;
            }
            if (timer > 0)
            {
                timer -= Time.deltaTime;
            }
            else
            {
                hit2 = false;
            }
        }
        else
        {
            timeToAttack -= Time.deltaTime;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireCube(new Vector2(this.gameObject.transform.position.x + 14, this.gameObject.transform.position.y), new Vector3(11, 6, 1));
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(new Vector2(this.gameObject.transform.position.x + 12, this.gameObject.transform.position.y - 2), new Vector3(9, 4, 1));
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(new Vector2(this.gameObject.transform.position.x + 10, this.gameObject.transform.position.y+4), new Vector3(7, 5, 1));

    }
}
