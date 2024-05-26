using System;
using UnityEngine;

namespace Code.Interatable.Inventory
{

    public interface IStorable
    {
        public Action PickedUp{ get; set; }
        public void Pickup();

        public void Drop();

        public void Use();
    }
    public class Storable : Interactable, IStorable
    {
        public Action PickedUp { get; set; }
        private void OnEnable()
        {
            Interacted += Pickup;
        }

        // Start is called before the first frame update
        protected override void Start()
        {
            singleUse = false;
            base.Start();
        }

        public virtual void Pickup()
        {
            GameManager.MainPlayer.Pickup(this);
            gameObject.SetActive(false);
            PickedUp.Invoke();
        }

        public void Drop()
        {
            GameManager.MainPlayer.Drop(this);
            gameObject.SetActive(true);
        }

        public void Use()
        {
            GameManager.MainPlayer.Drop(this);
            Destroy(gameObject);
        }

        private void OnDestroy()
        {
            Interacted -= Pickup;
        }
    }
}
