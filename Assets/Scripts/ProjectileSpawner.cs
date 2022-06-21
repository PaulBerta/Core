using Assets.Scripts.Utils;
using System.Collections;
using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _projectile;

    [SerializeField] private FloatScriptableObject _scoreSO;
    [SerializeField] private FloatScriptableObject _timeElapsedSO;

    private GameObject _core;
    private const int _numberOfAngles = 6;
    private const float _spawnRadius = 5.5f;
    
    
    void Start()
    {
        _core = GameObject.Find("Core");
       
        GameManager.Instance.isGameActive = true;
        _previousRandom = _numberOfAngles+1;

        StartCoroutine(SpawnDelayTimer());
    }

    void SpawnObjectAroundAnotherObject(GameObject spawnedObject,GameObject objectToSpawnAround,int numberOfAngles , float spawnRadius)
    {
        var spawnLocation = CalculateSpawningPosition(objectToSpawnAround, numberOfAngles, spawnRadius);
        Instantiate(spawnedObject, spawnLocation, spawnedObject.transform.rotation);
    }

    private int _previousRandom;
    private Vector3 CalculateSpawningPosition(GameObject objectToSpawnAround, int numberOfAngles, float spawnRadius) 
    {
        int random;
        do
        {
            random = Random.Range(1, numberOfAngles + 1);
        } while (random == _previousRandom);

        _previousRandom = random;

        float angle = random * 2 * Mathf.PI / numberOfAngles;
        float x = Mathf.Cos(angle) * spawnRadius;
        float y = Mathf.Sin(angle) * spawnRadius;
        Vector3 spawnLocation = objectToSpawnAround.transform.position + new Vector3(x, y, 0f);

        return spawnLocation;
    }

    private IEnumerator SpawnDelayTimer()
    {
        while (GameManager.Instance.isGameActive)
        {
            var movementDelay = CalculateSpawnDelay();
            yield return new WaitForSeconds(movementDelay);

            SpawnObjectAroundAnotherObject(_projectile, _core, _numberOfAngles, _spawnRadius);
        }
    }

    [SerializeField] private const float _beta = 30f; // the logistic function is 0.5 (the middle) for this value
    [SerializeField] private const float _alfa = 628 / 10000f; // controlls the steepness of the function curve

    [SerializeField] private const float _minSpawnRate = 0.4f;
    [SerializeField] private const float _maxSpawnRate = 1.0f;

    private float CalculateSpawnDelay()
    {
        float delayLenght;

        float exponent = (_timeElapsedSO.Value - _beta) * _alfa;
        delayLenght = Mathf.Clamp(MathUtility.Sigmoid(exponent), _minSpawnRate, _maxSpawnRate);

        return delayLenght;
    }
}
