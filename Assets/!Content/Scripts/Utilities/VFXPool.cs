namespace Game.Utilities
{
    using Game.Core;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;

    public class VFXPool
    {
        // private VFXStorage _storage;
        // private List<GameVFX> _particles = new List<GameVFX>();
        //
        // public VFXPool()
        // {
        //     _storage = GameServiceLocator.Instance.Resolve<VFXStorage>();
        //     var holder = new GameObject("VFX Holder");
        //
        //     foreach (var vfx in _storage.Data)
        //     {
        //         var parent = new GameObject($"{vfx.Key} holder");
        //         parent.transform.parent = holder.transform;
        //         
        //         for (int i = 0; i < vfx.Value.Capacity; i++)
        //         {
        //             var particle = Object.Instantiate(vfx.Value.ParticleSystem, parent.transform);
        //             particle.gameObject.SetActive(false);
        //
        //             var newVfx = new GameVFX
        //             {
        //                 ParticleSystem = particle,
        //                 Type = vfx.Key
        //             };
        //
        //             _particles.Add(newVfx);
        //         }
        //     }
        // }
        //
        // public void TrySpawnParticleOf(EVFXType type, Vector3 pos, Quaternion quaternion)
        // {
        //     if (!TryGetParticle(type, out GameVFX result)) return;
        //
        //     var ps = result.ParticleSystem;
        //     ps.transform.position = pos;
        //     ps.transform.rotation = quaternion;
        //     ps.gameObject.SetActive(true);
        //     ps.Play();
        // }
        //
        // public bool TryGetParticle(EVFXType type, out GameVFX result)
        // {
        //     result = _particles.FirstOrDefault(p => p.ParticleSystem.gameObject.activeSelf == false && p.Type == type);
        //
        //     return result != null;
        // }
        //
        // [System.Serializable]
        // public class GameVFX
        // {
        //     public ParticleSystem ParticleSystem;
        //     [HideInInspector] public EVFXType Type;
        //     public int Capacity;
        // }
    }
}