using UnityEngine;

public interface BaseSpell{

	string name { get; set; }
	int rank { get; set; }
	int distance { get; set; }
	GameObject particleEffect { get; set; }

	bool LineOfSight { get; set; }
	Mobility mobile { get; set; }

	float cooldown { get; set; }
	float cooldownTimer { get; }
	bool coolingDown { get; }

	float castTime { get; set; }
	float castTimer { get; }
	bool casting { get; }

	void Cast();

	void Update();



}