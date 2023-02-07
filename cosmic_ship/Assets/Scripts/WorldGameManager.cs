using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Game
{
    public class WorldGameManager : MonoBehaviour
    {
        public GameObject DangerLight;
        public AudioSource dangerSound;
        public List<DoorOpener> doors;
        public List<GameObject> lights;
        public GameObject level1;
        public GameObject level2;
        public int destroyed;
        public List<DamageMaterial> damages;
        public int damageLevel = 0;
        public AudioSource backgroundSound;
        public AudioSource powerOffSound;
        private bool lightsState = false;
        public ChangeHolo hologram;
        public AudioSource siren;
        public Animator LeverArmLight;

        public TMP_Text Count_Asteroids;

        // Start is called before the first frame update
        void Start()
        {
            lightsState = true;
            destroyed = 0;
            Count_Asteroids.text = destroyed.ToString();
            level1.SetActive(false);
            level2.SetActive(false);
            StartCoroutine(Danger());

        }

        IEnumerator Danger()
        {
            yield return new WaitForSeconds(10);
            dangerSound.Play();
            while (true)
            {
                DangerLight.SetActive(true);
                yield return new WaitForSeconds(1);
                DangerLight.SetActive(false);
                yield return new WaitForSeconds(1);
            }
        }

        IEnumerator Doors()
        {
            while (true)
            {
                yield return new WaitForSeconds(Random.Range(30, 60));
                doors[Random.Range(0, doors.Count)].Disable();
            }

        }

        IEnumerator Lights()
        {
            while (true)
            {
                yield return new WaitForSeconds(Random.Range(50, 100));
                foreach (var light in lights)
                {
                    light.SetActive(false);
                }
            }



        }

        public void OnRockCollision()
        {
            Debug.Log("Crash");
            damageLevel++;
            if (damageLevel == 1)
            {
                Level1();
            }
            if (damageLevel == 2)
            {
                Level2();
            }
            if (damageLevel == 3)
            {
                CrossScene.destroyed = destroyed;
                SceneManager.LoadScene("GameOver");
            }

        }


        public void ToogleLights()
        {
            if (lightsState)
            {
                lightsOff();
            }
            else
            {
                lightsOn();
            }
        }

        public void ProjectileDestroyed()
        {
            destroyed++;
            Count_Asteroids.text = destroyed.ToString();

        }

        public void lightsOff()
        {
            LeverArmLight.SetBool("light", !LeverArmLight.GetBool("light"));
            if (!lightsState)
                return;
            lightsState = false;
            backgroundSound.Pause();
            powerOffSound.Play();
            foreach (var light in lights)
            {
                light.SetActive(false);
            }
        }

        public void lightsOn()
        {
            LeverArmLight.SetBool("light", !LeverArmLight.GetBool("light"));
            if (lightsState)
                return;
            lightsState = true;
            backgroundSound.Play();
            foreach (var light in lights)
            {
                light.SetActive(true);
            }
        }

        public void Level1()
        {

            level1.SetActive(true);
            foreach (var obj in damages)
            {
                obj.Level1();
            }
            lightsOff();
            hologram.level1();

        }

        public void Level2()
        {
            siren.Play();
            foreach (var door in doors)
            {
                door.Disable();
            }
            level2.SetActive(true);
            foreach (var obj in damages)
            {
                obj.Level2();
            }
            hologram.level2();
            StartCoroutine(Doors());
            lightsOff();
        }
        private Save CreateSaveGameObject()
        {
            Save save = new Save
            {
                charactermoved = new Vector3(0,0,0),
                MVolume_value = 0,
                EVolume_value = 0,

            };
            return save;
        }
        public void SaveGame()
        {
            // 1
            Save save = CreateSaveGameObject();

            // 2
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(Application.persistentDataPath + "/gamesave.save");
            bf.Serialize(file, save);
            file.Close();
            Debug.Log("Game Saved");
        }

    }
}
    
