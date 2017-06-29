using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoltSpell : BaseSpell {

	public void Cast(){
		throw new System.NotImplementedException();
	}

	public void Update(){
		throw new System.NotImplementedException();
	}


	//getters and setters
	public string name{
		get{
			return name;
		}
		set{
			name = value;
		}
	}

	public int rank{
		get{
			return rank;
		}
		set{
			rank = value;
		}
	}

	public int distance{
		get{
			return distance;
		}
		set{
			distance = value;
		}
	}

	public GameObject particleEffect{
		get{
			return particleEffect;
		}
		set{
			particleEffect = value;
		}
	}

	public bool LineOfSight{
		get{
			return LineOfSight;
		}
		set{
			LineOfSight = value;
		}
	}

	public Mobility mobile{
		get{
			return mobile;
		}
		set{
			mobile = value;
		}
	}

	public float cooldown{
		get{
			return cooldown;
		}
		set{
			cooldown = value;
		}
	}

	public float cooldownTimer{
		get{
			return cooldownTimer;
		}
	}

	public bool coolingDown{
		get{
			return coolingDown;
		}
	}

	public float castTime{
		get{
			return castTime;
		}
		set{
			castTime = value;
		}
	}

	public float castTimer{
		get{
			return castTimer;
		}
	}

	public bool casting{
		get{
			return casting;
		}
	}

}
