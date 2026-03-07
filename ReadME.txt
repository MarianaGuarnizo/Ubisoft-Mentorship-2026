Current state of audio implementation:

~ 15%

Integrated systems: 
- UI : fully functional, included Volume Slider (added script)

- Environments : 50%; Main Menu SFX completed, Lvl1 started and Lvl2 untouched

- Props : 75% on Lvl1, some prefab implementation will apply on Lvl2

- Chara : 0%

- Creatures : Only Chomper IDLE

- Music : 25%

Issues and limitations:

Problem with too many instances --> going to look into a script to balance

AkSoundEngine.PostEvent("EventName" , this.gameObject);
Does not recognise the ID through Sound Bank, when implementing audio directly through code

Even with Short Attenuation we can hear the Portal Idle

