using UnityEngine;

public class ProjectileCollisionHandler : MonoBehaviour
{
    [SerializeField] private FloatScriptableObject _scoreSO;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Shield"))
        {
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Core"))
        {
            GameManager.Instance.GameOver();
        }
        else if(collision.gameObject.CompareTag("Perfect Block"))
        {
            Destroy(gameObject);
            _scoreSO.Value += 5;
        }
    }
}
