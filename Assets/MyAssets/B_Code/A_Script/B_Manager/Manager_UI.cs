using System.Collections;
using UnityEngine;

public class Manager_UI : MonoBehaviour
{
    public const float UnitTime = 1.0f;

    public bool AlphaIsOne(string targetName)
    {
        GameObject obj = GameObject.Find(targetName);
        var component = obj.GetComponent<CanvasGroup>();

        int alpha = (int)component.alpha;
        return (alpha == 1);
    }

    public IEnumerator WaitChangeAlpha(string targetName, float waitTime)
    {
        float nowTime = 0;
        while (nowTime < waitTime * UnitTime)
        {
            nowTime += Time.deltaTime;
            yield return null;
        }

        StartCoroutine(ChangeAlpha(targetName));
    }

    public IEnumerator ChangeAlpha(string targetName)
    {
        GameObject obj = GameObject.Find(targetName);
        var component = obj.GetComponent<CanvasGroup>();

        var alpha = component.alpha;
        float value0 = alpha, value1 = 1 - alpha;

        // 响应一次
        component.blocksRaycasts = !AlphaIsOne(targetName);

        // 平滑变化
        float nowTime = 0;
        while (nowTime < UnitTime)
        {
            nowTime += Time.deltaTime;
            component.alpha = Mathf.Lerp(value0, value1, nowTime / UnitTime);
            yield return null;
        }

        component.alpha = value1;
    }
}