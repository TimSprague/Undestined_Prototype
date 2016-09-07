using UnityEngine;
using System.Collections;

public class MeleeEnemy : EnemyScript {
    public ParticleSystem EnemyBleed;
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
        if(EnemyBleed&&!EnemyBleed.isPlaying)
           EnemyBleed.Play(true);
    }
}
