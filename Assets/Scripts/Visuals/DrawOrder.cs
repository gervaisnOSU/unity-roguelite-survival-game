using UnityEngine;

public class DrawOrder : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private int offset = 0;

    private void Awake()
    {

        // Get renderer
        if(spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }  
    }

    // Update is called once per frame
    void LateUpdate()
    {
        // Updrate draw order once per frame
        spriteRenderer.sortingOrder = Mathf.RoundToInt(-transform.position.y * 100);
        
    }
}
