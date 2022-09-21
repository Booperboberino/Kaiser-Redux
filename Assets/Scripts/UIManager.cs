using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    public RectTransform selectionBox;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateSelectionBox(Vector2[] vectors)
    {

        bool isValidVectorArray = vectors != null && vectors.Length == 2;
        selectionBox.gameObject.SetActive(isValidVectorArray);

        if (isValidVectorArray)
        {
            selectionBox.sizeDelta = vectors[0];
            selectionBox.anchoredPosition = vectors[1];
        }
    }
    public void UpdateSelectionBox(Vector3 startPosition, Vector3 endPosition)
    {
        Vector2 startPositionScreenSpace = Camera.main.WorldToScreenPoint(startPosition);
        Vector2 endPositionScreenSpace = Camera.main.WorldToScreenPoint(endPosition);


        Vector3 centerPos = new Vector3((startPositionScreenSpace.x + endPositionScreenSpace.x), startPositionScreenSpace.y + endPositionScreenSpace.y ) / 2f;
        float width = endPositionScreenSpace.x - startPositionScreenSpace.x;
        float height = endPositionScreenSpace.y - startPositionScreenSpace.y;

        if (!selectionBox.gameObject.active)
        {
        selectionBox.gameObject.SetActive(true);
        }
        selectionBox.GetComponent<RectTransform>().anchoredPosition = startPositionScreenSpace + new Vector2(width / 2, height / 2) ;
        selectionBox.GetComponent<RectTransform>().sizeDelta = new Vector2(Mathf.Abs(width), Mathf.Abs(height));
    }
    public void clearSelectionBox()
    {
        selectionBox.gameObject.SetActive(false);
    }
}
