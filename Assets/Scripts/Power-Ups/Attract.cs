using UnityEngine;

public class Attract : MonoBehaviour
{
    [SerializeField]
    private float myAttractReach = 8f;
    [SerializeField]
    private float myAttractSpeed = 2f;
    private Player myPlayer = null;

    void Start()
    {
        myPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        if (myPlayer == null)
        {
            Debug.LogError("Error: myPlayer missing");
        }
    }

    void Update()
    {
        if (myPlayer == null)
        {
            return;
        }
        AttractObject();
    }

    private void AttractObject()
    {
        if (Vector3.Distance(transform.position, myPlayer.transform.position) <= myAttractReach)
        {
            Vector3 dir = (myPlayer.transform.position - transform.position).normalized;
            transform.position += dir * Time.deltaTime * myAttractSpeed;
        }
    }
}
