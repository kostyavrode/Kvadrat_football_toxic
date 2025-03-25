using UnityEngine;

namespace Code
{
    public class Character : MonoBehaviour
    {
        public Transform _cameraTransform;
        public Animator _animator;

        public void Up()
        {
            _animator.SetTrigger("Up");
        }

        public void Down()
        {
            _animator.SetTrigger("Down");
        }
    }
}