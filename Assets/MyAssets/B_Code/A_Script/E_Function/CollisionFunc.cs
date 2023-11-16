using UnityEngine;

public class CollisionFunc : MonoBehaviour
{
    private Score score0, score1;
    private Rigidbody myRigidbody;

    private void Start()
    {
        score0 = GameObject.Find("Heart0").GetComponent<Score>();
        score1 = GameObject.Find("Heart1").GetComponent<Score>();

        myRigidbody = GetComponent<Rigidbody>();
    }

    public void OnCollisionEnter(Collision other)
    {
        string otherName = other.gameObject.name;
        Vector3 reboundDir = transform.position - other.transform.position;
        reboundDir.Set(reboundDir.x, 0, reboundDir.z);
        reboundDir = Vector3.Normalize(reboundDir);

        switch (otherName)
        {
            case "Door0":
            {
                score0.ChangeHeart();
                myRigidbody.AddForce(reboundDir * 5000);
                break;
            }
            case "Door1":
            {
                score1.ChangeHeart();
                myRigidbody.AddForce(reboundDir * 5000);
                break;
            }
            default: break;
        }
    }
}