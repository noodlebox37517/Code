using System;
using System.Collections;
using System.Collections.Generic;
using Code.Interatable.Inventory;
using UnityEngine;

namespace Core.player
{
        public class Player : MonoBehaviour
        {
            public readonly float Gravity = -9.8f;
            public readonly float LadderGravity = 3;
        // Start is called before the first frame update

        public bool _isOnLadder = false;
        private List<Iinteractable> _interactablesInRange = new();
        private List<IStorable> _storedItems = new();
        private Iinteractable targetInteractable;
        private void Awake()
        {
            if( GameManager.MainPlayer == null)
                GameManager.MainPlayer = this;
        }

        void Start()
        {
           
            _isOnLadder = false;
        }
        

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown("e"))
            {
                AttemptInteract();
            }
            if (Input.GetKeyDown("j"))
            {
                UIManager.instance?.ShowJournal();
            }

            if (Input.GetKeyDown("h"))
            {
                UIManager.instance?.EscapeMenu();
            }
        }

        public void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Ladder"))
            {
                _isOnLadder = true;
            }
            if (other.CompareTag("Player"))
            {
                return;
            }

            var interactable = other.GetComponent<Iinteractable>();
            if (interactable != null && interactable.isInteractble)
            {
                _interactablesInRange.Add(interactable);
                if (targetInteractable == null)
                {
                   InteractionPompt.instance.ActivePrompt(other.gameObject);
                   targetInteractable = interactable;
                }
                
                Debug.Log("found Interactable");
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Ladder"))
            {
                _isOnLadder = false;
            }
            if (other.CompareTag("Player"))
            {
                return;
            }
            var interactable = other.GetComponent<Iinteractable>();
            if (interactable == null)
                return;
            interactable.TriggerLeft();
            RemoveInteractable(interactable);


        }

        private void RemoveInteractable(Iinteractable interactable)
        {
            _interactablesInRange.Remove(interactable);
            if (targetInteractable == interactable)
            {
                targetInteractable = null;
                if (_interactablesInRange.Count == 0)
                {
                    InteractionPompt.instance.DeActivePrompt();
                    return;
                }
                    //TODO upgrade to check which is the best to use at any given time. assume the last one entered is the most relevant
                targetInteractable = _interactablesInRange[^1];
                InteractionPompt.instance.ActivePrompt(targetInteractable.go);
            }
        }
        
        public void InteractableDisabled(Iinteractable interactable)
        {
            if (_interactablesInRange.Contains(interactable))
            {
                RemoveInteractable(interactable);
            }
        }
        
        public void AttemptInteract()
        {
            if (targetInteractable == null)
                return;
            targetInteractable.Interact();
        }

        public bool Pickup(IStorable item)
        {
            if (_storedItems.Contains(item) == false)
            {
                _storedItems.Add(item);
                return true;
            }

            return false;
        }

        public bool Drop(IStorable item)
        {
            if (_storedItems.Contains(item) == true)
            {
                _storedItems.Remove(item);
                return true;
            }

            return false;
        }
    }

}
