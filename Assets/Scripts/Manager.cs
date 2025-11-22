using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using FMODUnity;

public class Manager : MonoBehaviour
{
    public GameObject[] rooms;
    public TMP_Text textBox;
    public Button continueButton;
    public Button listenButton;
    public InventoryManager inventoryManager;
    public CombatManager combatManager;
    public CinematicController cinematicManager;

    [Header("Enemies")]
    public Enemy[] enemies;

    GameObject room;
    [SerializeField] public int roomIndex = 0;
    bool listenButtonClicked = false;

    private void Awake()
    {
        room = rooms[roomIndex];
    }
    void Start()
    {
        room.SetActive(true);
        combatManager.DisableButtons();
        textBox.text = "You pick up the key.";
        RuntimeManager.PlayOneShot(FMODEvents.instance.itemSFX);
        continueButton.gameObject.SetActive(true);


        if (AudioManager.instance != null)
        {
            AudioManager.instance.SetMusicParameter("Area", 1.0f);
        }

    }

    void Update()
    {
        
    }

    public void OnContinueClicked()
    {
        RuntimeManager.PlayOneShot(FMODEvents.instance.buttonSFX);
        continueButton.gameObject.SetActive(false);
        ChangeRoom(roomIndex +1);
    }

    void ChangeRoom(int nextIndex)
    {
        if (nextIndex < 0 || nextIndex >= rooms.Length)
        {
            Debug.LogWarning($"Room index {nextIndex} out of range.");
            return;
        }

        rooms[roomIndex].SetActive(false);
        roomIndex = nextIndex;
        rooms[roomIndex].SetActive(true);

        switch (roomIndex)
        {
            case 0:
                textBox.text = "";
                continueButton.gameObject.SetActive(true);
                listenButton.gameObject.SetActive(false);
                combatManager.DisableButtons();
                break;

            case 1:
                combatManager.DisableButtons();
                StartCoroutine(Room1Dialogue());
                break;

            case 2:
                AudioManager.instance.PlayCaveAmbience();
                combatManager.StartBattle(enemies[roomIndex]);              
                break;

            case 3:
                textBox.text = "Gained Pliers.";
                inventoryManager.AddItem("pliers");
                RuntimeManager.PlayOneShot(FMODEvents.instance.itemSFX);
                combatManager.playerAcc += 5;
                continueButton.gameObject.SetActive(true);
                break;

            case 4:
                combatManager.StartBattle(enemies[roomIndex]);
                break;

            case 5:
                combatManager.StartBattle(enemies[roomIndex]);
                break;

            case 6:
                combatManager.StartBattle(enemies[roomIndex]);
                break;

            case 7:
                RuntimeManager.PlayOneShot(FMODEvents.instance.healSFX);
                textBox.text = "Gained Feast Bell.";
                inventoryManager.AddItem("feast bell");
                RuntimeManager.PlayOneShot(FMODEvents.instance.itemSFX);
                combatManager.playerHealth += 7;
                combatManager.maxHealth += 7;
                if (combatManager.playerHealth > combatManager.maxHealth)
                    combatManager.playerHealth = combatManager.maxHealth;
                continueButton.gameObject.SetActive(true);
                break;

            case 8:
                combatManager.StartBattle(enemies[roomIndex]);
                break;
            case 9:
                RuntimeManager.PlayOneShot(FMODEvents.instance.healSFX);
                textBox.text = "Fully heal and gain +1 Wheyflask.";
                RuntimeManager.PlayOneShot(FMODEvents.instance.itemSFX);
                combatManager.playerHealth = combatManager.maxHealth;
                combatManager.maxWheyflaskQuantity++;
                combatManager.wheyflaskQuantity = combatManager.maxWheyflaskQuantity;
                combatManager.wheyflaskText.text = "(" + combatManager.wheyflaskQuantity + "/" + combatManager.maxWheyflaskQuantity + ")";
                continueButton.gameObject.SetActive(true);
                break;

            case 10:
                //Gain Wheel of Truth
                RuntimeManager.PlayOneShot(FMODEvents.instance.itemSFX);
                textBox.text = "Gained Wheel of Truth.";
                inventoryManager.AddItem("wheel");
                combatManager.playerAcc += 5;
                continueButton.gameObject.SetActive(true);
                break;

            case 11:
                combatManager.StartBattle(enemies[roomIndex]);
                break;

            case 12:
                //Watcher's Vigil
                //Gain Firefly Lantern
                combatManager.DisableButtons();
                StartCoroutine(Room12Dialogue());
                break;

            case 13:
                combatManager.StartBattle(enemies[roomIndex]);
                break;

            case 14:
                textBox.text = "You find a Wheyflask, long forgotten by its carrier.";
                combatManager.wheyflaskQuantity++;
                RuntimeManager.PlayOneShot(FMODEvents.instance.itemSFX);
                if (combatManager.wheyflaskQuantity > combatManager.maxWheyflaskQuantity)
                {
                    RuntimeManager.PlayOneShot(FMODEvents.instance.healSFX);
                    combatManager.wheyflaskQuantity = combatManager.maxWheyflaskQuantity;
                    combatManager.playerHealth += 10;
                    if (combatManager.playerHealth > combatManager.maxHealth)
                        combatManager.playerHealth = combatManager.maxHealth;
                }
                combatManager.wheyflaskText.text = "(" + combatManager.wheyflaskQuantity + "/" + combatManager.maxWheyflaskQuantity + ")";
                continueButton.gameObject.SetActive(true);
                break;

            case 15:
                combatManager.StartBattle(enemies[roomIndex]);
                break;

            case 16:
                combatManager.StartBattle(enemies[roomIndex]);
                break;

            case 17:
                RuntimeManager.PlayOneShot(FMODEvents.instance.healSFX);
                textBox.text = "Fully heal and gain +1 Wheyflask.";
                RuntimeManager.PlayOneShot(FMODEvents.instance.itemSFX);
                combatManager.playerHealth = combatManager.maxHealth;
                combatManager.maxWheyflaskQuantity++;
                combatManager.wheyflaskQuantity = combatManager.maxWheyflaskQuantity;
                combatManager.wheyflaskText.text = "(" + combatManager.wheyflaskQuantity + "/" + combatManager.maxWheyflaskQuantity + ")";
                continueButton.gameObject.SetActive(true);
                break;

            case 18:
                //fight
                combatManager.StartBattle(enemies[roomIndex]);
                break;
            case 19:
                //broken curdguard
                combatManager.DisableButtons();
                StartCoroutine(Room19Dialogue());
                break;
            case 20:
                //fight
                combatManager.StartBattle(enemies[roomIndex]);
                break;
            case 21:
                //gain tongue
                RuntimeManager.PlayOneShot(FMODEvents.instance.healSFX);
                textBox.text = "Gained The Silent Board.";
                inventoryManager.AddItem("tongue");
                RuntimeManager.PlayOneShot(FMODEvents.instance.itemSFX);
                combatManager.playerAcc += 2;
                combatManager.playerMinDamage += 1;
                combatManager.playerMaxDamage += 2;
                combatManager.maxHealth += 3;
                combatManager.playerHealth += 3;
                if (combatManager.playerHealth > combatManager.maxHealth)
                    combatManager.playerHealth = combatManager.maxHealth;

                continueButton.gameObject.SetActive(true);
                break;
            case 22:
                //fight
                combatManager.StartBattle(enemies[roomIndex]);
                break;
            case 23:
                //heal
                RuntimeManager.PlayOneShot(FMODEvents.instance.healSFX);
                RuntimeManager.PlayOneShot(FMODEvents.instance.itemSFX);
                textBox.text = "Fully heal and gain +1 Wheyflask.";
                combatManager.playerHealth = combatManager.maxHealth;
                combatManager.maxWheyflaskQuantity++;
                combatManager.wheyflaskQuantity = combatManager.maxWheyflaskQuantity;
                combatManager.wheyflaskText.text = "(" + combatManager.wheyflaskQuantity + "/" + combatManager.maxWheyflaskQuantity + ")";
                continueButton.gameObject.SetActive(true);
                break;
            case 24:
                combatManager.StartBattle(enemies[roomIndex]);
                //if (enemies[roomIndex].totalHealth <= 0)
                    //cinematicManager.GoToEndScene();
                break;

            default:
                textBox.text = "";
                continueButton.gameObject.SetActive(true);
                listenButton.gameObject.SetActive(false);
                break;
        }
    }

