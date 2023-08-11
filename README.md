# BitMod

An elegant BattleBit: Remastered modding framework designed to enable plugins of all kinds to operate
simultaneously on the same API server.

## Introduction

BitMod features four kinds of "callback" objects: **Events**, **Hooks**, **Producers**, and **Mutators**.
Each handles a different API call used by the BattleBit gameserver. BitMod chooses which event to use based
on a callback's first argument. Callbacks are implemented as [lilikoi containers](https://github.com/Mooshua/Lilikoi/blob/dev/Docs/containers.md).

**Events** are one-off alerts that do not require any form of response:
```cs
public class MyPlugin
{
    [BitEvent]
    public Task OnPlayerDied(PlayerDiedEvent ev)
    {
    
        return Task.CompletedTask;
    }
}
```

**Hooks** expect a response of either *Allow*, *Disallow*, or *Neutral*. The first plugin to express
an opinion will have it's opinion selected. Plugins are executed in ascending order of priority (0 = highest, 255 = lowest).
The priority is specified by the plugin.

**Events** are one-off alerts that do not require any form of response:
```cs
public class MyPlugin
{
    [BitHook(32)]
    public async Task<Directive> OnPlayerChatted(PlayerTypedMessageEventArgs ev)
    {
        //  No hate speech!
        if (ev.Message.Contains("i hate you"))
            return Directive.Disallow;
    
        return Directive.Neutral;
    }
}
```

**Producers** produce a value out of thin air. Similarly to hooks, they are executed in ascending order of priority.
A producer can choose to either produce a value or do nothing. If a producer produces a value, then the chain ends.

Producers are used for when data is expected to appear out of thin air--For example, when loading or saving
player stats.

**Mutators** mutate the object created by the producer. They can choose to modify whatever values they want,
but are critically **not responsible for originally producing that data**. Mutators, similarly to events, will
always run regardless of what a mutator before it does.

## Contributors

| Who       | What                |
|-----------|---------------------|
| @Mooshua  | Core library design |
| @moddedmcplayer | Event Arg Classes   |
