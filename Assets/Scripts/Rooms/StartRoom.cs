using UnityEngine;

public class StartRoom : MonoBehaviour
{
    [SerializeField] private int dificultyLevel = 1;
[SerializeField] private GameObject slimePrefab;
[SerializeField] private GameObject tankPrefab;
[SerializeField] private GameObject cactusPrefab;
[SerializeField] private GameObject enemyParent;
[SerializeField] private BoxCollider arenaBox;

[SerializeField] private bool finalRoom = false;
[SerializeField] private GameObject ending;

private int amountOf;
private int amountOfMelee;
private int amountOfTank;
private int amountOfRange;

private GameObject[] doors;

private bool started = false;
private bool finished = false;



// Start is called before the first frame update
void OnTriggerEnter(Collider other)
{
    if (!other.CompareTag("Player") || started) return;
    
    doors = GameObject.FindGameObjectsWithTag("Door");
    foreach (GameObject door in doors)
    {
        door.GetComponent<CloseAndOpenDoor>().Close();
    }
    
    amountOf = Mathf.RoundToInt((3 * Mathf.Sqrt(dificultyLevel)));
        if (dificultyLevel > 7)
        {
            for (int i = 0; i < amountOf/4; i++)
            {
                amountOfTank++;
                amountOf--;
            }
        }
        if (dificultyLevel > 3)
        {
            for (int i = 0; i < amountOf/2; i++)
            {
                amountOfRange++;
                amountOf--;
            }
        }
        for (int i = 0; i < amountOf; i++)
        {
            amountOfMelee++;
        }
        
        StartThisRoom();
        started = true;
}

void Update()
{
    if (enemyParent.transform.childCount <= 0 && started && !finished)
    {
        finished = true;
        foreach (GameObject door in doors)
        {
            door.GetComponent<CloseAndOpenDoor>().Open();
        }

        if (finalRoom)
        {
            ending.SetActive(true);
        }
    }
}

void StartThisRoom()
{
    for (int i = 0; i < amountOfMelee; i++)
    {
        //Vector3 randomTile =tiles[Random.Range(0, tiles.Count)];
        Instantiate(slimePrefab, GetRandomPosition() + Vector3.up*2, Quaternion.identity ,enemyParent.transform);
        amountOfMelee--;
    }
    for (int i = 0; i < amountOfRange; i++)
    {
        //Vector3 randomTile =tiles[Random.Range(0, tiles.Count)];
        Instantiate(cactusPrefab, GetRandomPosition() + Vector3.up*2, Quaternion.identity ,enemyParent.transform);
        amountOfRange--;
    }
    for (int i = 0; i < amountOfTank; i++)
    {
        //Vector3 randomTile =tiles[Random.Range(0, tiles.Count)];
        Instantiate(tankPrefab, GetRandomPosition() + Vector3.up*4, Quaternion.identity ,enemyParent.transform);
        amountOfTank--;
    }
    
    
}

public Vector3 GetRandomPosition()
{
    Vector3 center = arenaBox.center;
    Vector3 size = arenaBox.size;
    float x = Random.Range(-size.x / 2f, size.x / 2f);
    float z = Random.Range(-size.z / 2f, size.z / 2f);
    return arenaBox.transform.TransformPoint(center + new Vector3(x, 0, z));
}

}
