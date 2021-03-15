using UnityEngine;

namespace BreadAndButter.AI
{
    public class Spawner : MonoBehaviour
    {
        public Vector3 size = Vector3.one;
        public Vector3 center = Vector3.zero;

        [SerializeField, Tooltip("Use the objects y position always when spawning an object.")]
        private bool floorYPosition = false;
        [SerializeField]
        private Vector2 spawnRate = new Vector2(0, 1); 

        [SerializeField]
        private bool shouldSpawnBoss = false;
        [SerializeField, Range(0, 100)]
        private float bossSpawnChance = 1;

        [SerializeField]
        private GameObject bossPrefab = null;
        [SerializeField]
        private GameObject enemyPrefab = null;

        private float time = 0;
        private float timeStep = 0;

        public void Spawn()
        {
            GameObject prefab = shouldSpawnBoss && Random.Range(0, 100) < bossSpawnChance ? bossPrefab : enemyPrefab;
            Vector3 position = transform.position + new Vector3(
                Random.Range(-size.x * 0.5f, size.x * 0.5f),
                floorYPosition ? 0 : Random.Range(-size.y * 0.5f, size.y * 0.5f),
                Random.Range(-size.z * 0.5f, size.z * 0.5f)) + center;

            position = transform.InverseTransformPoint(position);

            Instantiate(prefab, position, transform.rotation, transform);

            timeStep = Random.Range(spawnRate.x, spawnRate.y);
            time = 0;
        }

        void Start()
        {
            timeStep = Random.Range(spawnRate.x, spawnRate.y);
        }

        void Update()
        {
            if (time < timeStep)
            {
                time += Time.deltaTime * Time.timeScale;
            }
            else
            {
                Spawn();
            }
        }

#if UNITY_EDITOR
        // Implement this OnDrawGizmos if you want to draw gizmos that are also pickable and always drawn
        private void OnDrawGizmos()
        {
            // Store the default matrix
            Matrix4x4 baseMatrix = Gizmos.matrix;

            // Make the gizmos use the objects matrix
            Matrix4x4 rotationMatrix = transform.localToWorldMatrix;
            Gizmos.matrix = rotationMatrix;

            // Draw a green, partially transparent cube lol
            Gizmos.color = new Color(0, 1, 0, 0.5f);
            Gizmos.DrawCube(center, size);

            // Reset the gizmos matrix back to default
            Gizmos.matrix = baseMatrix;
        }
#endif
    }
}