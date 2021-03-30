using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackGenerator : MonoBehaviour
{
    public GameObject[] trackPrefab;
    public List<GameObject> activeTracks = new List<GameObject>();

    private float spawnPos = 0;
    private float tileLength = 100;

    [SerializeField] private Transform player;
    private int startTracks = 5;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < startTracks; i++)
        {
            SpawnTrack(Random.Range(0, trackPrefab.Length));
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player.position.z - 60 > spawnPos - (startTracks * tileLength))
        {
            SpawnTrack(Random.Range(0, trackPrefab.Length));
            DeleteTrack();
        }
    }

    private void SpawnTrack(int trackIndex)
    {
        GameObject nextTrack = Instantiate(trackPrefab[trackIndex], transform.forward * spawnPos, transform.rotation);
        activeTracks.Add(nextTrack);
        spawnPos += tileLength;
    }

    private void DeleteTrack()
    {
        Destroy(activeTracks[0]);
        activeTracks.RemoveAt(0);
    }

}
