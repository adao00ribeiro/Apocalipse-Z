using UnityEngine;
using UnityEngine.Networking;

namespace Snake.ApocalipseZ.Enemys {

    public class EnemyZombie : EnemyBase {


        private new void Start() {
            base.Start();
            target = Vector3.zero;
            var startRand = Random.Range(5, 20);
            InvokeRepeating("SetStateAndAnimations", startRand, startRand);
        }

        [ServerCallback]
        private void Update()
        {
            if (Vector3.Distance(transform.position, player.transform.position) > distanceForWalk)
            {
                state = State.WALK;
            }

            if (Vector3.Distance(transform.position, player.transform.position) < distanceForFollow)
            {
                FollowAndAttack(speedMoviment);
                state = State.FOLLOW;
            }
            if (Vector3.Distance(transform.position, player.transform.position) < distanceForAtack)
            {
                state = State.ATTACK;
                if (atack == true)
                {
                    StartCoroutine("Batendo");
                    Debug.Log("Bateu");
                }
            }
            else
            {
                atack = true;
            }

            {
                SetAnimations();

                if (state == State.IDLE)
                {
                    agent.speed = 0;
                    agent.SetDestination(transform.position);
                }

                if (state == State.WALK)
                {
                    if (Time.time > timer)
                    {
                        timer = Time.time + timeToRand;
                        var newPos = Random.insideUnitSphere * distancePatrol;
                        newPos += transform.position;
                        target = newPos;
                    }

                    agent.speed = 0.5f;
                    agent.SetDestination(target);
                }

                if (state == State.FOLLOW)
                {
                    FollowAndAttack(speedMoviment);
                }
                if (state == State.ATTACK)
                    FollowAndAttack(speedMoviment * 0);
            }
        }


        private void FollowAndAttack(float speedMoviment) {
            CancelInvoke();
            agent.speed = speedMoviment;
            agent.SetDestination(player.position);
        }


        private void FollowSound() {
            var distance = Vector3.Distance(transform.position, player.position);

            if(distance <= 10 && Input.GetMouseButton(0))
                state = State.ATTACK;
        }


        private void SetAnimations() {
            anim.animator.SetFloat("Move", agent.velocity.magnitude, 0.5f, Time.deltaTime);
        }


        private void SetStateAndAnimations() {
            var rand = Random.Range(0, 100);
            state = (rand >= 35) ? State.WALK : State.IDLE;
        }


        private void DisableEnemy() {
            OnToggleShared.Invoke(false);
        }


        private void EnableEnemy() {
            OnToggleShared.Invoke(true);
        }


        private void Respawn() {
            EnableEnemy();
        }


        protected override void OnDamage() {
            anim.SetTrigger("Hit");
            state = State.ATTACK;
        }


        protected override void OnDie() {
            DisableEnemy();
            Invoke("Respawn", respawnTime);
        }
    }
}