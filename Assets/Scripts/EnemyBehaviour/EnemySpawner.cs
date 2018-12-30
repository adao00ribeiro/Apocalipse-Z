using UnityEngine;
using UnityEngine.Networking;

namespace Snake.ApocalipseZ.Enemys {
    
    public class EnemySpawner : NetworkBehaviour {
        [SerializeField] private GameObject enemyPrefab;
        [SerializeField] private float distance;
        [SerializeField] private int enemyNumber;

        [ServerCallback]
        private void Start() {
            for(int i = 0; i < enemyNumber; i++) {
                var newPos = Random.insideUnitSphere * distance;
                newPos += transform.position;

                var loadPosition = new Vector3(newPos.x, transform.position.y, transform.position.z);

                var enemy = Instantiate(enemyPrefab, loadPosition, transform.rotation);
                enemy.GetComponent<NetworkIdentity>().localPlayerAuthority = false;

                NetworkServer.Spawn(enemy);
            }
        }
    }
}