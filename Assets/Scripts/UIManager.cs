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
}
