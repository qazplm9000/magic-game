using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkillSystem{
	public class ParticleObject : MonoBehaviour
	{
        [SerializeField]
		private float lifetime = 2f;
        [SerializeField]
        private bool shrinkOverTime = false;
        private Timer timer;

        private void Awake()
        {
            timer = new Timer();
        }

        private void OnEnable()
        {
            timer = new Timer();
            transform.localScale = Vector3.one;
            if(shrinkOverTime) ShrinkObjectOverTime();

        }

        private void OnDisable()
        {
            transform.SetParent(null);
        }

        private void Update()
        {
            timer.Tick();

            if (timer.AtTime(lifetime)) gameObject.SetActive(false);
        }

        private void ShrinkObjectOverTime()
        {
            var shrinkTweener = DOTween.To(() => transform.localScale, x => transform.localScale = x, Vector3.zero, lifetime);
        }
    }
}