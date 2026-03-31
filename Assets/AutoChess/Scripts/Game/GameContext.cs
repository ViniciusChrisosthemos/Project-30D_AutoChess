using UnityEngine;

public class GameContext
{
    public GameState State {  get; private set; }
    public GameServices Services {  get; private set; }
    
    public GameContext(GameState state, GameServices services)
    {
        State = state;
        Services = services;
    }
}
