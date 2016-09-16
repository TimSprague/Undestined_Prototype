﻿using UnityEngine;
using System.Collections;

public class RangedEnemy : EnemyScript {
    public GameObject bullet;
    // Use this for initialization
    public override void Start () {
        base.Start();
	}

    // Update is called once per frame
    public override void Update () {
        base.Update();
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
                if (Vector3.Distance(playerTransform.position, transform.position) > 12)
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
                    enemyAnim.Stop();
                    enemyAnim.Play("Attack", PlayMode.StopAll);
                    player.DecreaseHealth(5);
                    canAttack = true;
                    attackTimer = 1.25f;
                    pause = true;
                    pauseTimer = 1.25f;
                }
            }
        }
        rigidBody.velocity = velocity;
    }
}