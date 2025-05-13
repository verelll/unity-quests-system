using System;
using System.Collections;
using UnityEngine;

namespace verelll.Architecture
{
	public class UnityEventsProvider : SingletonMonoBehaviour<UnityEventsProvider>
	{
		public static float DeltaTime => Time.deltaTime;
		
		public static event Action OnUpdate
		{
			add => Instance._onUpdate += value;
			remove => Instance._onUpdate -= value;
		}

		public static event Action OnSecondTick
		{
			add => Instance._onSecondTick += value;
			remove => Instance._onSecondTick -= value;
		}

		public static event Action OnFixedUpdate
		{
			add => Instance._onFixedUpdate += value;
			remove => Instance._onFixedUpdate -= value;
		}

		public static event Action OnLateUpdate
		{
			add => Instance._onLateUpdate += value;
			remove => Instance._onLateUpdate -= value;
		}

		public static event Action OnNextUpdate
		{
			add => Instance._onNextUpdate += value;
			remove => Instance._onNextUpdate -= value;
		}

		public static event Action<bool> OnApplicationFocusChanged
		{
			add => Instance._onApplicationFocusChanged += value;
			remove => Instance._onApplicationFocusChanged -= value;
		}

		public static void CoroutineStart(IEnumerator coroutine)
		{
			Instance.StartCoroutine(coroutine);
		}

		public static void CoroutineStop(IEnumerator coroutine)
		{
			Instance.StopCoroutine(coroutine);
		}

		public static void CoroutineStopAll()
		{
			Instance.StopAllCoroutines();
		}

		private event Action _onUpdate;

		private event Action _onSecondTick;

		private event Action _onFixedUpdate;

		private event Action _onLateUpdate;

		private event Action _onNextUpdate;

		private event Action<bool> _onApplicationFocusChanged;

		private float _nextSecond;

		private void Update()
		{
			_onUpdate?.Invoke();

			var next = _onNextUpdate;
			_onNextUpdate = null;
			next?.Invoke();

			if (_nextSecond < Time.time)
			{
				_nextSecond += 1;
				_onSecondTick?.Invoke();
			}
		}

		private void FixedUpdate()
		{
			_onFixedUpdate?.Invoke();
		}

		private void LateUpdate()
		{
			_onLateUpdate?.Invoke();
		}

		private void OnApplicationFocus(bool hasFocus)
		{
			_onApplicationFocusChanged?.Invoke(hasFocus);
		}
	}
}
