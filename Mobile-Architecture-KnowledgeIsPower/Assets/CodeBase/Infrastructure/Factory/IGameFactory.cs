using System;
using System.Collections.Generic;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
    public interface IGameFactory : IService
    {
        GameObject CreateHero(GameObject at);
        GameObject CreateHud();
        List<ISavedProgressReader> ProgressReaders { get; }
        
        event Action HeroCreated;
        GameObject HeroGameObject { get; }
        List<ISavedProgress> ProgressWriters { get; }
        void RegisterProgressWatchers(EnemySpawner spawner);
        void Cleanup();
    }
}