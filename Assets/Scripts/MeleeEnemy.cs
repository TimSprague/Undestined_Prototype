using UnityEngine;
using System.Collections;

public class MeleeEnemy : EnemyScript {
    //public ParticleSystem EnemyBleed;
    public ParticleSystem EnemyGround;

    // Use this for initialization
    public override void Start () {
        
        base.Start();
        enemyAnim.SetBool("Moving", true);
        enemyAnim.SetFloat("Velocity Z", 1.0f);
        enemyAnim.Play("Unarmed-Walk");

    }

    // Update is called once per frame
    public override void Update() {
        base.Update();
	}
    public void PlayBleedParticle()
    {
        //if(EnemyBleed&&!EnemyBleed.isPlaying)
        //   EnemyBleed.Play(true);
    }
    public void PlayGroundParticle()
    {
        if (EnemyGround && !EnemyGround.isPlaying)
            EnemyGround.Play(true);
    }

    public override void moveToTarget(Vector3 target)
    {
        Vector3 moveDirection = target - transform.position;
        Vector3 velocity = rigidBody.velocity;
        if (moveDirection.magnitude < 5.5f && !playerTarget)
        {
            destPoint = (destPoint + 1) % points.Length;
            planRoute.FindPath(transform.position, points[destPoint].position);
            path = planRoute.grid.path;
            pathDest = 0;
            pathCount = path.Count;

        }
        else
        {
            if (!pause)
            {
                if (Vector3.Distance(playerTransform.position, transform.position) > 3)
                {
                    if (!playerTarget)
                    {
                        moveDirection = path[pathDest].worldPosition - transform.position;


                        if (moveDirection.magnitude < 3.5f)
                        {
                            planRoute.FindPath(transform.position, points[destPoint].position);
                            path = planRoute.grid.path;
                            pathDest = 0;
                            pathCount = path.Count;

                        }
                    }
                    velocity = new Vector3(moveDirection.normalized.x * speed, 0, moveDirection.normalized.z * speed);
                }
                else
                {
                    if (player.isAlive)
                    {
                        //enemyAnim.Play("Attack", PlayMode.StopAll);
                        enemyAnim.SetBool("Moving", false);
                        enemyAnim.SetFloat("Velocity Z", 0);
                        enemyAnim.Play("Unarmed-Attack-L3");
                        player.DecreaseHealth(5);
                        canAttack = true;
                        attackTimer = 1.25f;
                        pause = true;
                        pauseTimer = 1.25f;
                    }
                }
            }
        }
        rigidBody.velocity = velocity;
    }
}
