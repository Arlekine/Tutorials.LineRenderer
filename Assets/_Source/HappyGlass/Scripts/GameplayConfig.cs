using UnityEngine;

namespace LineRendererTutorial.HappyGlass
{
    [CreateAssetMenu(menuName = "Configs/Gameplay", fileName = "GameplayConfig")]
    public class GameplayConfig : ScriptableObject
    {
        [SerializeField] private float _winTriggerTime;

        public float WinTriggerTime => _winTriggerTime;
    }
}