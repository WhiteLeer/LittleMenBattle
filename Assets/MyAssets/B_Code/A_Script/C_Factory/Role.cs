using UnityEngine;

public interface I_Role
{
    void Movement(Vector3 dir, float speed);
    void UseSkill();
    void Recover();
    void Hurt();
    void Roll();
    void Fall();
    void Shot();
}

public class Role : MonoBehaviour, I_Role
{
    const float speedValue = 10;

    public Animator _anim;

    public void Movement(Vector3 dir, float speed)
    {
        Vector3 v = dir * speedValue * speed;
        this.transform.Translate(v * Time.deltaTime);

        _anim.speed = speed;
        _anim.Play("走路");
    }

    public void UseSkill()
    {
        throw new System.NotImplementedException();
    }

    public void Recover()
    {
        throw new System.NotImplementedException();
    }

    public void Hurt()
    {
        throw new System.NotImplementedException();
    }

    public void Roll()
    {
        throw new System.NotImplementedException();
    }

    public void Fall()
    {
        throw new System.NotImplementedException();
    }

    public void Shot()
    {
        throw new System.NotImplementedException();
    }
}