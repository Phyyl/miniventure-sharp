namespace com.mojang.ld22.statistics;

/**
 * Represents a Statistic object for querying user statistics
 *  //@author dillyg10
 *
 */
public class Statistic {

    /**
     * The identifier for the statistic. 
     */
    private string id;
    
    /**
     * The data contained in the statistic
     */
    private Object data;
    
    /**
     * Instintate a new statistic Object
     *  //@param id The statistic identifier
     *  //@param data The data in the statistic
     */
    public Statistic(string id, Object data){
        this.id = id;
        this.data = data;
    }

    /**
     * Returns the ID for this statistic
     *  //@return the ID
     */
    public virtual string getId() {
        return id;
    }

    /**
     * 
     *  //@return
     */
    public virtual Object getData() {
        return data;
    }

}
