using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillSystem
{
    public class GameObjectComponent : ISkillComponent
    {
        public float GetEndTime()
        {
            throw new NotImplementedException();
        }

        public int GetID()
        {
            throw new NotImplementedException();
        }

        public bool IsFinished(SkillCastData castData)
        {
            throw new NotImplementedException();
        }

        public void RunComponent(SkillCastData castData)
        {
            throw new NotImplementedException();
        }

        public void SetID(int newId)
        {
            throw new NotImplementedException();
        }

        public void ShowGUI()
        {
            throw new NotImplementedException();
        }

        public void UpdateDescription()
        {
            throw new NotImplementedException();
        }
    }
}
