using System.Collections;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public Manager_Role _ManagerRole;

    public CameraMove _CameraMove;

    private Subject_Init _subjectInit;
    private Subject_Select _subjectSelect;

    private bool _enableInput;

    private Role _nowRole;
    private float _nowSpeed;
    private Animator _nowAnim;
    private Transform _nowRoleTrans;
    private SpriteRenderer _nowSpriteRenderer;

    private const float UnitTime = 1.0f;

    private void Awake()
    {
        //
        _subjectInit = gameObject.AddComponent<Subject_Init>();
        _subjectInit.Attach(_ManagerRole);

        _subjectSelect = gameObject.AddComponent<Subject_Select>();
        _subjectSelect.Attach(_CameraMove);

        //
        _enableInput = false;
    }

    private void Start()
    {
        //
        _subjectInit.Notify();

        //
        _nowRoleTrans = _ManagerRole.RoleL0.transform;
        _nowRole = _ManagerRole.RoleL0.GetComponent<Role>();
        _nowAnim = _ManagerRole.RoleL0.GetComponent<Animator>();
        _nowSpriteRenderer = _nowRoleTrans.GetChild(0).GetComponent<SpriteRenderer>();

        // 
        _subjectSelect.Notify(_nowRoleTrans.gameObject);

        // 
        StartCoroutine(WaitEnableInput(3));
    }

    private void Update()
    {
        if (_enableInput)
        {
            // 选取
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                _nowAnim.SetBool("走路", false);

                //
                _nowRoleTrans = _ManagerRole.RoleL0.transform;
                _subjectSelect.Notify(_nowRoleTrans.gameObject);

                _nowSpriteRenderer = _nowRoleTrans.GetChild(0).GetComponent<SpriteRenderer>();
                _nowRole = _nowRoleTrans.gameObject.GetComponent<Role>();
                _nowAnim = _nowRole.GetComponent<Animator>();
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                _nowAnim.SetBool("走路", false);

                //
                _nowRoleTrans = _ManagerRole.RoleL1.transform;
                _subjectSelect.Notify(_nowRoleTrans.gameObject);

                _nowSpriteRenderer = _nowRoleTrans.GetChild(0).GetComponent<SpriteRenderer>();
                _nowRole = _nowRoleTrans.gameObject.GetComponent<Role>();
                _nowAnim = _nowRole.GetComponent<Animator>();
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                _nowAnim.SetBool("走路", false);

                //
                _nowRoleTrans = _ManagerRole.RoleL2.transform;
                _subjectSelect.Notify(_nowRoleTrans.gameObject);

                _nowSpriteRenderer = _nowRoleTrans.GetChild(0).GetComponent<SpriteRenderer>();
                _nowRole = _nowRoleTrans.gameObject.GetComponent<Role>();
                _nowAnim = _nowRole.GetComponent<Animator>();
            }

            // 动画
            _nowAnim.SetBool("走路", false);

            // 移动
            Vector3 dir = Vector3.zero;
            if (Input.GetKey(KeyCode.W)) dir += Vector3.forward;
            if (Input.GetKey(KeyCode.S)) dir += Vector3.back;
            if (Input.GetKey(KeyCode.A)) dir += Vector3.left;
            if (Input.GetKey(KeyCode.D)) dir += Vector3.right;

            if (Input.GetKeyDown(KeyCode.A)) _nowSpriteRenderer.flipX = false;
            if (Input.GetKeyDown(KeyCode.D)) _nowSpriteRenderer.flipX = true;

            _nowSpeed = dir.magnitude;
            if (_nowSpeed != 0)
            {
                _nowAnim.SetBool("走路", true);
                _nowRole.Movement(dir, _nowSpeed);
            }
        }
    }

    private IEnumerator WaitEnableInput(float waitTime)
    {
        float nowTime = 0;
        while (nowTime < waitTime * UnitTime)
        {
            nowTime += Time.deltaTime;
            yield return null;
        }

        _enableInput = true;
    }
}