    private IEnumerator Room1Dialogue()
    {
        continueButton.gameObject.SetActive(false);

        yield return SayAndWait("“...Oh, you... You’re not molded, eh? Thank goodness... I’m done for, I fear. The rot’s deep. I’ll be gone soon, then hollow as the rind itself.”");
        yield return SayAndWait("“I wish to ask something of you... You and I, we’re both knights of the Curd. Hear me out, won’t you?”");
        yield return SayAndWait("“...Regrettably, I have failed in my duty. I was sent to the depths to cleanse the King, to cut away the first bloom of mold before it spread. But the rot took hold... and I could not reach him.”");
        yield return SayAndWait("“Yet perhaps you can. There is an old saying among our order. ‘When the Wheel turns and the rind cracks, the one unspoiled must descend, and from the Heart of the Cheese, restore the flavor of the King.’”");
        yield return SayAndWait("“Please go, now, to the deepest halls of the Citadel. The King still sits upon his curdled throne, wrapped in the Mold’s embrace. End his suffering... or save what little of him remains.”");
        yield return SayAndWait("“Ah, but before you go, take this.”\r\n (He hands you the The Wheyflask, trembling.)\r\n “An old relic of the Curdguard. It has served me well... and will keep you strong.”");
        inventoryManager.AddItem("wheyflask");
        RuntimeManager.PlayOneShot(FMODEvents.instance.itemSFX);
        combatManager.wheyflaskButton.gameObject.SetActive(true);
        combatManager.wheyflaskButton.interactable = true;
        combatManager.wheyflaskText.text = "(" + combatManager.wheyflaskQuantity + "/" + combatManager.maxWheyflaskQuantity + ")";
        yield return SayAndWait("“Now... I must bid you farewell. I would hate to turn on you, once the Mold takes my mind. So, go now... and thank you... for listening.”");
        yield return SayAndWait("He exhales softly.");

        //Interactables
        yield return SayAndWait("You step to the small shrine. Its edges are veined with mold and carvings soft with age. Offerings of melted candles and dried curds surround it.\r\nAt the shrine’s base rest two relics."); 
        yield return SayAndWait("You read the decree.\n“By decree of His Majesty, the First Bloom is holy. None shall scrape it away, for it is the mark of divine ripeness.”");
        //narrator speaks “He crowned decay in gold, and we knelt to kiss it.”
        yield return SayAndWait("You inspect the chalice.\nA silver cup half-filled with hardened green whey. Crystals form along its rim like jewels.");
        //narrator speaks “They called it the King’s Blessing, and drank in his name.”

        listenButton.gameObject.SetActive(false);
        continueButton.gameObject.SetActive(true);
    }

