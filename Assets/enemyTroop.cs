using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;

public class enemyTroop : MonoHelper
{
    public int health = 100;
    public bool canBeHurt = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        //GRAVITY
        //transform.position -= new Vector3(0, 2 * Time.deltaTime, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "PlayerHitbox" && canBeHurt)
        {
            shaker.ShakeOneShotDirectional(player.transform.position - transform.position,0.03f);
            health -= 45;
            canBeHurt = false;
            Invoke("ReHurt", 0.6f);
            var newPoss = (other.transform.position - transform.position) * 0.3f;
            
            transform.position -= newPoss;  //Vector3.Lerp(transform.position, transform.position - (other.transform.position - transform.position) * 1, Time.deltaTime);
            transform.position = new Vector3(transform.position.x, -1, transform.position.z);
        }

        if (health <= 0)
        {
            Destroy(this.gameObject);
            FindObjectOfType<PlayerController>().counter++;
        }

    }

    void ReHurt()
    {
        canBeHurt = true;
    }
}
