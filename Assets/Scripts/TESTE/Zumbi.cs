using UnityEngine;

namespace Snake.ApocalipseZ.Enemy
{

    public class Zumbi : ZumbiBase
    {


        private new void Start()
        {
            atack = true;
            state = State.IDLE;
            base.Start();
            target = Vector3.zero;
            InvokeRepeating("SetStateAndAnimations", 5, 5);
        }


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
                    STATUSPLAYER.currentLife -= dano;
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

                    agent.speed = speedMoviment;
                    agent.SetDestination(target);
                }

                if (state == State.FOLLOW)
                {
                    FollowAndAttack(speedMoviment);
                }
                if (state == State.ATTACK)
                    FollowAndAttack(speedMoviment * 0);


                // FollowSound();
                //SetAnimations();

            }
        }
        void FollowAndAttack(float speedMoviment)
        {
            CancelInvoke();
            agent.speed = speedMoviment;
            agent.SetDestination(player.position);

        }

        void FollowSound()
        {
            var distance = Vector3.Distance(transform.position, player.position);
            state = State.ATTACK;

        }


        void SetAnimations()
        {
            anim.SetFloat("Move", agent.velocity.magnitude, 0.5f, Time.deltaTime);
        }


        void SetStateAndAnimations()
        {
            var rand = Random.Range(0, 100);
            state = (rand >= 35) ? State.WALK : State.IDLE;
        }

        void Follow()
        {
            agent.destination = player.position;
        }

        protected override void OnDamage()
        { }
        protected override void OnDie()
        { }

    }

}
