using System.Collections.Generic;

namespace NGame
{
    //Used when game is initialized 
    public delegate void DEL_GAME_STATUS(bool winOrLose);
    public delegate void DEL_ITEM_INITIATE(List<ItemInfo> items);
    //Used when an item is acquired, dropped from suitcase
    public delegate void DEL_ITEM(ItemInfo item);

    public enum ITEM_TYPE { LOTION, CLOCK, SENDWITCH }
    public enum ITEM_STATUS { UNREGISTERED, REGISTERED }
    public enum ITEM_PURPOSE { OFFICE,BEACH,SCHOOL}
}
