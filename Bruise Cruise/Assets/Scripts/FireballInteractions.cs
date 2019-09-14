using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballInteractions : MonoBehaviour
{
    public float xVel;
    public ParticleSystem pSystem;
    private void Start()
    {
        pSystem.Play();
    }
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * xVel * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            col.GetComponent<Enemy>().TakeDamage();
            this.gameObject.SetActive(false);
        }

    }
}
