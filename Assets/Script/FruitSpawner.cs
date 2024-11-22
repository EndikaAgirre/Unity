using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FruitSpawner : MonoBehaviour
{
    [Serializable]
    public class RitmoMusica
    {
        public float time;
        public int position;
        public bool right;
    }
    [Serializable]
    public class Musica
    {
        public string name;
        public AudioClip song;
        public List<RitmoMusica> ritmo;
    }
    [SerializeField] List<float> powers;
    [SerializeField] List<Musica> musicas;
    [SerializeField] List<GameObject> fruits;
    [SerializeField] List<Transform> fruitSpawnPositions;
    [SerializeField] Difficulty difficulty;
    [SerializeField] TextMeshProUGUI timer;
    Dictionary<Difficulty, float> difficultySpeed = new Dictionary<Difficulty, float>
    {
        { Difficulty.Easy, 1f },
        { Difficulty.Normal, 1.5f },
        { Difficulty.Hard, 2f }
    };

    int n_Ritmo = 0;
    float time = 0;
    float delayTime = 0;
    AudioSource audioSource;
    bool isSongPlaying = false;
    float delay = 4.38f;

    public enum Difficulty
    {
        Easy,
        Normal,
        Hard
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        PlaySong(musicas[0]);
    }
    private void Update()
    {
        if (isSongPlaying)
        {
            time += Time.deltaTime;
            timer.text = delayTime.ToString("F2");

            if (n_Ritmo < musicas[0].ritmo.Count && time >= musicas[0].ritmo[n_Ritmo].time)
            {
                int position = GetPosition(musicas[0].ritmo[n_Ritmo].position);
                float power = GetPower(musicas[0].ritmo[n_Ritmo].position);
                SpawnFruit(fruitSpawnPositions[position], fruits[UnityEngine.Random.Range(0,fruits.Count)], power, difficultySpeed[difficulty], musicas[0].ritmo[n_Ritmo].right);
                n_Ritmo++;
            }
        }
    }
    private float GetPower(int _number)
    {
        switch (_number)
        {
            case 1:
            case 2:
            case 8:
            case 7:
                return powers[0];
                break;
            case 3:
            case 4:
            case 10:
            case 9:
                return powers[1];
                break;
            case 5:
            case 6:
            case 12:
            case 11:
                return powers[2];
                break;
            default:
                return 0;
        }
    }
    private int GetPosition(int _number)
    {
        switch(_number)
        {
            case 1:
            case 3:
            case 5:
                return 0;
                break;
            case 2:
            case 4:
            case 6:
                return 1;
                break;
            case 8:
            case 10:
            case 12:
                return 2;
                break;
            case 7:
            case 9:
            case 11:
                return 3;
                break;
            default:
                return 0;
        }
    }
    private void PlaySong(Musica musica)
    {
        audioSource.clip = musica.song;
        audioSource.Play();
        time = 0;
        isSongPlaying = true;
    }
    private void SpawnFruit(Transform transform, GameObject fruit, float power, float speed, bool right)
    {
        GameObject go = Instantiate(fruit, transform.position, Quaternion.identity);
        Rigidbody rb = go.GetComponent<Rigidbody>();
        if (rb != null)
            rb.AddForce(transform.forward * power);
        go.GetComponent<Fruit>().Right = right;
        go.GetComponent<Fruit>().SetSpeed(speed);
    }
}