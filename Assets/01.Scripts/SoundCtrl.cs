using UnityEngine;
using System.Collections;
using Unity.Collections.LowLevel.Unsafe;
using DigitalRuby.SoundManagerNamespace;

public class SoundCtrl : ObjectSingleton<SoundCtrl>
{
    public enum SFX
    {
        STAGE_CLEAR = 0,
        
        RUNNER_JUMP,
        RUNNER_FREEZE,
        RUNNER_GET_STAR,
        RUNNER_DIE,
        RUNNER_CHANGE_DIR,
        RUNNER_SHIFT,
        RUNNER_CANNON,                
    }

    public AudioSource[] SFXSources;

    public void PlaySound(SFX sfx)
    {
        AudioSource source = SFXSources[UnsafeUtility.EnumToInt(sfx)];
        source.PlayOneShotSoundManaged(source.clip);
    }
}
