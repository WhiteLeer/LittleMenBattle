using BehaviorDesigner.Runtime.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class Score : Manager_UI
{
    private GameObject[] hearts;

    void Start()
    {
        hearts = new GameObject[3];
        hearts[0] = transform.GetChild(0).GameObject();
        hearts[1] = transform.GetChild(1).GameObject();
        hearts[2] = transform.GetChild(2).GameObject();
    }

    public void ChangeHeart()
    {
        for (int i = 0; i < 3; i++)
        {
            if (AlphaIsOne(hearts[i].name))
            {
                StartCoroutine(ChangeAlpha(hearts[i].name));
                break;
            }

            if (i == 2) Debug.Log("End");
        }
    }
}