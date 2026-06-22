using UnityEngine;
using UnityEngine.Events;

public class CharacterCollect : MonoBehaviour
{
    public UnityEvent <CollectableObject> onColletedObject;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        CollectableObject collectable = collision.GetComponent<CollectableObject>();
        if(collectable != null)
        {
            onColletedObject.Invoke(collectable);
            collectable.NotifyCollected();
        }

    }
}
