using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;
using UnityEngine.AI;

public class enemyTroop : MonoHelper
{
    public int health = 100;
    public bool canBeHurt = true;
    public AudioSource AudioSource;
    public AudioSource AudioSource2;

    public AudioClip[] HitCrackSounds;
    public AudioClip[] HitPunchSounds;

    public NavMeshAgent Agent;
    // Start is called before the first frame update
    void Start()
    {
        Agent = GetComponent<NavMeshAgent>();
        StartCoroutine(AgentUpdate());
    }

    IEnumerator AgentUpdate()
    {
        while(true)
        {
            Agent.destination = player.transform.position;
            
            yield return new WaitForSeconds(0.5f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "PlayerHitbox" && canBeHurt)
        {
            shaker.ShakeOneShotDirectional(player.transform.position - transform.position,0.1f);
            AudioSource.PlayOneShot(HitCrackSounds[Random.Range(0, HitCrackSounds.Length - 1)]);
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
