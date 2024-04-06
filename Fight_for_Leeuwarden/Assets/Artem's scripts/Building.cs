using UnityEngine;

public class Building : MonoBehaviour
{
    public Renderer mainRenderer; 

    
    public void SetTransparent(bool available)
    {
        if (available)
        {
            
            mainRenderer.material.color = Color.green;
        }
        else
        {
            
            mainRenderer.material.color = Color.red;
        }
    }
    
    public void SetNormal()
    {
        mainRenderer.material.color = Color.white;
    }
}






