using UnityEngine;

public class SlimeBehavior : MonoBehaviour 
{
    public Slime slimeData;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        var renderer = GetComponent<SpriteRenderer>();
        if (renderer && slimeData.EnemyIcon)
        {
            renderer.sprite = slimeData.EnemyIcon;
        }
        Debug.Log($"{slimeData.EnemyName}");
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnDestroy()
    {
        GameManager.Instance?.UnregisterEnemy();
    }
}
