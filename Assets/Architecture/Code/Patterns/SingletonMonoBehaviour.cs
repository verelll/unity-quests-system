﻿using UnityEngine;

namespace verelll.Architecture
{
	public class SingletonMonoBehaviour<T> : MonoBehaviour where T : SingletonMonoBehaviour<T>
	{
		private static T _instance;

		public static T Instance
		{
			get
			{
				if (_instance == null)
				{
					var o = new GameObject(typeof(T).Name);
					_instance = o.AddComponent<T>();
					DontDestroyOnLoad(o);
					_instance.Initialize();
				}

				return _instance;
			}
		}
        
		protected virtual void Initialize() { }
	}
}