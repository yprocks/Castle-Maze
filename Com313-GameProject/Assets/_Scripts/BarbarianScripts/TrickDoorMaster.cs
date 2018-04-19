using UnityEngine;
using Random = UnityEngine.Random;

namespace _Scripts.BarbarianScripts
{
	public class TrickDoorMaster : MonoBehaviour
	{
	    public static TrickDoorMaster Instance;

		public GameObject[] TrapDoors;
		public GameObject[] Traps;
		private int _random1, _random2;

		private void Awake()
		{
			if (Instance == null)
				Instance = this;
			else if (Instance != this)
				Destroy(gameObject);
		}

		private void Start ()
		{
			_random1 = Random.Range(0, Traps.Length);
			_random2 = _random1;

			while (_random2 == _random1)
			{
				_random2 = Random.Range(0, Traps.Length);
				if(_random1 != _random2)
					break;
			}

			Traps[_random1].SetActive(false);
			Traps[_random2].SetActive(false);
		}
	
		public void DisableColliders()
		{
			foreach (var trapDoor in TrapDoors)
			{
				trapDoor.GetComponent<BoxCollider>().enabled = false;
			}

			GameManager.Instance.SpawnEnemy = true;
			Invoke("ShowTrapMessage", 5f);
		}

		// ReSharper disable once MemberCanBeMadeStatic.Local
		private void ShowTrapMessage()
		{
			InGameTextManager.GetInstance().ShowPanelForSeconds("Ahahh!!!", "Watch for the traps. Traps are hidden and can hurt you!!!", 15);
		}
	}
}
