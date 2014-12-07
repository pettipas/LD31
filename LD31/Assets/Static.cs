using UnityEngine;
using System.Collections;

public partial class Static : MonoBehaviour {
	
	static Static instance;
	static Static Instance {
		get {
			if (!instance) {
				instance = FindObjectOfType(typeof(Static)) as Static;
			}
			return instance;
		}
	}
	
	void Awake() {
		if (instance && instance != this) {
			Destroy(this);
		}
		else {
			instance = this;
		}
	}
	
	public static bool Active {
		get { return Instance; }
	}
}
