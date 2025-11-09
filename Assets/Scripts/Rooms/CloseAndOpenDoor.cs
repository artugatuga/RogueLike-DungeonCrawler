using UnityEngine;

public class CloseAndOpenDoor : MonoBehaviour
{
    [SerializeField] private float startRotationY;
    private bool open = false;
    private bool close = false;

    void Start()
    {
        startRotationY = transform.eulerAngles.y;
    }
    void Update()
    {
        if (open)
        {
            Open();
        }

        if (close)
        {
            Close();
        }
    }
    
    public void Open()
    {
        open = true;
        Quaternion target = Quaternion.Euler(0, startRotationY, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * 15f);
        if (transform.rotation.y >= target.y)
        {
            open = false;
        }
    }

    public void Close()
    {
        close = true;
        Quaternion target = Quaternion.Euler(0, startRotationY - 90, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * 15f);
        if (transform.rotation.y <= target.y)
        {
            close = false;
        }
    }
}
