using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Geep : MonoBehaviour
{
    Transform targets;
    Animator animator;
    public Transform Player;
    public float speed;
    public int Maxhealth = 100;
    public int Curhealth;

    float enemyMoveSpeed = 10f;
    float smoothTime = 0.2f; // ���� �ð�

    private Rigidbody _enemyRigidbody;
    private Vector3 previousPosition; // ���� ��ġ ���� ����
    private Vector3 currentVelocity; // SmoothDamp�� ����� �ӵ� ����

    private void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(UpdateTargetCoroutine());
        TryGetComponent(out _enemyRigidbody);
    }

    private IEnumerator UpdateTargetCoroutine()
    {
        while (true)
        {
            UpdateTarget();
            yield return new WaitForSeconds(0.25f);
        }
    }

    private void UpdateTarget()
    {
        Collider[] Cols = Physics.OverlapSphere(transform.position, 10f);
        Debug.Log(Physics.OverlapSphere(transform.position, 10f, 1 << 8));
        if (Cols.Length > 0)
        {
            float distance = Mathf.Infinity;
            for (int i = 0; i < Cols.Length; i++)
            {
                if (Cols[i].tag == "Player")
                {
                    Debug.Log("Target found");
                    float newDistance = Vector3.Distance(transform.position, Cols[i].transform.position);
                    if (newDistance < distance)
                    {
                        distance = newDistance;
                        targets = Cols[i].gameObject.transform;
                    }
                }
            }
        }
        Collider[] Colss = Physics.OverlapSphere(transform.position, 3f);
    }

    private void Update()
    {
      
            if (targets != null)
            {
                targets.position = new Vector3(targets.position.x, transform.position.y, targets.position.z);
                Vector3 dir = targets.position - transform.position;

                // ���� ��� ������ �Ÿ� ���
                float distanceToTarget = Vector3.Distance(transform.position, targets.position);

                if (distanceToTarget <= 2f) // �÷��̾ ���� ���� ���� �ִ��� Ȯ��
                {
                    animator.SetBool("Run", false);
                    animator.SetBool("Attack", true);
                }
                else if (distanceToTarget <= 10f) // �÷��̾ �ν� ���� ���� �ִ��� Ȯ��
                {
                    animator.SetBool("Run", true);
                    animator.SetBool("Attack", false);
                }
                else
                {
                    animator.SetBool("Run", false);
                    animator.SetBool("Attack", false);
                }


                animator.SetBool("Run", true);

                // SmoothDamp�� ����� ��ġ ����
                Vector3 targetPosition = previousPosition + dir.normalized * enemyMoveSpeed * Time.deltaTime;
                transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, smoothTime);

                _enemyRigidbody.MovePosition(transform.position);

                // ������ ȸ��
                Quaternion targetRotation = Quaternion.LookRotation(dir);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, smoothTime);

                // ��ġ ������ ���� ���� ��ġ ������Ʈ
                previousPosition = transform.position;



            }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            targets.position = new Vector3(other.transform.position.x, transform.position.y, other.transform.position.z);
            transform.LookAt(targets.position);
        }
    }
}