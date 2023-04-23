using System.Collections.Generic;
using UnityEngine;

public class GridHighlight : MonoBehaviour
{
    Grid grid;
    List<GameObject> highlightPoints = new List<GameObject>();

    [SerializeField] GameObject highlightPoint;

    private void Awake()
    {
        grid = GetComponentInParent<Grid>();
    }

    private GameObject CreateHighlightPoint()
    {
        GameObject go = Instantiate(highlightPoint);
        highlightPoints.Add(go);
        go.transform.SetParent(gameObject.transform);

        return go;
    }

    private GameObject GetHighlightPoint(int i)
    {
        if (highlightPoints.Count > i)
        {
            return highlightPoints[i];
        }

        GameObject highlightObject = CreateHighlightPoint();

        return highlightObject;
    }

    public void Highlight(int posX, int posY, GameObject highlightObject)
    {
        highlightObject.SetActive(true);
        Vector3 position = grid.GetWorldPosition(posX, posY, true);
        position += Vector3.up * 0.2f;
        highlightObject.transform.position = position;
    }

    public void Highlight(List<Vector2Int> positions)
    {
        for (int i = 0; i < positions.Count; i++)
        {
            Highlight(positions[i].x, positions[i].y, GetHighlightPoint(i));
        }
    }

    public void Highlight(List<PathNode> positions)
    {
        for (int i = 0; i < positions.Count; i++)
        {
            Highlight(positions[i].posX, positions[i].posY, GetHighlightPoint(i));
        }
    }

    public void Hide()
    {
        for (int i = 0; i < highlightPoints.Count; i++)
        {
            highlightPoints[i].SetActive(false);
        }
    }
}
