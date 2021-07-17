using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class RankingManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI rankText;

    [SerializeField]
    private Transform finishLine;

    private List<Character> characters;
    private Player player;

    private void Start()
    {
        characters = FindObjectsOfType<Character>().ToList();
        player = FindObjectOfType<Player>();
    }

    private void Update()
    {
        if (Extensions.RateLimiter(10))
        {
            rankText.text = $"{GetPlayersRank()} / {characters.Count}";
        }
    }

    private int GetPlayersRank()
    {
        characters = characters.OrderBy(c => finishLine.position.z - c.transform.position.z).ToList();
        return characters.IndexOf(player) + 1;
    }
}