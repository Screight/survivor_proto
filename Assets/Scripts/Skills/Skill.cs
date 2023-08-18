using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SurvivorProto {
    public interface ISkill
    {
        public virtual void SaveReference()
        {
            LevelManager.Instance.SkillDictionary.Add(this.GetType(), this);
        }
    }
}