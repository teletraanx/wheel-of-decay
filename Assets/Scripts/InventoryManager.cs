using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using static UnityEngine.Rendering.DebugUI.Table;

public class InventoryManager : MonoBehaviour
{
    public Image[] inventorySlots;
    public ItemTooltip[] tooltips;
    public Sprite[] sprites; //0 = wheyflask
    int currentSlot = 0;
    List<string> inventoryItems = new List<string>();
    public int questItemCount = 0;

    public void AddItem(string item)
    {
        switch (item)
        {
            case "wheyflask": //sprite 0
                questItemCount++;
                inventorySlots[currentSlot].gameObject.SetActive(true);
                //change current slot sprite
                inventorySlots[currentSlot].sprite = sprites[0];
                tooltips[currentSlot].description = "The Wheyflask\nA glass dripper relic once carried by the knights of the Curdguard.\n\nSaid to restore the body by feeding the mold that dwells within, coaxing it to heal rather than devour.\n\n“The Curdguard once called it mercy. Now, it tastes of salt and sorrow.”";
                
                currentSlot++;
                break;
            case "pliers": //sprite 1
                inventorySlots[currentSlot].gameObject.SetActive(true);
                inventorySlots[currentSlot].sprite = sprites[1];
                tooltips[currentSlot].description = "Pliers\r\n\nA pair of rusted pliers.\r\n\nThe doubtful were held down, jaws forced open, their teeth removed in grim ceremony.\r\n\n“They took from them the power to chew… and with it, their dissent.”\r\n\n+5 Accuracy";
                currentSlot++;
                break;
            case "feast bell": //sprite 2
                inventorySlots[currentSlot].gameObject.SetActive(true);
                inventorySlots[currentSlot].sprite = sprites[2];
                tooltips[currentSlot].description = "The Feast Bell\n\nA cracked silver bell from the King’s mold-feasts.\r\nNone could ignore its summons. Even the wary came.\r\n\nThey watched in dread as the devoted consumed the blooming rot with frantic worship.\r\n\r\n“All came when it rang. Only some kept their humanity.”\r\n\n+7 Health";
                currentSlot++;
                break;
            case "wheel": //slot 3
                inventorySlots[currentSlot].gameObject.SetActive(true);
                inventorySlots[currentSlot].sprite = sprites[3];
                tooltips[currentSlot].description = "The Wheel of Truth\n\nA tiny wheel scored with eyes that never close.\r\nThey belonged to the first who recognized the King’s rot, and they paid dearly for it.\r\n\r\n“They saw the truth too soon. They will see its end as well.”\r\n\n+5 Accuracy";
                currentSlot++;
                break;
            case "lantern": 
                inventorySlots[currentSlot].gameObject.SetActive(true);
                inventorySlots[currentSlot].sprite = sprites[4];
                tooltips[currentSlot].description = "The Firefly Lantern\n\nA lantern of drifting fireflies.\r\nWhen the halls fell into night, everyone, devoted or doubtful, had no choice but to learn to see in the dark.\r\n\nThis lantern kept hope alive, if only barely.\r\n\r\n“The faithful prayed. The doubters watched. All needed light.”\r\n\n+2-3 Damage";
                currentSlot++;
                break;
            case "shroom":
                inventorySlots[currentSlot].gameObject.SetActive(true);
                inventorySlots[currentSlot].sprite = sprites[5];
                tooltips[currentSlot].description = "A Glowing Mushroom\n\nA faintly glowing mushroom plucked from the Citadel’s deeper veins.\r\n\nIts light is soft, warm… and wrong.\r\n\r\n“In the end, all lanterns went out... except those that grew roots.”\r\n\n+1-3 Damage\n+5 Health";
                currentSlot++;
                break;
            case "tongue":
                inventorySlots[currentSlot].gameObject.SetActive(true);
                inventorySlots[currentSlot].sprite = sprites[6];
                tooltips[currentSlot].description = "The Silent Board\n\nA shriveled tongue nailed to a wooden board, taken from one who questioned the King’s ripening.\r\n\r\n“In their doubt, they found only silence, for the King carved the truth from their very mouths.”\r\n\n+1-2 Damage, +3 Health, +2 Accuracy";
                currentSlot++;
                break;

        }

        inventoryItems.Add(item);
        //Debug.Log("In inventory: " + inventoryItems[0]);
        return;
    }
}
