using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CombatSystem
{
    public class CharacterSwapper : MonoBehaviour
    {
        private Combatant character;
        public List<GameObject> characters = new List<GameObject>();
        public int currIndex = 0;
        public KeyCode swapKey = KeyCode.L;
        public GameObject swapEffect;
        public float swapTime = 1;
        private float swapTimer = 0;
        private bool isSwapping = false;
        private CameraManager cam;

        private Vector3 savedPosition;
        private Quaternion savedRotation;

        // Start is called before the first frame update
        void Start()
        {
            character = transform.GetComponent<Combatant>();
            cam = FindObjectOfType<CameraManager>();
            characters[1].gameObject.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(swapKey) && !isSwapping)
            {
                Debug.Log("Swapping chars");
                StartCoroutine(ChangeAnimation());
            }
        }





        public void ChangeCharacter()
        {
            if (!isSwapping)
            {
                /*GameObject currChar = characters[currIndex];
                SwapOutCharacter(currChar);

                GameObject newChar = characters[index];
                SwapInCharacter(newChar);
                */
                StartCoroutine(ChangeAnimation());
            }
        }

        private void SwapOutCharacter(GameObject character)
        {
            //savedPosition = character.transform.position;
            //savedRotation = character.transform.rotation;
            //character.ResetState();
            character.gameObject.SetActive(false);
        }

        private void SwapInCharacter(GameObject character)
        {
            //character.transform.position = savedPosition;
            //character.transform.rotation = savedRotation;
            character.gameObject.SetActive(true);
            //character.ResetState();
            //cam.ChangeTarget(character);
        }


        private IEnumerator ChangeAnimation()
        {
            swapTimer = 0;
            isSwapping = true;

            SwapOutCharacter(characters[currIndex]);
            GameObject effect = Instantiate(swapEffect);
            effect.transform.position = transform.position;
            effect.transform.SetParent(transform);

            while(swapTimer < swapTime)
            {
                swapTimer += Time.deltaTime;
                yield return null;
            }
            int newIndex = (currIndex + 1) % characters.Count;
            SwapInCharacter(characters[newIndex]);
            effect.GetComponent<ParticleSystem>().Stop();
            currIndex = newIndex;

            yield return new WaitForSeconds(0.2f);
            effect.SetActive(false);
            effect.transform.SetParent(null);
            isSwapping = false;
        }
    }
}