    private IEnumerator Room12Dialogue()
    {
        continueButton.gameObject.SetActive(false);

        yield return SayAndWait("A deceased Curdguard kneels.");

        //Interactables
        yield return SayAndWait("At its feet:\r\nA lantern filled with fireflies.\r\nA letter, unfinished, written in a trembling hand.");
        yield return SayAndWait("You inspect the lantern.");
        yield return SayAndWait("Gained Firefly Lantern.");
        inventoryManager.AddItem("lantern");
        RuntimeManager.PlayOneShot(FMODEvents.instance.itemSFX);
        combatManager.playerMinDamage += 2;
        combatManager.playerMaxDamage += 3;
        //narrator speaks “In time, all learned to see in the dark. Faith or fear made no difference”
        yield return SayAndWait("You read the letter.\n“His Majesty no longer sleeps. He whispers to the walls. The mold moves when he breathes. I tell myself this is grace.”");
        //narrator speaks "We called it faith, though our hearts already knew its taste was wrong."

        listenButton.gameObject.SetActive(false);
        continueButton.gameObject.SetActive(true);
    }

    private IEnumerator Room19Dialogue()
    {
        continueButton.gameObject.SetActive(false);

        yield return SayAndWait("There lies a fallen Curdguard.");

        //Interactables
        yield return SayAndWait("Beside the corpse:\r\nA sword. The edge is dull, eaten by rust and spore.\r\nA journal, open, its pages fused together by dried whey.");
        yield return SayAndWait("You inspect the sword.\nIt's heavier than it looks, not with weight, but memory.");
        //narrator speaks “Every knight swore to serve the King. None asked what they served.”
        yield return SayAndWait("You read the journal.\n“He said the mold would make us eternal… We believed him. Gods forgive us, we believed him.”");
        //narrator speaks "Each death we named deliverance, so we could bear the weight."
        yield return SayAndWait("Gained Glowing Mushroom.");
        RuntimeManager.PlayOneShot(FMODEvents.instance.healSFX);
        inventoryManager.AddItem("shroom");
        RuntimeManager.PlayOneShot(FMODEvents.instance.itemSFX);
        combatManager.playerMinDamage += 1;
        combatManager.playerMaxDamage += 3;
        combatManager.maxHealth += 5;
        combatManager.playerHealth += 5;
        if (combatManager.playerHealth > combatManager.maxHealth)
            combatManager.playerHealth = combatManager.maxHealth;

        listenButton.gameObject.SetActive(false);
        continueButton.gameObject.SetActive(true);
    }
    public void OnListenButtonClicked()
    {
        RuntimeManager.PlayOneShot(FMODEvents.instance.buttonSFX);
        
        listenButtonClicked = true;
    }

    private IEnumerator SayAndWait(string line)
    {
        listenButtonClicked = false;              
        textBox.text = line;
        listenButton.gameObject.SetActive(true);
        yield return new WaitUntil(() => listenButtonClicked);
        //brief debounce so double-clicks don’t skip a line
        yield return null;
        listenButton.gameObject.SetActive(false);
    }

}
