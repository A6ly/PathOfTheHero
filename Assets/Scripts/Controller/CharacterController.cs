using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    GridObject gridObject;
    Character character;
    CharacterAnimator characterAnimator;

    List<Vector3> pathWorldPositions;

    float moveSpeed = 1.0f;

    private void Awake()
    {
        gridObject = GetComponent<GridObject>();
        character = GetComponent<Character>();
        characterAnimator = GetComponent<CharacterAnimator>();
    }

    private void RotateCharacter(Vector3 originPosition, Vector3 destinationPosition)
    {
        Vector3 direction = (destinationPosition - originPosition).normalized;
        direction.y = 0;
        transform.rotation = Quaternion.LookRotation(direction);
    }

    private void RotateCharacter(Vector3 towards)
    {
        Vector3 direction = (towards - transform.position).normalized;
        direction.y = 0;
        transform.rotation = Quaternion.LookRotation(direction);
    }

    public void Move(List<PathNode> path)
    {
        pathWorldPositions = StageManager.Instance.StageGrid.ConvertPathNodesToWorldPositions(path);

        StageManager.Instance.StageGrid.RemoveObject(gridObject.positionOnGrid, gridObject);

        StartCoroutine(Moving());

        gridObject.positionOnGrid.x = path[path.Count - 1].posX;
        gridObject.positionOnGrid.y = path[path.Count - 1].posY;
    }

    private IEnumerator Moving()
    {
        characterAnimator.StartMoving();

        while (pathWorldPositions != null && pathWorldPositions.Count > 0)
        {
            if (Vector3.Distance(transform.position, pathWorldPositions[0]) > 0.05f)
            {
                transform.position = Vector3.MoveTowards(transform.position, pathWorldPositions[0], moveSpeed * Time.deltaTime);
            }
            else
            {
                pathWorldPositions.RemoveAt(0);

                if (pathWorldPositions.Count == 0)
                {
                    characterAnimator.StopMoving();
                }
                else
                {
                    RotateCharacter(transform.position, pathWorldPositions[0]);
                }
            }

            yield return null;
        }

        StageManager.Instance.StageGrid.PlaceObject(gridObject.positionOnGrid, gridObject);
    }

    public void Attack(GridObject targetGridObject)
    {
        StartCoroutine(Attacking(targetGridObject));
    }

    private IEnumerator Attacking(GridObject targetGridObject)
    {
        RotateCharacter(targetGridObject.transform.position);
        characterAnimator.Attack();

        yield return new WaitForSeconds(2.0f);

        Character target = targetGridObject.GetComponent<Character>();

        int damage = character.GetDamage();
        damage -= target.GetDefense(character.damageType);

        if (damage <= 0)
        {
            damage = 1;
        }

        Debug.Log("target takes damage " + damage.ToString());

        //target.TakeDamage(damage);
    }
}