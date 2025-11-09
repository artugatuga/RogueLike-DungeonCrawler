using Unity.Mathematics;
using UnityEngine;

public class CloseAndOpenDoor : MonoBehaviour
{
    [SerializeField] private float startRotationY;


    void Start()
    {
        startRotationY = transform.eulerAngles.y;
    }


    
    public void Open()
    {
        transform.rotation = Quaternion.Euler(0f, startRotationY, 0f);
    }

    public void Close()
    {
        transform.rotation = Quaternion.Euler(0f, startRotationY - 90, 0f);
    }
}
