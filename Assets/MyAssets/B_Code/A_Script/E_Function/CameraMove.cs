using UnityEngine;

public class CameraMove : MonoBehaviour, I_Observer_Select
{
    public float moveSpeed = 1.0f;

    private Transform _targetTransform;

    private void Update()
    {
        if (_targetTransform != null)
        {
            float targetPosX = _targetTransform.position.x;
            var position = gameObject.transform.position;
            float smoothedPosX = Mathf.Lerp(position.x, targetPosX, moveSpeed * Time.deltaTime);

            position = new Vector3(smoothedPosX, position.y, position.z);
            gameObject.transform.position = position;
        }
    }

    public void Handle_Select(GameObject obj)
    {
        _targetTransform = obj.transform;
    }
}