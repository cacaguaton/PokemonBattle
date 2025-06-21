using UnityEngine;

public class DestoyInsSeconds : MonoBehaviour
{
    [SerializeField]
    private float _destroyAfterSeconds = 2f;
    private void Start()
    {
        Destroy(gameObject, _destroyAfterSeconds);
    } 
}
