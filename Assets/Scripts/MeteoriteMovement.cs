using Assets.Scripts.Utils;
using System.Collections;
using UnityEngine;

public class MeteoriteMovement : MonoBehaviour
{
    [SerializeField] private float _meteoriteSpeed = 0f;
    [SerializeField] private FloatScriptableObject _timeElapsedSO;

    private GameObject _core;

    void Start()
    {
        _core = GameObject.Find("Core");
        StartCoroutine(MoveDelay());
    }

    void Update()
    {
        MoveTowardsObjectAtSpeed(_core, _meteoriteSpeed);
    }

    private void MoveTowardsObjectAtSpeed(GameObject objectToGoTowards, float speed)
    {
        transform.position = Vector3.MoveTowards(transform.position, objectToGoTowards.transform.position, Time.deltaTime * speed);
    }

    private void SetSpeed(float speed)
    {
        _meteoriteSpeed = speed;
        Debug.LogWarning(speed);
    }
    
    private const float _beta = 30f; // the logistic function is 0.5 (the middle) for this value
    private const float _alfa = 628 / 10000f; // controlls the steepness of the function curve

    private const float _minMeteoriteSpeed = 2f;
    private const float _maxMeteoriteSpeed = 20f;

    private float CalculateMeteoriteSpeed()
    {
        float speed;

        float exponent = -(_timeElapsedSO.Value - _beta) * _alfa;
        speed = Mathf.Clamp(_maxMeteoriteSpeed * MathUtility.Sigmoid(exponent), _minMeteoriteSpeed, _maxMeteoriteSpeed);

        return speed;
    }

    private IEnumerator MoveDelay()
    {
        yield return new WaitForSeconds(0.5f);
        SetSpeed(CalculateMeteoriteSpeed());
    }
}
