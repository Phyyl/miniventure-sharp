namespace com.mojang.ld22.statistics;

/**
 * A class to handle the pushing of statistics
 *  //@author dillyg10
 *
 */
public class StatisticHandler {
    
    public static readonly string url = "http://www.playmincraft.com/stats/";

    public virtual void push(ModData mod, Statistic stat){
        //TODO Implementation of this params method[]
    }
    
    public virtual void pingURL(string data){
        //TODO Figure out if we are going to use a get/post method in order to send statistics
    }
}
