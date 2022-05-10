using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Lava : MonoBehaviour
{
    private void Start()
    {
        GetComponent<BoxCollider>().isTrigger = true;
    }
}