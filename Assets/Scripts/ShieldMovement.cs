using UnityEngine;
using Assets.Scripts.Utils;

public class ShieldMovement : MonoBehaviour
{

    [SerializeField] private float _speed;

    private GameObject _core;

    void Start()
    {
        _speed = 700;
        _core = GameObject.Find("Core");
    }

    void Update()
    {
        RotateAroundObject(_core, _speed);
    }

    [SerializeField] private const float _beta = 0.75f; // minimum speed multiplier
    [SerializeField] private const float _alfa = 0.25f; // cotrols the steepnes of the movementSpeed parabola graph

    public void RotateAroundObject(GameObject objectToRotateAround, float speed)
    {
        var direction = Input.GetAxis("Horizontal");
        var rawDirection = Input.GetAxisRaw("Horizontal");
        var movementSpeed = speed * MathUtility.Parabola(_alfa, direction, _beta);

        transform.RotateAround(objectToRotateAround.transform.position, Vector3.back, rawDirection * Time.deltaTime * movementSpeed);
        
    }

}
