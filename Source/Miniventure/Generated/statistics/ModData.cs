namespace com.mojang.ld22.statistics;

/**
 * Represents ModData for sending statistics using specfiic mod paramaters
 *  //@author dillyg10
 *
 */
public class ModData {
    private readonly long modID;
    private string name;
    private string user;
    
    public ModData(long modID, string name){
        this.modID = modID;
        this.name = name;
        //TODO Figuring out the user, and setting the parameter.
    }

    /**
     * Get the unique Mod ID for this mod. 
     *  //@return
     */
    public virtual long getModID() {
        return modID;
    }

    /**
     * Get the name of this Mod.
     *  //@return the name
     */
    public virtual string getName() {
        return name;
    }

    /**
     * Gets the user currently in the mod
     *  //@return the user
     */
    public virtual string getUser() {
        return user;
    }

}
