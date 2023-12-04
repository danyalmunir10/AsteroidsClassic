using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Asteroids.UI
{ 
    public class GameplayHud : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _score;
        [SerializeField] private TextMeshProUGUI _lives;

        public void SetLives(int lives)
        {
            _lives.SetText(lives.ToString());
        }

        public void SetScore(int score)
        {
            _score.SetText(score.ToString());
        }
    }
}
