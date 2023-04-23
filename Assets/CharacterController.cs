using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class CharacterController : MonoBehaviour
{
    GridObject gridObject;
    Character character;
    CharacterAnimator characterAnimator;

    List<Vector3> pathWorldPositions;

    IEnumerator enumerator;

    float moveSpeed = 1.0f;

    public bool IS_MOVING
    {
        get
        {
            if (pathWorldPositions == null) 
            { 
                return false;
            }

            return pathWorldPositions.Count > 0;
        }
    }

    private void Awake()
    {
        gridObject = GetComponent<GridObject>();
        character = GetComponent<Character>();
        characterAnimator = GetComponent<CharacterAnimator>();
    }

    private void Update()
    {
        Moving();
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

    public void SkipAnimation()
    {
        if (pathWorldPositions.Count < 2) 
        { 
            return;
        }

        transform.position = pathWorldPositions[pathWorldPositions.Count - 1];
        Vector3 originPosition = pathWorldPositions[pathWorldPositions.Count - 2];
        Vector3 destinationPosition = pathWorldPositions[pathWorldPositions.Count - 1];
        RotateCharacter(originPosition, destinationPosition);
        pathWorldPositions.Clear();
        characterAnimator.StopMoving();
    }

    public void Move(List<PathNode> path)
    {
        if (IS_MOVING)
        {
            SkipAnimation();
        }

        pathWorldPositions = gridObject.targetGrid.ConvertPathNodesToWorldPositions(path);

        gridObject.targetGrid.RemoveObject(gridObject.positionOnGrid, gridObject);

        gridObject.positionOnGrid.x = path[path.Count - 1].posX;
        gridObject.positionOnGrid.y = path[path.Count - 1].posY;

        gridObject.targetGrid.PlaceObject(gridObject.positionOnGrid, gridObject);

        RotateCharacter(transform.position, pathWorldPositions[0]);

        characterAnimator.StartMoving();
    }

    private void Moving()
    {
        if (pathWorldPositions == null || pathWorldPositions.Count == 0)
        {
            return;
        }

        transform.position = Vector3.MoveTowards(transform.position, pathWorldPositions[0], moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, pathWorldPositions[0]) < 0.05f)
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
    }

    public void Attack(GridObject targetGridObject)
    {
        enumerator = Attacking(targetGridObject);

        StartCoroutine(enumerator);
    }

    IEnumerator Attacking(GridObject targetGridObject)
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