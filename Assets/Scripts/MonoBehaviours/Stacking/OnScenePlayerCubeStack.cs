using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class OnScenePlayerCubeStack : MonoBehaviour
{
    [SerializeField] private int _cubeNumber = 0;
    [SerializeField] private PlayerCube _cubeObject;
    private List<PlayerCube> _stack;

    public List<PlayerCube> Stack => _stack;

    private void Start()
    {
        GetComponent<BoxCollider>().isTrigger = true;
        foreach (PlayerCube cube in _stack)
        {
            Rigidbody rigidbody = cube.GetComponent<Rigidbody>();
            if (rigidbody) rigidbody.isKinematic = true;
        }
    }

    private void OnValidate()
    {
        List<PlayerCube> cubeToDelete = new List<PlayerCube>();
        foreach (PlayerCube cube in transform.GetComponentsInChildren<PlayerCube>())
        {
            cubeToDelete.Add(cube);
        }
        foreach (PlayerCube cube in cubeToDelete)
        {
            StartCoroutine(DestroyInEditMode(cube.gameObject));
        }
        _stack = new List<PlayerCube>();
        for (int i = 0; i < _cubeNumber; i++)
        {
            PlayerCube cube = Instantiate(
                _cubeObject, 
                transform.position + Vector3.up * i * _cubeObject.Height, 
                transform.rotation, 
                transform
                );
            _stack.Add(cube);
        }
    }

    private IEnumerator DestroyInEditMode(GameObject gameObject)
    {
        yield return new WaitForEndOfFrame();
        DestroyImmediate(gameObject);
    }
}