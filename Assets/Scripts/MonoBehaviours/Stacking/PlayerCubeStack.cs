using System.Collections.Generic;
using UnityEngine;
using LeoEcsPhysics;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(OnTriggerEnterChecker))]
public class PlayerCubeStack : MonoBehaviour
{
    [SerializeField] private List<PlayerCube> _stack;
    [SerializeField] private float _height;

    public List<PlayerCube> Stack => _stack;

    private void Start()
    {
        GetComponent<Collider>().isTrigger = true;
        _height = 0f;
        foreach (PlayerCube cube in _stack)
        {
            _height += cube.Height;
            cube.PlayerCubeStack = this;
        }
    }

    public void AddToStack(List<PlayerCube> cubeList)
    {
        foreach (PlayerCube cube in cubeList)
        {
            cube.transform.position = transform.position + Vector3.up * (_height + cube.Height / 2);
            _height += cube.Height;
            _stack.Insert(0, cube);
            cube.transform.parent = transform;
            cube.PlayerCubeStack = this;
        }
    }

    public List<PlayerCube> PopFromStack(int cubeNumber)
    {
        List<PlayerCube> popedCubes = new List<PlayerCube>();
        for (int i = 0; i < cubeNumber; i++)
        {
            PlayerCube cube = _stack[_stack.Count - 1];
            popedCubes.Add(cube);
            cube.transform.parent = null;
            cube.PlayerCubeStack = null;
            _height -= cube.Height;
            _stack.RemoveAt(_stack.Count - 1);
        }
        return popedCubes;
    }

    public void RemoveFromStack(PlayerCube cubeToRemove)
    {
        foreach (PlayerCube cube in _stack)
        {
            if (cube == cubeToRemove)
            {
                cube.transform.parent = null;
                cube.PlayerCubeStack = null;
                _stack.Remove(cube);
                break;
            }
        }
    }

    private void PushUpCubesByHeight(float height)
    {
        foreach (PlayerCube cube in _stack)
        {
            cube.transform.position += Vector3.up * height;
        }
    }

    private void PushDownCubesByHeight(float height)
    {
        PushUpCubesByHeight(-height);
    }
}