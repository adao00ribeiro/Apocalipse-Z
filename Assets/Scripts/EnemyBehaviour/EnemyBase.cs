using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.Networking;
using Snake.ApocalipseZ.Interface;

namespace Snake.ApocalipseZ.Enemys {

    public enum State { IDLE, WALK, FOLLOW, ATTACK, DIE }

    [System.Serializable]
    public class ToggleEvent : UnityEvent<bool>{}
    
    public abstract class EnemyBase : NetworkBehaviour, IHealth {

        [SerializeField] protected ToggleEvent OnToggleShared;

        [SerializeField] public State state;

        [Header("Settings Character")]
        [SerializeField] protected float health;
        [SerializeField] protected float respawnTime;
        [SerializeField] public int dano;
        [SerializeField] protected float speedMoviment;
        [SerializeField] protected float distancePatrol;

        [Header("Settings Actions")]
        [SerializeField] protected float TimeAttack;
        [SerializeField] protected float distanceForAtack;
        [SerializeField] protected float distanceForFollow;
        [SerializeField] protected float distanceForWalk;
        [SerializeField] protected float distanceOfPlayer;
        [SerializeField] protected bool atack;

        [Header("Settings Audio")]
        public AudioClip attack;
        public AudioClip hit;
        public AudioClip die;

        protected NetworkAnimator anim;
        protected AudioSource audioSource;
        protected NavMeshAgent agent;
        protected float speed;
        protected float distance;
        protected bool isDied;
        protected Vector3 target;
        [SerializeField]
        protected Transform player;
        protected float timer;
        protected float timeToRand = 5;
        protected float currentHealth;


        protected void Start() {
            anim = GetComponent<NetworkAnimator>();
            audioSource = GetComponent<AudioSource>();
            agent = GetComponent<NavMeshAgent>();
            player = GameObject.FindWithTag("Player").transform;
        }

        private void OnEnable() {
            currentHealth = health;
        }

        [Server]
        public bool ApplyDamage(float hit)
        {
            bool died = false;
			
			currentHealth -= hit;
            died = currentHealth <= 0;

            OnDamage();
            RpcDeath(died);
            return died;
		}

        [ClientRpc]
        private void RpcDeath(bool died) {
            if(die) 
                OnDie();
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