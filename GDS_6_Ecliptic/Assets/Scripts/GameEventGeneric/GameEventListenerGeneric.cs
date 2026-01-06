using UnityEngine;
using UnityEngine.Events;

public class GameEventListenerGeneric<T> : MonoBehaviour
{
    public GameEventGeneric<T> gameEvent;
    public UnityEvent<T> onEventTriggered;

	private void OnEnable()
	{
		gameEvent.AddListener(this);
	}

	private void OnDisable()
	{
		gameEvent.RemoveListener(this);
	}

	public void OnEventTriggered(T payload)
	{
		onEventTriggered.Invoke(payload);
	}
}
