using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KamikazeScript : EnemyScript {

    protected override void Start() {
        base.Start();     
    }

    protected override void Update() {
        base.Update();
    }

    protected override void FixedUpdate() {
        base.FixedUpdate();
    }

    protected override void Shoot() {}

    protected override void Movement() {

        float step = speed * Time.deltaTime;

        if (playerController.isAlive) {
            if (timeRemaining > 4.5f) {
                timeRemaining -= Time.deltaTime;
                transform.Translate(Vector3.forward * step);

            } else {
                speed = 15;
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position, step);
                transform.LookAt(player.transform);
            }
        } else if (!playerController.isAlive) {
            speed = 15;
            transform.position = Vector3.MoveTowards(transform.position, placeholderCenter.transform.position, step);
            transform.LookAt(placeholderCenter.transform);
        }

        
    }

}
