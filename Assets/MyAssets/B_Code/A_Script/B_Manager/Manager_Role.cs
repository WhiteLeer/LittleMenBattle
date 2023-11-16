using UnityEngine;

public class Manager_Role : MonoBehaviour, I_Observer_Init
{
    public GameObject RoleL0, RoleL1, RoleL2;
    public GameObject RoleR0, RoleR1, RoleR2;

    public Vector3 Position0, Position1, Position2;
    public Vector3 Rotation0, Rotation1, Rotation2;

    // Observer_Init
    public void Handle_Init()
    {
        RoleL0 = Instantiate(RoleL0, Position0, Quaternion.Euler(Rotation0));
        RoleL1 = Instantiate(RoleL1, Position1, Quaternion.Euler(Rotation1));
        RoleL2 = Instantiate(RoleL2, Position2, Quaternion.Euler(Rotation2));

        RoleL0.transform.GetChild(0).GetComponent<SpriteRenderer>().flipX = true;
        RoleL1.transform.GetChild(0).GetComponent<SpriteRenderer>().flipX = true;
        RoleL2.transform.GetChild(0).GetComponent<SpriteRenderer>().flipX = true;

        Position0.x *= -1;
        Position1.x *= -1;
        Position2.x *= -1;

        RoleR0 = Instantiate(RoleR0, Position0, Quaternion.Euler(Rotation0));
        RoleR1 = Instantiate(RoleR1, Position1, Quaternion.Euler(Rotation1));
        RoleR2 = Instantiate(RoleR2, Position2, Quaternion.Euler(Rotation2));
    }
}