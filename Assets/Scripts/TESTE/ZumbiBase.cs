using UnityEngine;
using UnityEngine.AI;
using System.Collections;

namespace Snake.ApocalipseZ.Enemy
{

    public enum State { IDLE, WALK, FOLLOW, ATTACK, DIE }

    public abstract class ZumbiBase : MonoBehaviour
    {

        [SerializeField] public State state;

        [Header("Settings Character")]
        public float health;
        public float dano;
        public float speedMoviment;
        public float distancePatrol;

        [Header("Settings Actions")]
        public float TimeAttack;
        public float distanceForAtack;
        public float distanceForFollow;
        public float distanceForWalk;
        public float distanceOfPlayer;
        public bool atack;

        [Header("Settings Audio")]
        public AudioClip attack;
        public AudioClip hit;
        public AudioClip die;

        protected Animator anim;
        protected AudioSource audioSource;
        protected NavMeshAgent agent;
        protected float speed;
        protected float distance;
        protected bool isDied;
        protected Vector3 target;
        protected Transform player;
        protected float timer;
        protected float timeToRand = 5;


        protected void Start()
        {
            anim = GetComponent<Animator>();
            audioSource = GetComponent<AudioSource>();
            agent = GetComponent<NavMeshAgent>();
            player = GameObject.FindWithTag("Player").transform;
        }


        public void ApplyDamage(float hit)
        {
            if (isDied) return;

            health -= hit;

            if (health <= 0 && !isDied)
            {
                isDied = true;
                state = State.DIE;
                anim.Play("Die");
            }
            else
                OnDamage();
        }

        protected abstract void OnDamage();
        protected abstract void OnDie();
        IEnumerator Batendo()
        {
            atack = false;
            yield return new WaitForSeconds(2);
            atack = true;
            yield return new WaitForSeconds(TimeAttack);
            atack = false;
        }
    }
}