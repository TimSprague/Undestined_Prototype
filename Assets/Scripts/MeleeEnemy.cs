using UnityEngine;
using System.Collections;

public class MeleeEnemy : EnemyScript {
    //public ParticleSystem EnemyBleed;
    public ParticleSystem EnemyGround;
    // Use this for initialization
    public override void Start () {
        base.Start();
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
}
