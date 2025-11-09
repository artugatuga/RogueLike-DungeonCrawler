using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] GameObject perkItemPrefab;
    [SerializeField] float dropChance = 10f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnItem()
    {
        if (dropChance > Random.Range(0, 100))
        {
            Vector3 position = transform.position + new Vector3(0, 0.5f, 0);
            GameObject item = Instantiate(perkItemPrefab, position, Quaternion.identity);
        }
    }
}
